using JSONgenerator.Entities;
using JSONgenerator.Presentation.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSONgenerator.Presentation.Windows
{
    public class NOWindow : BaseWindow
    {
        private Label _label;
        private Table<FileItem> _tabledir;
        //private Table<List<string>> _tablecurrValue;
        private Button _addButton;
        private Button _parentButton;
        private Button _enterButton;
        public new event Action<string>? Submitted;
        //public List<string> CurrentValue { get; set; }

        private string CurrentDirectory { get; set; }

        public NOWindow(List<string> currentValue
            , 
            string title, Application application, IWindow? returnWindow = null)
            : base(title, application, returnWindow)
        {
            CurrentDirectory = Directory.GetParent(
                                  Directory.GetParent(
                                     Directory.GetParent(
                                        Directory.GetCurrentDirectory())!
                                        .FullName)!
                                        .FullName)!
                                        .FullName
                                        + "\\Data\\Output"; ; ;

            _label = new Label(title);

           // CurrentValue = currentValue ?? new List<string>();

            _tabledir = new Table<FileItem>();
            //_tablecurrValue = new Table<List<string>>();
            
            _addButton = new Button("Select directory",true);
            _parentButton = new Button("Return to parent folder", true);
            _enterButton = new Button("Enter folder", true);


            RegisterComponent(_label);
            RegisterComponent(_tabledir);
            //RegisterComponent(_tablecurrValue);
            RegisterComponent(_addButton);
            RegisterComponent(_parentButton);
            RegisterComponent(_enterButton);

            _addButton.Clicked += Submit;
            _parentButton.Clicked += ParentButtonClicked;
            _enterButton.Clicked += EnterButtonClicked;

            LoadDirectory();
            //LoadCurrentValue();
        }
        public new void Submit()
        {
            
            Submitted?.Invoke(CurrentDirectory);
            Close();
            

        }
        private void ParentButtonClicked()
        {
            if (Directory.GetParent(CurrentDirectory) != null)
            {
                _tabledir.Items.Clear();
                CurrentDirectory = Directory.GetParent(CurrentDirectory)!.FullName;
                LoadDirectory();
            }
        }
        private void EnterButtonClicked()
        {
            var selectedItem = _tabledir.SelectedItem;
            if (_tabledir.SelectedItem != null && _tabledir.SelectedItem.Type == "Folder")
            {
                _tabledir.Items.Clear();
                CurrentDirectory = Path.Combine(CurrentDirectory, selectedItem!.Name);
            }
            LoadDirectory();
        }
        private void LoadDirectory()
        {
            Console.Clear();
            _tabledir.Items.Clear();
            foreach (string dir in Directory.GetDirectories(CurrentDirectory))
            {

                _tabledir.Items.Add(new FileItem
                (
                    Path.GetFileName(dir),
                    0,
                    Directory.GetCreationTime(dir),
                    "Folder"
                ));
            }
            LoadLabel();
            LoadCurrentValue();
        }
        private void LoadCurrentValue()
        {

        }
        private void LoadLabel()
        {
            _label.Text = $"Current directory: {CurrentDirectory} ";
        }
    }
}
