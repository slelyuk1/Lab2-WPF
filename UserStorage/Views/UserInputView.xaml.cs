using System.Windows.Controls;
using UserStorage.Models;
using UserStorage.ViewModels;

namespace UserStorage.Views
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