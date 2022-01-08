using TaskManager.ViewModel;

namespace TaskManager.View
{
    public partial class ProcessesInfoView
    {
        public ProcessesInfoView(ProcessesInfoViewModel model)
        {
            DataContext = model;
            InitializeComponent();
        }
    }
}