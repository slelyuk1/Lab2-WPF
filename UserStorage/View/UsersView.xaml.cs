using System.Windows.Controls;
using UserStorage.ViewModel;

namespace UserStorage.Content
{
    public partial class UsersView
    {
        private readonly UsersViewModel _viewModel;

        public UsersView(UsersViewModel viewModel)
        {
            InitializeComponent();
            _viewModel = viewModel;
            DataContext = _viewModel;
        }

        private void OnUsersSorting(object sender, DataGridSortingEventArgs e)
        {
            _viewModel.UsersSorting(sender, e);
        }
    }
}