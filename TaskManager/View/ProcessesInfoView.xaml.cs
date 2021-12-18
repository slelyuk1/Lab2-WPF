using TaskManager.ViewModel;

namespace TaskManager.View
{
    public partial class ProcessesInfoView
    {
        public ProcessesInfoView()
        {
            InitializeComponent();
            DataContext = new ProcessesInfoViewModel();
        }
    }
}