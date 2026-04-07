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


            JsonSerializerOptions options = new JsonSerializerOptions { WriteIndented = true };

            string output = _parameters.Output!.Trim();

            string filePath;

            if (Path.HasExtension(output))
            {
                filePath = output;
            }
            else
            {
                string name = string.IsNullOrWhiteSpace(_parameters.Name)
                    ? "default"
                    : Path.GetFileNameWithoutExtension(_parameters.Name.Trim());

                filePath = Path.Combine(output, name + ".json");
            }


            if (File.Exists(filePath))
            {
                string existingJson = File.ReadAllText(filePath);
                root = JsonSerializer.Deserialize<CparametersList>(existingJson) ?? new CparametersList();
            }
            else
            {
                root = new CparametersList();
            }

            root.Methods.Add(_parameters);

            string json = JsonSerializer.Serialize(root, options);
            File.WriteAllText(filePath, json);
        }
        public List<ConfigParameters> LoadFromFile(string path)
        {
            
            string json = File.ReadAllText(path);

            JsonSerializerOptions options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            CparametersList root = JsonSerializer.Deserialize<CparametersList>(json)!;
            return root?.Methods ?? new List<ConfigParameters>();
        }
    }
}
