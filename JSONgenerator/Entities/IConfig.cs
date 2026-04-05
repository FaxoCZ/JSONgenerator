using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSONgenerator.Entities
{
    public interface IConfig
    {
        public string Name { get; set; }
        public List<string> Sources { get; set; }
        public List<string> Targets { get; set; }
        public string Method { get; set; }
        public string Timing { get; set; }
    }
}
