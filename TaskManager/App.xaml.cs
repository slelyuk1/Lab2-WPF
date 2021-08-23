using System.Windows;
using TaskManager.Managers;
using TaskManager.Models;

namespace TaskManager
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            // todo fix exit code 500 (admin rights)
            base.OnStartup(e);
            MainWindow window = new MainWindow();
            NavigationManager.Instance.Initialise(new NavigationModel(window));
            NavigationManager.Instance.Navigate(Models.Views.ProcessesView);
            window.Show();
        }
    }
}