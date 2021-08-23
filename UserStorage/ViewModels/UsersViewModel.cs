using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using UserStorage.Managers;
using UserStorage.Models;
using UserStorage.Tools;

namespace UserStorage.ViewModels
{
    class UsersViewModel : ObservableItem
    {
        private ObservableCollection<Person> _users;
        private ObservableCollection<string> _properties;
        private ICommand _addCommand, _deleteCommand, _editCommand, _filterCommand;

        public UsersViewModel(Storage data)
        {
            Data = data;
            Model = new UsersModel(data);
            _users = new ObservableCollection<Person>(data.Users);
            UsersCollectionView = CollectionViewSource.GetDefaultView(_users);
            FilterProperties = new ObservableCollection<string>()
                {"All", "Name", "Surname", "Email", "SunSign", "ChineseSign"};

            data.UserAdded += AddUser;
            data.UserEdited += EditUser;
            data.UserDeleted += DeleteUser;
        }

        public UsersModel Model { get; set; }
        public Storage Data { get; }

        public ObservableCollection<Person> Users
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

        public Person SelectedUser
        {
            set => Model.ChosenPerson = value;
        }

        //property defined for future
        public int SelectedIndex { get; set; }

        public ICollectionView UsersCollectionView { get; set; }

        public ICommand AddCommand
        {
            get
            {
                if (_addCommand == null)
                {
                    _addCommand = new RelayCommand(OpenUsersChangerInAddMode);
                }

                return _addCommand;
            }
        }

        public ICommand DeleteCommand
        {
            get
            {
                if (_deleteCommand == null)
                {
                    _deleteCommand = new RelayCommand(ExecuteDelete, UserChosen);
                }

                return _deleteCommand;
            }
        }

        public ICommand EditCommand
        {
            get
            {
                if (_editCommand == null)
                {
                    _editCommand = new RelayCommand(OpenUsersChangerInEditMode, UserChosen);
                }

                return _editCommand;
            }
        }

        public ICommand FilterCommand
        {
            get
            {
                if (_filterCommand == null)
                {
                    _filterCommand = new RelayCommand(ExecuteFilter, null);
                }

                return _filterCommand;
            }
        }

        //method for implementing in future
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
            NavigationManager.Instance.Navigate(Models.Views.UserInputView);
        }

        private void OpenUsersChangerInEditMode(object obj)
        {
            UserInputViewModel.EditMode = UserEditMode.Edit;
            NavigationManager.Instance.Navigate(Models.Views.UserInputView);
        }

        private void ExecuteFilter(object obj)
        {
            UsersCollectionView.Filter = item =>
                Model.FilterPredicate((Person) item, FilterText, SelectedProperty);
        }

        private void ExecuteDelete(object obj)
        {
            Data.DeleteUser(Model.ChosenPerson);
        }

        private bool UserChosen(object obj)
        {
            return Model.IsUserChosen;
        }

        private void AddUser(Person newUser)
        {
            _users.Add(newUser);
            Model.AddUser(newUser);
        }

        private void DeleteUser(Person toDelete)
        {
            int index = _users.IndexOf(toDelete);
            if (index == -1)
                throw new NullReferenceException("No such user in observable collection !");
            _users.Remove(_users[index]);
            Model.DeleteUser(toDelete);
        }

        private void EditUser(Person edited)
        {
            Model.EditUser(edited);
            int index = _users.IndexOf(Model.ChosenPerson);
            if (index == -1)
                throw new NullReferenceException("No such user in observable collection !");
            _users[index] = edited;
        }
    }
}