using System.Windows.Controls;
using UserStorage.Models;
using UserStorage.ViewModels;

namespace UserStorage.Views
{
    /// <summary>
    /// Interaction logic for UsersView.xaml
    /// </summary>
    public partial class UsersView : UserControl
    {
        private UsersViewModel _model;

        public UsersView(Storage data)
        {
            InitializeComponent();

            _model = new UsersViewModel(data);
            DataContext = _model;
        }

        private void OnUsersSorting(object sender, DataGridSortingEventArgs e)
        {
            _model.UsersSorting(sender, e);
        }
    }
}