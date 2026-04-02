using JSONgenerator.Presentation.Windows;

namespace JSONgenerator
{
    public class Program
    {
        static void Main(string[] args)
        {
            Application app = new Application();

            IWindow window = new MainMenu(app);

            app.Run(window);
        }
    }
}
