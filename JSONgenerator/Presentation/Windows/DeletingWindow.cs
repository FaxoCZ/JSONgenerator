using JSONgenerator.Data;
using JSONgenerator.Entities;
using JSONgenerator.Presentation.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSONgenerator.Presentation.Windows
{
    public class DeletingWindow : BaseWindow
    {
        private Label _label;
        private Table<PathItem> _table;
        private Button _deleteButton;
        public new event Action<string>? Submitted;
        public List<string> CurrentValue;
        public string Output;


        public DeletingWindow(List<string> currentValue
            ,
            string title, Application application, IWindow? returnWindow = null)
            : base(title, application, returnWindow)
        {

            _label = new Label(title);

            Output = "";

            CurrentValue = currentValue;

            _table = new Table<PathItem>();

            _deleteButton = new Button("Delete", true);


            RegisterComponent(_label);
            RegisterComponent(_table);
            RegisterComponent(_deleteButton);

            _deleteButton.Clicked += DeleteButtonClicked;

            LoadData();
        }
        public new void Submit()
        {
            foreach (var item in _table.Items)
            {
                if (Output == "")
                {
                    Output = item.Path.ToString();
                }
                else
                {
                    Output += ", " + item.Path.ToString();
                }
            }


            Submitted?.Invoke(Output);
            Close();


        }
       
        private void LoadData()
        {
            Console.Clear();
            _table.Items.Clear();
            foreach (string value in CurrentValue)
            {
                _table.Items.Add(new PathItem
                (
                    value
                ));

            }
            LoadLabel();
        }
        private void LoadLabel()
        {
            _label.Text = $"Delete: ";
        }
        private void DeleteButtonClicked()
        {
            _table.Items.Remove(_table.SelectedItem!);
            Submit();
        }
    }
}
