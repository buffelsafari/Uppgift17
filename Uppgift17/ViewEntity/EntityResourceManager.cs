using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Uppgift17.Models;

namespace Uppgift17.ViewEntity
{
    public class EntityResourceManager:IEntityResourceManager
    {
        private HttpClient http;

        private Dictionary<string, List<DrawOperation>> elementOperationDictionary = new Dictionary<string, List<DrawOperation>>();

        

        public EntityResourceManager(HttpClient http)
        {
            this.http = http;

            //Load(); // todo register 
        }

        

        private async Task<List<DrawOperation>> Load(string fileName)
        { 
            var randomid = Guid.NewGuid().ToString();// fulhack to prevent browser cache during dev
            
            FactoryDto fore = await http.GetFromJsonAsync<FactoryDto>($"sample-data/{fileName}.json?{randomid}");

            List<DrawOperation> list=new List<DrawOperation>();
            list.AddRange(fore.Operations);

            return list;

            //elementOperationDictionary.Add(fileName, list);
        }

        public async Task<IEnumerable<DrawOperation>> GetOperations(string name)
        {
            if (elementOperationDictionary.ContainsKey(name))
            {
                return elementOperationDictionary[name];
            }

            List<DrawOperation> list = await Load(name);

            elementOperationDictionary.Add(name, list);
            return list;

            // todo handle wrong file
            
        }

    }

    public class FactoryDto 
    { 
        public IEnumerable<DrawOperation> Operations { get; set; }
    }
}
