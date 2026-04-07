using JSONgenerator.Entities;
using JSONgenerator.Entities;
using JSONgenerator.Presentation.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
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
        private TextBoxEnter _sourcesTextBoxEnter;
        private TextBoxEnter _targetsTextBoxEnter;
        private TextBox _methodTextBox;
        private TextBox _timingTextBox;
        private TextBox _countTextBox;
        private TextBox _sizeTextBox;
        private TextBoxEnter _outputTextBoxEnter;

        private Button _saveButton;
        private Button _cancelButton;
        private Button _addSourceButton;
        private Button _addTargetButton;        

        private CparametersList _parametersList;

        public ConfigAddEditWindow(ConfigParameters parameters, Application application, IWindow? returnWindow = null)
            : base("JSON editor", application, returnWindow)
        {
            _parameters = parameters;

            _parametersList = new CparametersList();

            DataValidator = new DataValidator();

            _nameTextBox = new TextBox("Name: ", 20);
            _sourcesTextBoxEnter = new TextBoxEnter("Sources: ", 0, false);
            _targetsTextBoxEnter = new TextBoxEnter("Targets: ", 0, false);
            _methodTextBox = new TextBox("Method: ", 10, false);
            _timingTextBox = new TextBox("Timing: ", 10);
            _countTextBox = new TextBox("Count: ", 2);
            _sizeTextBox = new TextBox("Size: ", 2);
            _outputTextBoxEnter = new TextBoxEnter("Output: ", 0);

            _saveButton = new Button("Save", true);
            _cancelButton = new Button("Cancel", true);
            _addSourceButton = new Button("Add Source", true);
            _addTargetButton = new Button("Add Target", true);

            RegisterComponent(_nameTextBox);
            RegisterComponent(_sourcesTextBoxEnter);
            RegisterComponent(_targetsTextBoxEnter);
            RegisterComponent(_methodTextBox);
            RegisterComponent(_timingTextBox);
            RegisterComponent(_countTextBox);
            RegisterComponent(_sizeTextBox);
            RegisterComponent(_outputTextBoxEnter);




            RegisterComponent(_saveButton);
            RegisterComponent(_cancelButton);

            _saveButton.Clicked += SaveButtonClicked;
            _cancelButton.Clicked += CancelButtonClicked;
            _sourcesTextBoxEnter.Clicked += SourcesTextBoxEnterClicked;
            _sourcesTextBoxEnter.Deleted += SourcesTextBoxEnterDeleted;
            _targetsTextBoxEnter.Clicked += TargetsTextBoxEnterClicked;
            _targetsTextBoxEnter.Deleted += TargetTextBoxEnterDeleted;
            _outputTextBoxEnter.Clicked += OutputTextBoxEnterClicked;
            _outputTextBoxEnter.Deleted += OutputTextBoxEnterDeleted;


            SetComponentValues();
        }

        private void SetComponentValues()
        {
            _nameTextBox.Value = _parameters.Name;
            _sourcesTextBoxEnter.Value = string.Join(", ", _parameters.Sources);
            _targetsTextBoxEnter.Value = string.Join(", ", _parameters.Targets);
            _methodTextBox.Value = _parameters.Method ?? "Full";
            _timingTextBox.Value = _parameters.Timing ?? "";
            _countTextBox.Value = Convert.ToString(_parameters.retention.Count) ?? "";
            _sizeTextBox.Value = Convert.ToString(_parameters.retention.Size)!;
            _outputTextBoxEnter.Value = _parameters.Output!;
        }

        private void SetEntityValues()
        {
            if(_parameters.retention == null)
            {
                _parameters.retention = new ConfigParameters.Retention();
            }
            _parameters.Name = _nameTextBox.Value;
            _parameters.Sources = _sourcesTextBoxEnter.Value.Split(',').Select(x => x.Trim()).Where(x => x.Length > 0).ToList();
            _parameters.Targets = _targetsTextBoxEnter.Value.Split(',').Select(y => y.Trim()).Where(y => y.Length > 0).ToList();
            _parameters.Method = _methodTextBox.Value;
            _parameters.Timing = _timingTextBox.Value;
            _parameters.retention.Count = Convert.ToInt32(_countTextBox.Value);
            _parameters.retention.Size = Convert.ToInt32(_sizeTextBox.Value);
            _parameters.Output = _outputTextBoxEnter.Value;
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
        private void SourcesTextBoxEnterClicked()
        {
            
            NOWindow sourcewindow = new NOWindow(GetValue(_sourcesTextBoxEnter),"", _application, this);
            sourcewindow.Submitted += (value) =>
            {
                if (_sourcesTextBoxEnter.Value == "")
                {
                    _sourcesTextBoxEnter.Value = value;
                }
                else
                {
                    _sourcesTextBoxEnter.Value += ", " + value;
                }
            };
            sourcewindow.Show();
        }
        private void TargetsTextBoxEnterClicked()
        {

            NOWindow targetswindow = new NOWindow(GetValue(_targetsTextBoxEnter), "", _application, this);
            targetswindow.Submitted += (value) =>
            {
                if (_targetsTextBoxEnter.Value == "")
                {
                    _targetsTextBoxEnter.Value = value;
                }
                else
                {
                    _targetsTextBoxEnter.Value += ", " + value;
                }
            };
            targetswindow.Show();
        }
        private void OutputTextBoxEnterClicked()
        {
            if (_outputTextBoxEnter.Value != "")
                return;

            IWindow outputwindow = new NOWindow(GetValue(_outputTextBoxEnter), "", _application, this);
            ((NOWindow)outputwindow).Submitted += (value) =>
            {
                if (_outputTextBoxEnter.Value == "")
                {
                    _outputTextBoxEnter.Value = value;
                }
                else
                {
                    _outputTextBoxEnter.Value += ", " + value;
                }
            };
            outputwindow.Show();
        }
        private void OutputTextBoxEnterDeleted()
        {
            IWindow deleteWindow = new DeletingWindow(GetValue(_outputTextBoxEnter), "", _application, this);
            ((DeletingWindow)deleteWindow).Submitted += (value) =>
            {
                _outputTextBoxEnter.Value = (value);
            };
            deleteWindow.Show();
        }
        private void SourcesTextBoxEnterDeleted()
        {
            IWindow deleteWindow = new DeletingWindow(GetValue(_sourcesTextBoxEnter), "", _application, this);
            ((DeletingWindow)deleteWindow).Submitted += (value) =>
            {
                _sourcesTextBoxEnter.Value = (value);
            };
            deleteWindow.Show();
        }
        private void TargetTextBoxEnterDeleted()
        {
            IWindow targetWindow = new DeletingWindow(GetValue(_targetsTextBoxEnter), "", _application, this);
            ((DeletingWindow)targetWindow).Submitted += (value) =>
            {
                _targetsTextBoxEnter.Value = (value);
            };
            targetWindow.Show();
        }
        private List<string> GetValue(TextBoxEnter list)
        {
            return list.Value.Split(',').Select(x => x.Trim()).Where(x => x.Length > 0).ToList();
        }
    }
}
