using System.Windows;
using TaskManager.View;

namespace TaskManager
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            // todo logging
            // todo make safe type conversions
            // todo use LINQ

            // todo fix exit code 500 (admin rights)
            base.OnStartup(e);
            var window = new MainWindow();
            var processesInfoView = new ProcessesInfoView();
            window.Visualize(processesInfoView);
            window.Show();
        }
    }
}