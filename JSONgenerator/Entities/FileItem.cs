using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSONgenerator.Entities
{
    public class FileItem
    {
        public string Name { get; set; }
        public long Size { get; set; }
        public DateTime Created { get; set; }
        public string Type { get; set; }

        public FileItem(string name, long size, DateTime created, string type)
        {
            Name = name;
            Size = size;
            Created = created;
            Type = type;
        }
    }
}
