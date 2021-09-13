using System.Windows.Controls;
using UserStorage.ViewModel;

namespace UserStorage.Content
{
    public partial class UsersContent
    {
        private readonly UsersViewModel _viewModel;

        public UsersContent(UsersViewModel viewModel)
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