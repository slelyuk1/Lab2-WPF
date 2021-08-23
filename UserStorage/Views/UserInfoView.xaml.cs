using System.Windows.Controls;
using UserStorage.Models;
using UserStorage.ViewModels;

namespace UserStorage.Views
{
    /// <summary>
    /// Interaction logic for UserInfoView.xaml
    /// </summary>
    public partial class UserInfoView : UserControl
    {
        private UserInfoViewModel _model;

        public UserInfoView(Storage data)
        {
            InitializeComponent();
            _model = new UserInfoViewModel(data);
            DataContext = _model;
        }
    }
}