using JSONgenerator.Data;
using JSONgenerator.Entities;
using JSONgenerator.Presentation.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace JSONgenerator.Presentation.Windows
{
    public class ConfigAddEditWindow : BaseWindow
    {
        public DataValidator DataValidator { get; set; }
        private ConfigParameters _parameters;

        private TextBox _nameTextBox;
        private TextBox _sourcesTextBox;
        private TextBox _targetsTextBox;
        private TextBox _methodTextBox;
        private TextBox _timingTextBox;
        private TextBox _countTextBox;
        private TextBox _sizeTextBox;
        private TextBox _outputTextBox;
        //private TextBox _numberOfMethodsTextBox;

        private Button _saveButton;
        private Button _cancelButton;

        public ConfigAddEditWindow(ConfigParameters parameters, Application application, IWindow? returnWindow = null)
            : base("JSON editor", application, returnWindow)
        {
            _parameters = parameters;

            DataValidator = new DataValidator();

            _nameTextBox = new TextBox("Name: ", 20);
            _sourcesTextBox = new TextBox("Sources: ", 100);
            _targetsTextBox = new TextBox("Targets: ", 100);
            _methodTextBox = new TextBox("Method: ", 12);
            _timingTextBox = new TextBox("Timing: ", 10);
            _countTextBox = new TextBox("Count: ", 2);
            _sizeTextBox = new TextBox("Size: ", 2);
            _outputTextBox = new TextBox("Output: ", 100);

            _saveButton = new Button("Save", true);
            _cancelButton = new Button("Cancel", true);

            RegisterComponent(_nameTextBox);
            RegisterComponent(_sourcesTextBox);
            RegisterComponent(_targetsTextBox);
            RegisterComponent(_methodTextBox);
            RegisterComponent(_timingTextBox);
            RegisterComponent(_countTextBox);
            RegisterComponent(_sizeTextBox);
            RegisterComponent(_outputTextBox);
            
            RegisterComponent(_saveButton);
            RegisterComponent(_cancelButton);

            _saveButton.Clicked += SaveButtonClicked;
            _cancelButton.Clicked += CancelButtonClicked;

            SetComponentValues();
        }

        private void SetComponentValues()
        {
            _nameTextBox.Value = _parameters.Name!;
            _sourcesTextBox.Value = string.Join(", ", _parameters.Sources);
            _targetsTextBox.Value = string.Join(", ", _parameters.Targets);
            _methodTextBox.Value = _parameters.Method ?? "Full";
            _timingTextBox.Value = _parameters.Timing ?? "";
            _countTextBox.Value = Convert.ToString(_parameters.retention.Count) ?? "";
            _sizeTextBox.Value = Convert.ToString(_parameters.retention.Size)!;
            _outputTextBox.Value = _parameters.Output!;
        }

        private void SetEntityValues()
        {
            _parameters.Name = _nameTextBox.Value;
            _parameters.Sources = _sourcesTextBox.Value.Split(',').Select(x => x.Trim()).ToList();
            _parameters.Targets = _targetsTextBox.Value.Split(',').Select(x => x.Trim()).ToList();
            _parameters.Method = _methodTextBox.Value;
            _parameters.Timing = _timingTextBox.Value;
            _parameters.retention.Count = Convert.ToInt32(_countTextBox.Value);
            _parameters.retention.Size = Convert.ToInt32(_sizeTextBox.Value);
            _parameters.Output = _outputTextBox.Value;
        }

        private void SaveButtonClicked()
        {
            SetEntityValues();
            if (!DataValidator.CheckData(_parameters))
            {   
                DataInvalid();
                return;
            }
            Submit();
        }

        private void CancelButtonClicked()
        {
            Close();
        }
        private void DataInvalid()
        {
            IWindow dialog = new InvalidWindow("Invalid data", "Please try again.", _application, this);
            dialog.Show();
        }
    }
}
