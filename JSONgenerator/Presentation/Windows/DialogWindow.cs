using JSONgenerator.Presentation.Components;

namespace JSONgenerator.Presentation.Windows
{
    public class DialogWindow : BaseWindow
    {
        private Label _label;
        private Button _okButton;

        public DialogWindow(string title, string text, Application application, IWindow? returnWindow = null) 
            : base(title, application, returnWindow)
        {
            _label = new Label(text);
            _okButton = new Button("OK", true);

            RegisterComponent(_label);
            RegisterComponent(_okButton);

            _okButton.Clicked += Submit;

        }
    }
}
