using UserStorage.ViewModel;

namespace UserStorage.Content
{
    public partial class UserInfoContent
    {
        public UserInfoContent(UserInfoViewModel model)
        {
            InitializeComponent();
            DataContext = model;
        }
    }
}