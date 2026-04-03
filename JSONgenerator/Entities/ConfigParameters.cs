using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSONgenerator.Entities
{
    public class ConfigParameters : IConfig
    {
        public string Sources { get; set; }
        public string Targets { get; set; }
        public string Method { get; set; }
        public string Timing { get; set; }
        public int Count { get; set; }
        public int Size { get; set; }
        public string Output { get; set; }
        public int NumberOfMethods { get; set; }

        public ConfigParameters(string sources, string targets, string method, string timing, int count, int size)
        {
            Sources = sources;
            Targets = targets;
            Method = method;
            Timing = timing;
            Count = count;
            Size = size;
            Output = GetOutput();
            NumberOfMethods = 0;
        }

        private string GetOutput()
        {
            string OutputDir = Directory.GetParent(
                Directory.GetParent(
                    Directory.GetParent(
                            Directory.GetCurrentDirectory())!
                            .FullName)!
                            .FullName)!
                            .FullName
                            + "\\Data\\Output";

            return OutputDir;
        }
    }
}
