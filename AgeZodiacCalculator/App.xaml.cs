using System.Windows;
using AgeZodiacCalculator.Manager;
using AgeZodiacCalculator.Model;
using AgeZodiacCalculator.Window;

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
            NavigationManager.Instance.Navigate(Model.View.PickData);
        }
    }
}