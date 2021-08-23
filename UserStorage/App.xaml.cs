using System.Windows;
using UserStorage.Managers;
using UserStorage.Models;

namespace UserStorage
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            // todo fix serialization/deserialization process
            Storage data = new Storage("..\\..\\SerializedData\\users.bin");
            MainWindow mainWindow = new MainWindow(data);
            NavigationManager.Instance.Initialise(new NavigationModel(mainWindow, data));
            NavigationManager.Instance.Navigate(Models.Views.UsersView);
            mainWindow.Show();
        }
    }
}