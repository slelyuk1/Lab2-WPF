using System.Windows.Controls;
using Lab2_Leliuk.Models;
using Lab2_Leliuk.ViewModels;

namespace Lab2_Leliuk.Views
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