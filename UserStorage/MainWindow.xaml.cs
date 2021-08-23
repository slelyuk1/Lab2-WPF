using System.ComponentModel;
using System.Windows;
using UserStorage.Managers;
using UserStorage.Models;

namespace UserStorage
{
    public partial class MainWindow : Window
    {
        private Storage _data;

        public MainWindow(Storage data)
        {
            _data = data;
            InitializeComponent();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            SerializationManager.Serialise("..\\..\\SerializedData\\users.bin", _data.Users);
            base.OnClosing(e);
        }
    }
}