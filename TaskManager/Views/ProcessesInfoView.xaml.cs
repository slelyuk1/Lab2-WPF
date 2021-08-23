using System.Windows.Controls;
using TaskManager.ViewModels;

namespace TaskManager.Views
{
    public partial class ProcessesInfoView : UserControl
    {
        private ProcessesInfoViewModel Model { get; }

        public ProcessesInfoView()
        {
            InitializeComponent();
            Model = new ProcessesInfoViewModel();
            DataContext = Model;
        }
    }
}