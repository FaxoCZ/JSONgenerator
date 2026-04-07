using JSONgenerator.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace JSONgenerator.Entities
{
    public class FileCreatorEditor
    {
        ConfigParameters _parameters;
        CparametersList root;

        public FileCreatorEditor(ConfigParameters parameters)
        {
            _parameters = parameters;
            root = new CparametersList();

        }
        public void CreateFile()
        {
            var options = new JsonSerializerOptions { WriteIndented = true };

            string filePath = Path.Combine(_parameters.Output!, _parameters.Name + ".json");


            if (File.Exists(filePath))
            {
                string existingJson = File.ReadAllText(filePath);
                root = JsonSerializer.Deserialize<CparametersList>(existingJson) ?? new CparametersList();
            }
            else
            {
                root = new CparametersList();
            }

            // 🔑 tady je fix — přidání místo přepsání
            root.Methods.Add(_parameters);

            string json = JsonSerializer.Serialize(root, options);
            File.WriteAllText(filePath, json);
        }
        public List<ConfigParameters> LoadFromFile(string path)
        {
            string json = File.ReadAllText(path);

            var root = JsonSerializer.Deserialize<CparametersList>(json);

            return root?.Methods ?? new List<ConfigParameters>();
        }
    }
}
