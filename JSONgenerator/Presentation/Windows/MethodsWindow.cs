using JSONgenerator.Entities;
using JSONgenerator.Presentation.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSONgenerator.Presentation.Windows
{
    public class MethodsWindow : BaseWindow
    {
        private Table<MethodCounterHelper> _table;
        private Button _selectButton;
        private Button _addButton;
        private Button _cancelButton;
        private List<ConfigParameters> _methods { get; set; }
        private FileCreatorEditor _editor;
        private MethodCounterHelper _counterHelper;
        private string _fileName;


        private ConfigParameters _configParameters;
        public MethodsWindow(ConfigParameters parameters, string title, Application application, IWindow? returnWindow = null) : base(title, application, returnWindow)
        {
            _configParameters = parameters;

            _editor = new FileCreatorEditor(parameters);
            _counterHelper = new MethodCounterHelper(parameters, 0);

            _methods = _editor.LoadFromFile(parameters.Output!);

            _fileName = title;

            _table = new Table<MethodCounterHelper>();

            _selectButton = new Button("Select", true);
            _addButton = new Button("Add", true);
            _cancelButton = new Button("Cancel", true);

            RegisterComponent(_table);
            RegisterComponent(_selectButton);
            RegisterComponent(_addButton);
            RegisterComponent(_cancelButton);

            _cancelButton.Clicked += Close;
            _selectButton.Clicked += EditSelectedMethod;
            _addButton.Clicked += AddButtonClicked;

            LoadMethods(_methods);
        }
        public void LoadMethods(List<ConfigParameters> methods)
        {
            int i = 1;
            foreach (ConfigParameters method in methods)
            {
                _table.Items.Add(new MethodCounterHelper(
                    method,
                    i++
                ));
            }
        }
        private void EditSelectedMethod()
        {
            MethodCounterHelper selected = (MethodCounterHelper)_table.SelectedItem!;

            if (selected == null)
                return;

            ConfigParameters parameter = selected.Parameter;

            IWindow window = new ConfigAddEditWindow(parameter, _application, this);

            window.Submitted += () =>
            {
                _table.Items.Clear();
                LoadMethods(_methods);
            };
            window.Show();
        }
        private void AddButtonClicked()
        {
            ConfigParameters newParameter = new ConfigParameters
            {
                Name = Path.GetFileNameWithoutExtension(_configParameters.Name)
            };

            FileCreatorEditor editor = new FileCreatorEditor(newParameter);

            IWindow window = new ConfigAddEditWindow(newParameter, _application, this);
            window.Submitted += () =>
            {
                FileCreatorEditor editor = new FileCreatorEditor(newParameter);

                editor.CreateFile();

                _methods = editor.LoadFromFile(
                    Path.Combine(newParameter.Output!, newParameter.Name + ".json")
                );

                _table.Items.Clear();
                LoadMethods(_methods);
            };

            window.Show();

        }
    }
}
