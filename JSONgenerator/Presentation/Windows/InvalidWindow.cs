using JSONgenerator.Presentation.Components;

namespace JSONgenerator.Presentation.Windows
{
    public class InvalidWindow : BaseWindow
    {
        private Label _label;
        private Button _okButton;

        public InvalidWindow(string title, string text, Application application, IWindow? returnWindow = null) 
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
