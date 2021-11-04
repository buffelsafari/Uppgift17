using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Uppgift17.Models;

namespace Uppgift17.ViewEntity
{
    public class Entity
    {
        


        List<DrawOperation> operationHeaderList = new List<DrawOperation>();
        List<DrawOperation> operationFooterList = new List<DrawOperation>();
        
        List<DrawOperation> operationList=new List<DrawOperation>();

         
        

        List<Entity> childList=new List<Entity>();

        public Entity(int angle)
        {
            operationHeaderList.Add(new DrawOperation { Operation = "save", Data = null });
            operationHeaderList.Add(new DrawOperation { Operation = "rotate", Data = new int[] { angle } });


            operationFooterList.Add(new DrawOperation { Operation = "restore", Data = null });
           
        }

        public void AddOperation(DrawOperation op)
        {
            operationList.Add(op);
        }

        public void AddOperation(IEnumerable<DrawOperation> ops)
        {
            operationList.AddRange(ops);
        }

        public void AddChild(Entity child)
        {
            childList.Add(child);
        }

        public IEnumerable<DrawOperation> GetDrawOperations()
        {
            IEnumerable<DrawOperation> dList=operationHeaderList;
            dList=dList.Concat(operationList);

            foreach (Entity child in childList)
            {
                Console.WriteLine("children----------"+child);
                dList=dList.Concat<DrawOperation>(child.GetDrawOperations());
            }

            dList = dList.Concat(operationFooterList);

            return dList;
            
            
        }
    }
}
