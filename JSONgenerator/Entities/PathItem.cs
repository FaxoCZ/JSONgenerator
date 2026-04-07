using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSONgenerator.Entities
{
    public class PathItem
    {
        public string Path { get; set; }
        public string Type { get; set; }

        public PathItem(string path)
        {
            Path = path;
            Type = "Path";
        }
    }
}
