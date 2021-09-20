using System.ComponentModel;
using System.Windows;
using Shared.View;
using Shared.View.Visualizer;
using UserStorage.Managers;
using UserStorage.Models;

namespace UserStorage
{
    public partial class MainWindow : IViewVisualizer
    {
        private readonly Storage _data;

        public MainWindow(Storage data)
        {
            _data = data;
            InitializeComponent();
        }

        public void Visualize(FrameworkElement toVisualize)
        {
            MinHeight = toVisualize.MinHeight;
            MinWidth = toVisualize.MinWidth;
            WindowContents.Content = toVisualize;
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            SerializationManager.Serialise(_data.Users);
            base.OnClosing(e);
        }
    }
}