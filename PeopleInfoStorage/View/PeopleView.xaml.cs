using System.Windows.Controls;
using PeopleInfoStorage.ViewModel;

namespace PeopleInfoStorage.View
{
    public partial class PeopleView
    {
        private readonly PeopleViewModel _viewModel;

        public PeopleView(PeopleViewModel viewModel)
        {
            InitializeComponent();
            _viewModel = viewModel;
            DataContext = _viewModel;
        }

        private void OnSorting(object sender, DataGridSortingEventArgs e)
        {
            _viewModel.Sort(sender, e);
        }
    }
}