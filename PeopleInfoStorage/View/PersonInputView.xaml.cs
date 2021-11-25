using PeopleInfoStorage.ViewModel;

namespace PeopleInfoStorage.View
{
    public partial class PersonInputView
    {
        public PersonInputView(PersonInputViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
        
    }
}