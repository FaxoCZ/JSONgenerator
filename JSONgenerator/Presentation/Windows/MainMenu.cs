using JSONgenerator.Presentation.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using JSONgenerator.Entities;
using JSONgenerator.Data;

namespace JSONgenerator.Presentation.Windows
{
    public class MainMenu : BaseWindow
    {

        public FileItem FileItem { get; set; }
        public FileCreatorEditor FileCreator { get; set; }

        public string CurrentDirectory { get; set; }

        private Table<FileItem> _table;

        private Label _label;

        private Button _createButton;
        private Button _editButton;
        private Button _parentButton;
        private Button _enterButton;
        public MainMenu(Application application) : base("Main menu", application)
        {
            CurrentDirectory = CurrentDirectory = Directory.GetParent(
                                  Directory.GetParent(
                                     Directory.GetParent(
                                        Directory.GetCurrentDirectory())!
                                        .FullName)!
                                        .FullName)!
                                        .FullName
                                        + "\\Data\\Output"; ; ;

            FileItem = new FileItem("", 0, DateTime.Now, "");

            FileCreator = new FileCreatorEditor(new ConfigParameters());

            _label = new Label("");

            _table = new Table<FileItem>();

            _createButton = new Button("Create", true);
            _editButton = new Button("Edit", true);
            _parentButton = new Button("Return to parent folder", true);
            _enterButton = new Button("Enter folder", true);

            RegisterComponent(_label);
            RegisterComponent(_table);
            RegisterComponent(_createButton);
            RegisterComponent(_editButton);
            RegisterComponent(_parentButton);
            RegisterComponent(_enterButton);

            _parentButton.Clicked += ParentButtonClicked;
            _enterButton.Clicked += EnterButtonClicked;
            _createButton.Clicked += CreateButtonClicked;
            _editButton.Clicked += EditButtonClicked;

            LoadFiles();

        }
         

        private void LoadFiles()
        {
            Console.Clear();
            _table.Items.Clear();
            foreach (string dir in Directory.GetDirectories(CurrentDirectory))
            {

                _table.Items.Add(new FileItem
                (
                    Path.GetFileName(dir),
                    0,
                    Directory.GetCreationTime(dir),
                    "Folder"
                ));
            }
            foreach (string file in Directory.GetFiles(CurrentDirectory))
            {
                _table.Items.Add(new FileItem
                (
                    Path.GetFileName(file),
                    new FileInfo(file).Length,
                    File.GetCreationTime(file),
                    "File"
                ));
            }

            
            LoadLabel();
        }
        private void ParentButtonClicked()
        {
            if (Directory.GetParent(CurrentDirectory) != null)
            {
                _table.Items.Clear();
                CurrentDirectory = Directory.GetParent(CurrentDirectory)!.FullName;
                LoadFiles();
            }
        }
        private void EnterButtonClicked()
        {
            var selectedItem = _table.SelectedItem;
            if (_table.SelectedItem != null && _table.SelectedItem.Type == "Folder")
            {
                _table.Items.Clear();
                CurrentDirectory = Path.Combine(CurrentDirectory, selectedItem!.Name);
            }
            LoadFiles();
        }
        private void EditButtonClicked()
        {
            FileCreatorEditor creator = new FileCreatorEditor(new ConfigParameters());

            var selectedItem = _table.SelectedItem;

            if (selectedItem == null || selectedItem.Name.Substring(selectedItem.Name.Length - 5) != ".json")
                return;

            

            string fullPath = Path.Combine(CurrentDirectory, selectedItem.Name);

            ConfigParameters parameter = creator.LoadFromFile(fullPath);
            parameter.Name = selectedItem.Name.Substring(0, selectedItem.Name.Length - 5);
            parameter.Output = fullPath;

            IWindow window = new ConfigAddEditWindow(parameter, _application, this);
            window.Submitted += () =>
            {
                FileCreatorEditor creator = new FileCreatorEditor(parameter);
                creator.CreateFile();

                LoadFiles();
            };
            window.Show();
        }

        private void CreateButtonClicked()
        {
            ConfigParameters parameter = new ConfigParameters();
            FileCreatorEditor creator = new FileCreatorEditor(parameter);
            parameter.Output = CurrentDirectory;
            IWindow window = new ConfigAddEditWindow(parameter, _application, this);

            window.Submitted += () =>
            {
                creator.CreateFile();
                LoadFiles();
            };
            window.Show();
        }
        private void LoadLabel()
        {
            _label.Text = $"Files in current directory: {CurrentDirectory} ";
        }
    }
}
