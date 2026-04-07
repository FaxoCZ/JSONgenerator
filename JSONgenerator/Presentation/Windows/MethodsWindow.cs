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
        private Button _cancelButton;
        private List<ConfigParameters> _methods { get; set; }
        private FileCreatorEditor _editor;
        private MethodCounterHelper _counterHelper;

        private ConfigParameters _configParameters;
        public MethodsWindow(ConfigParameters parameters,string title, Application application, IWindow? returnWindow = null) : base(title, application, returnWindow)
        {
            _configParameters = parameters;
            
            _editor = new FileCreatorEditor(parameters);
            _counterHelper = new MethodCounterHelper("");

            _methods = new List<ConfigParameters>
            {
                _editor.LoadFromFile(parameters.Output!)
            };

            _table = new Table<MethodCounterHelper>();

            _selectButton = new Button("Select", true);
            _cancelButton = new Button("Cancel", true);

            RegisterComponent(_table);
            RegisterComponent(_selectButton);
            RegisterComponent(_cancelButton);

            _cancelButton.Clicked += Close;

            LoadMethods(_methods);
        }
        public void LoadMethods(List<ConfigParameters> methods)
        {
            foreach (ConfigParameters method in methods)
            {
                _table.Items.Add(new MethodCounterHelper(
                    method.Method
                ));
            }
        }

    }
}
