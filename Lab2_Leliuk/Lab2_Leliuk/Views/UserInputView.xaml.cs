using System.Windows.Controls;
using Lab2_Leliuk.Models;
using Lab2_Leliuk.ViewModels;

namespace Lab2_Leliuk.Views
{
    /// <summary>
    /// Interaction logic for UserInputView.xaml
    /// </summary>
    public partial class UserInputView : UserControl
    {
        private UserInputViewModel _model;

        public UserInputView(Storage data)
        {
            InitializeComponent();

            _model = new UserInputViewModel(data);
            DataContext = _model;
        }
    }
}