using System.Windows;
using Lab1.Managers;
using Lab1.Models;

namespace Lab1
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            MainWindow mainWindow = new MainWindow();
            Storage data = new Storage();
            NavigationManager.Instance.Initialise(new NavigationModel(mainWindow, data));
            NavigationManager.Instance.Navigate(Models.Views.UserInputView);
            mainWindow.Show();
        }
    }
}