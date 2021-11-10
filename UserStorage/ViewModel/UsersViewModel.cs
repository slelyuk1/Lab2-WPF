using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using Shared.Tool;
using Shared.View.Navigator;
using UserStorage.Content;
using UserStorage.Models;

namespace UserStorage.ViewModel
{
    public class UsersViewModel : ObservableItem
    {
        private readonly IViewNavigator<Type> _navigator;
        private readonly UsersModel _model;
        private readonly Storage _data;
        private readonly ICollectionView _usersView;

        public ObservableCollection<PersonInfo> Users { get; }
        public ObservableCollection<string> FilterProperties { get; }
        public string FilterText { get; set; }

        // todo make non null
        public string? SelectedProperty { get; set; }

        public ICommand AddCommand { get; }
        public ICommand DeleteCommand { get; }
        public ICommand EditCommand { get; }
        public ICommand FilterCommand { get; }

        public UsersViewModel(IViewNavigator<Type> navigator, Storage data)
        {
            _navigator = navigator;
            _data = data;
            _model = new UsersModel(data);

            FilterText = "";

            Users = new ObservableCollection<PersonInfo>(data.Users);
            AddCommand = new DelegateBasedCommand(OpenUsersChangerInAddMode);
            DeleteCommand = new DelegateBasedCommand(ExecuteDelete, UserChosen);
            EditCommand = new DelegateBasedCommand(OpenUsersChangerInEditMode, UserChosen);
            FilterCommand = new DelegateBasedCommand(ExecuteFilter);

            // todo maybe remove view usage
            _usersView = new CollectionView(Users);
            // todo improve filter properties initialization process
            FilterProperties = new ObservableCollection<string> {"All", "Name", "Surname", "Email", "SunSign", "ChineseSign"};

            data.UserAdded += AddUser;
            data.UserEdited += EditUser;
            data.UserDeleted += DeleteUser;
        }


        public PersonInfo SelectedUser
        {
            set => _model.ChosenPersonInfo = value;
        }


        // todo implement
        public void UsersSorting(object sender, DataGridSortingEventArgs e)
        {
            /* bool ascending = e.Column.SortDirection == ListSortDirection.Ascending;
             var sorted = ascending ? Users.OrderBy(u => u.Name) : Users.OrderByDescending(u => u.Name);
             Users = new ObservableCollection<Person>(sorted);
            */
        }

        private void OpenUsersChangerInAddMode(object obj)
        {
            UserInputViewModel.EditMode = UserEditMode.Add;
            _navigator.Navigate(typeof(UserInputContent));
        }

        private void OpenUsersChangerInEditMode(object obj)
        {
            UserInputViewModel.EditMode = UserEditMode.Edit;
            _navigator.Navigate(typeof(UserInputContent));
        }

        private void ExecuteFilter(object obj)
        {
            _usersView.Filter = item => FilterPredicate((PersonInfo) item, FilterText, SelectedProperty);
        }

        private void ExecuteDelete(object obj)
        {
            _data.DeleteUser(_model.ChosenPersonInfo);
        }

        private bool UserChosen(object obj)
        {
            return _model.IsUserChosen;
        }

        private void AddUser(PersonInfo newUser)
        {
            Users.Add(newUser);
            _model.AddUser(newUser);
        }

        private void DeleteUser(PersonInfo toDelete)
        {
            int index = Users.IndexOf(toDelete);
            if (index == -1)
            {
                throw new NullReferenceException("No such user in observable collection !");
            }

            Users.Remove(Users[index]);
            _model.DeleteUser(toDelete);
        }

        private void EditUser(PersonInfo edited)
        {
            _model.EditUser(edited);
            int index = Users.IndexOf(_model.ChosenPersonInfo);
            if (index == -1)
            {
                throw new NullReferenceException("No such user in observable collection !");
            }

            Users[index] = edited;
        }

        private static bool FilterPredicate(PersonInfo user, string filter, string property)
        {
            filter = filter.ToLower();
            if (property == "All")
            {
                var name = user.Name.ToLower();
                var surname = user.Surname.ToLower();
                var email = user.Email.ToLower();
                var sunSign = user.SunSign.ToLower();
                var chineseSign = user.ChineseSign.ToLower();
                return name.Contains(filter) || surname.Contains(filter) || email.Contains(filter) ||
                       email.Contains(filter) || sunSign.Contains(filter) || chineseSign.Contains(filter);
            }

            var userProperty = user.GetType().GetProperty(property);
            if (userProperty == null)
                throw new ArgumentException("Inappropriate property for user !");
            var propertyVal = userProperty.GetValue(user, null);

            if (propertyVal is string s)
            {
                return s.ToLower().Contains(filter);
            }

            throw new ArgumentException("Inappropriate property type for filtering");
        }
    }
}