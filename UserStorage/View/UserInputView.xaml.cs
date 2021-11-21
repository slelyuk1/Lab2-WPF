using System.Windows;
using UserStorage.ViewModel;

namespace UserStorage.View
{
    public partial class UserInputView
    {
        public UserInputView(UserInputViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
        
    }
}