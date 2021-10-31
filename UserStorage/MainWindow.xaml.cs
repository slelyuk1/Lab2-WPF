using System.ComponentModel;
using System.Windows;
using Shared.View.Visualizer;
using UserStorage.Managers;
using UserStorage.Models;

namespace UserStorage
{
    public partial class MainWindow : IViewVisualizer
    {
        private readonly Storage _storage;
        private readonly AbstractSerializationFacade _serializationFacade;

        public MainWindow(Storage storage, AbstractSerializationFacade serializationFacade)
        {
            _storage = storage;
            _serializationFacade = serializationFacade;
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
            _serializationFacade.Serialize(App.StorageResourceName, _storage);
            base.OnClosing(e);
        }
    }
}