using System.Windows.Controls;
using Lab1.Models;
using Lab1.ViewModels;

namespace Lab1.Views
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