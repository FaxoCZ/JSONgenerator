using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using JSONgenerator.Presentation.Windows;

namespace JSONgenerator.Entities
{
    public class ConfigParameters : IConfig
    {
        public string Name { get; set; }
        public string Sources { get; set; }
        public string Targets { get; set; }
        public string Method { get; set; }
        public string Timing { get; set; }
        public int Count { get; set; }
        public int Size { get; set; }
        public string Output { get; set; }

        public ConfigParameters()
        {
            Name = "BackupConfig";
            Sources = string.Empty;
            Targets = string.Empty;
            Method = "Full";
            Timing = string.Empty;
            Count = 1;
            Size = 1;
            Output = GetOutput();
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
        public void CreateConfig(string path)
        {
            string json = JsonSerializer.Serialize(this, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(path, json);
        }
    }
}
