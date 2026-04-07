using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace JSONgenerator.Entities
{
    public class MethodCounterHelper
    {
        public int Number { get; set; }
        public string Method { get; set; }
        [JsonIgnore]
        public ConfigParameters Parameter;
        public MethodCounterHelper(ConfigParameters parameter, int number)
        {
            Number = number;
            Parameter = parameter;
            Method = parameter.Method;
        }

    }
    
}
