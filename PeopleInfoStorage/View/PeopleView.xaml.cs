using PeopleInfoStorage.ViewModel;

namespace PeopleInfoStorage.View
{
    public partial class PeopleView
    {
        public PeopleView(PeopleViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}