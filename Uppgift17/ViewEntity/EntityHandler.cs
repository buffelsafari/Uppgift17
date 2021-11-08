using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Uppgift17.Models;

namespace Uppgift17.ViewEntity
{
    public class EntityHandler
    {
        Entity baseEntity;
        public EntityHandler(IEntityResourceManager entityResourceManager)
        {
            baseEntity = new Entity(entityResourceManager, "factory_walls", 45, 100, 0);



            Entity child = new Entity(entityResourceManager, "factory_walls2", 0, 100, 100);
            baseEntity.AddChild(child);


            Entity machine1 = new Entity(entityResourceManager, "square_machine", 0, 600, 600);
            baseEntity.AddChild(machine1);

            Entity machine2 = new Entity(entityResourceManager, "triangle_machine", 0, -600, -600);
            baseEntity.AddChild(machine2);

            Entity machine3 = new Entity(entityResourceManager, "circle_machine", 30, 0, 0);
            baseEntity.AddChild(machine3);

            Entity machine4 = new Entity(entityResourceManager, "star_machine", 0, 200, 200);
            baseEntity.AddChild(machine4);
        }

        public async Task<IEnumerable<DrawOperation>> GetDrawOperations()
        {
            return await baseEntity.GetDrawOperations();
        }

        public void AddChild(Entity child)
        {
            baseEntity.AddChild(child);
        }

        public void TranslateEntity(string id, double x, double y)
        {
            baseEntity.TranslateEntity(id, x, y);
        }

        public void StopTranslatingEntity()
        {
            baseEntity.StopTranslatingEntity();
        }
    }
}
