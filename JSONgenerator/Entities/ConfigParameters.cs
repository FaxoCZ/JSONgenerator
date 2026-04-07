using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;
using JSONgenerator.Presentation.Windows;

namespace JSONgenerator.Entities
{
    public class ConfigParameters : IConfig
    {
        [JsonIgnore]
        public string Name { get; set; }
        [JsonPropertyName("sources")]
        public List<string> Sources { get; set; }
        [JsonPropertyName("targets")]
        public List<string> Targets { get; set; }
        [JsonPropertyName("method")]
        public string Method { get; set; }
        [JsonPropertyName("timing")]
        public string Timing { get; set; }

        [JsonPropertyName("retention")]
        public Retention retention { get; set; } = new Retention();

        public class Retention
        {
            [JsonPropertyName("count")]
            public int Count { get; set; }
            [JsonPropertyName("size")]
            public int Size { get; set; }
        }
        [JsonIgnore]
        public string? Output { get; set; }
        //[JsonIgnore]
        //public int NumberOfMethods { get; set; }
        [JsonIgnore]
        public int NumberOfSources { get; set; }
        [JsonIgnore]
        public int NumberOfTargets { get; set; }

public ConfigParameters()
{
    Name = "BackupConfig";
    Sources = new List<string>();
    Targets = new List<string>();
    Method = "Full";
    Timing = string.Empty;
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
}
}
