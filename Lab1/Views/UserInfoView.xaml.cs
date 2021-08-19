using System.Windows.Controls;
using Lab1.Models;
using Lab1.ViewModels;

namespace Lab1.Views
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