using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Uppgift17.Models
{
    public class DrawOperation
    {
        public string TargetId { get; set; }
        public string Operation { get; set; }
        public int[] Data { get; set; }
    }
}
