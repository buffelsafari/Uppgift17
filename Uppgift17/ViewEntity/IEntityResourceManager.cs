using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Uppgift17.Models;

namespace Uppgift17.ViewEntity
{
    public interface IEntityResourceManager
    {
        
        Task<IEnumerable<DrawOperation>> GetOperations(string name);
    }
}
