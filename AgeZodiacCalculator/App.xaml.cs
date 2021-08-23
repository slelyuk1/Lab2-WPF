using System.Windows;
using AgeZodiacCalculator.Managers;
using AgeZodiacCalculator.Models;

namespace AgeZodiacCalculator
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            ContentWindow window = new ContentWindow();


            NavigationManager.Instance.Initialise(new NavigationModel(window));
            window.Show();
            NavigationManager.Instance.Navigate(Models.Views.PickData);
        }
    }
}