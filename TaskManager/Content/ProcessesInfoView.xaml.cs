using System.Windows.Controls;
using TaskManager.ViewModels;

namespace TaskManager.Content
{
    public partial class ProcessesInfoView 
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