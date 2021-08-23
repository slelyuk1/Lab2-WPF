using System.Windows;
using AgeZodiacCalculator.Managers;
using AgeZodiacCalculator.Models;
using AgeZodiacCalculator.Windows;

namespace AgeZodiacCalculator
{
    // todo recall how it works
    public partial class App
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            var window = new ContentWindow();


            NavigationManager.Instance.Initialize(new NavigationModel(window));
            window.Show();
            NavigationManager.Instance.Navigate(Models.Views.PickData);
        }
    }
}