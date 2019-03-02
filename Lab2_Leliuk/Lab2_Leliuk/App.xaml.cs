using System.Windows;
using Lab2_Leliuk.Managers;
using Lab2_Leliuk.Models;

namespace Lab2_Leliuk
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