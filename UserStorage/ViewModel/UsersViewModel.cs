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
        private ObservableCollection<PersonInfo> _users;
        private ObservableCollection<string> _properties;
        public ICommand AddCommand { get; }
        public ICommand DeleteCommand { get; }
        public ICommand EditCommand { get; }
        public ICommand FilterCommand { get; }

        public UsersViewModel(IViewNavigator<Type> navigator, Storage data)
        {
            _navigator = navigator;
            Data = data;
            Model = new UsersModel(data);
            _users = new ObservableCollection<PersonInfo>(data.Users);
            UsersCollectionView = CollectionViewSource.GetDefaultView(_users);
            FilterProperties = new ObservableCollection<string> {"All", "Name", "Surname", "Email", "SunSign", "ChineseSign"};

            AddCommand = new DelegateBasedCommand(OpenUsersChangerInAddMode);
            DeleteCommand = new DelegateBasedCommand(ExecuteDelete, UserChosen);
            EditCommand = new DelegateBasedCommand(OpenUsersChangerInEditMode, UserChosen);
            FilterCommand = new DelegateBasedCommand(ExecuteFilter);
            
            data.UserAdded += AddUser;
            data.UserEdited += EditUser;
            data.UserDeleted += DeleteUser;
        }

        public UsersModel Model { get; set; }
        public Storage Data { get; }

        public ObservableCollection<PersonInfo> Users
        {
            get => _users;
            set
            {
                _users = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<string> FilterProperties
        {
            get => _properties;
            set
            {
                _properties = value;
                OnPropertyChanged();
            }
        }

        public string FilterText { get; set; }
        public string SelectedProperty { get; set; }

        public PersonInfo SelectedUser
        {
            set => Model.ChosenPersonInfo = value;
        }

        //property defined for future
        public int SelectedIndex { get; set; }

        public ICollectionView UsersCollectionView { get; set; }

        // todo method for implementing in future
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
            UsersCollectionView.Filter = item =>
                Model.FilterPredicate((PersonInfo) item, FilterText, SelectedProperty);
        }

        private void ExecuteDelete(object obj)
        {
            Data.DeleteUser(Model.ChosenPersonInfo);
        }

        private bool UserChosen(object obj)
        {
            return Model.IsUserChosen;
        }

        private void AddUser(PersonInfo newUser)
        {
            _users.Add(newUser);
            Model.AddUser(newUser);
        }

        private void DeleteUser(PersonInfo toDelete)
        {
            int index = _users.IndexOf(toDelete);
            if (index == -1)
                throw new NullReferenceException("No such user in observable collection !");
            _users.Remove(_users[index]);
            Model.DeleteUser(toDelete);
        }

        private void EditUser(PersonInfo edited)
        {
            Model.EditUser(edited);
            int index = _users.IndexOf(Model.ChosenPersonInfo);
            if (index == -1)
                throw new NullReferenceException("No such user in observable collection !");
            _users[index] = edited;
        }
    }
}