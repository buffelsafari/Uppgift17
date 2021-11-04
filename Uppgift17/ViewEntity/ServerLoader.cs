using Microsoft.AspNetCore.Components.WebAssembly.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Uppgift17.Models;

namespace Uppgift17.ViewEntity
{
    public class ServerLoader
    {

        

        private ServerLoader()
        { 
        
        }

        public static async Task<Entity> Load(HttpClient Http)
        {

            var randomid = Guid.NewGuid().ToString();// fulhack to prevent browser cache during dev
            

            Console.WriteLine("-----------------------------------------factory stuff----------------------------------------");
            FactoryElement fore=await Http.GetFromJsonAsync< FactoryElement>($"sample-data/factory_walls.json?{randomid}");

            
            
            Entity entity = new Entity(0);
            entity.AddOperation(fore.Operations);
            return entity;
            
            
        }
    }

    public class FactoryElement
    {
        public string Name { get; set; }
        public IEnumerable<DrawOperation> Operations { get; set; }
    }
}
