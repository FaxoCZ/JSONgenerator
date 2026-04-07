using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSONgenerator.Entities
{
    public class MethodCounterHelper
    {
        public int Number { get; set; }
        public string Method { get; set; }
        public MethodCounterHelper(string method)
        {
            Number = 0;
            Method = method;
        }

    }
    
}
