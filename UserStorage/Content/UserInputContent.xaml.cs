using UserStorage.ViewModel;

namespace UserStorage.Content
{
    public partial class UserInputContent
    {
        public UserInputContent(UserInputViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}