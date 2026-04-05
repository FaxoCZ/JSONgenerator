using JSONgenerator.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace JSONgenerator.Data
{
    public class FileCreatorEditor
    {
        ConfigParameters _parameters;

        public FileCreatorEditor(ConfigParameters parameters)
        {
            _parameters = parameters;

        }
        public void CreateFile()
        {
            var options = new JsonSerializerOptions();
            options.WriteIndented = true;

            string outputPath = _parameters.Output!;

            string json = JsonSerializer.Serialize(_parameters, options);
            if (Path.HasExtension(outputPath))
            {
                outputPath = Path.GetDirectoryName(outputPath)!;
            }

            File.WriteAllText(
                Path.Combine(outputPath, _parameters.Name + ".json"),
                json
            );
        
        }
        public ConfigParameters LoadFromFile(string path)
        {
            string json = File.ReadAllText(path);

            return JsonSerializer.Deserialize<ConfigParameters>(json)!;
        }
    }
}
