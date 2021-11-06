using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Uppgift17.Models;

namespace Uppgift17.ViewEntity
{
    public class Entity
    {

        static Dictionary<string, Entity> entityDictionary=new Dictionary<string, Entity>();
        static int selectedX;
        static int selectedY;
        static string selectedId;

        public string Id { get; }
        DrawOperation fillColor;

        List<DrawOperation> operationHeaderList = new List<DrawOperation>();
        List<DrawOperation> operationEndList = new List<DrawOperation>();

        List<DrawOperation> operationFooterList = new List<DrawOperation>();

        //List<DrawOperation> operationList=new List<DrawOperation>();

        string operationsResource = "factory_walls";

        IEntityResourceManager entityResourceManager;

        List<Entity> childList=new List<Entity>();
        private int x, y, angle;

        public Entity(IEntityResourceManager entityResourceManager, string operationsResource, int angle, int x, int y)
        {
            

            this.Id = Guid.NewGuid().ToString("n"); 
            this.angle = angle;
            this.x = x;
            this.y = y;

            this.entityResourceManager = entityResourceManager;
            this.operationsResource = operationsResource;

            
            operationHeaderList.Add(new DrawOperation { Operation = "save", Data = null });
            operationHeaderList.Add(new DrawOperation { Operation = "rotate", Data = new int[] { angle } });            
            operationHeaderList.Add(new DrawOperation { Operation = "translate", Data = new int[] { x, y } });

            fillColor = new DrawOperation{ Operation = "fillRGBA", Data = new int[] { 0, 255, 0, 255 } };

            operationEndList.Add(new DrawOperation { Operation = "lineWidth", Data = new int[] { 8 } });
            operationEndList.Add(new DrawOperation { Operation = "strokeRGBA", Data = new int[] {255,255,0,255 } });
            operationEndList.Add(fillColor);
            operationEndList.Add(new DrawOperation { Operation = "testClick", TargetId=this.Id, Data = null });
            
            
            //operationEndList.Add(new DrawOperation { Operation = "fill", Data = null });
            operationEndList.Add(new DrawOperation { Operation = "stroke", Data = null});
            




            operationFooterList.Add(new DrawOperation { Operation = "restore", Data = null });


            //    {
            //        "TargetId": "",
            //    "Operation": "stroke",
            //    "Data": []
            //}

            //    {
            //        "TargetId": "test_id",
            //    "Operation": "testClick",
            //    "Data": []
            //},
            //{
            //        "TargetId": "",
            //    "Operation": "closePath",
            //    "Data": []
            //}


            entityDictionary.Add(this.Id, this);


        }



        public void TranslateEntity(string id, double x, double y)
        {
           
            Entity entity = entityDictionary[id];            

            if (selectedId == null)
            {
                selectedId = entity.Id;
                selectedX = entity.x;
                selectedY = entity.y;
            }

            entity.x = (int)x+selectedX;
            entity.y = (int)y+selectedY;


        }

        public void StopTranslatingEntity()
        {
            selectedId = null;
        }

       



        //public async Task LoadResources(IEntityResourceManager entityResourceManager)
        //{
        //    this.AddOperation(await entityResourceManager.GetOperations("factory_walls"));

        //}        

        //public void AddOperation(DrawOperation op)
        //{
        //    //operationList.Add(op);
        //}

        //public void AddOperation(IEnumerable<DrawOperation> ops)
        //{
        //    //operationList.AddRange(ops);
        //}

        public void AddChild(Entity child)
        {
            childList.Add(child);
        }

        

        public async Task<IEnumerable<DrawOperation>> GetDrawOperations()
        {
            //angle = angle + 1;
            operationHeaderList.Clear();
            operationHeaderList.Add(new DrawOperation { Operation = "save", Data = null });
            operationHeaderList.Add(new DrawOperation { Operation = "rotate", Data = new int[] { angle } });
            operationHeaderList.Add(new DrawOperation { Operation = "translate", Data = new int[] { x, y } });






            IEnumerable<DrawOperation> dList=operationHeaderList;
            dList=dList.Concat(await entityResourceManager.GetOperations(operationsResource)).Concat(operationEndList);

            foreach (Entity child in childList)
            {
                //Console.WriteLine("children----------"+child);
                dList=dList.Concat<DrawOperation>(await child.GetDrawOperations());
            }

            dList = dList.Concat(operationFooterList);


            
            return dList;
            
            
        }
    }
}
