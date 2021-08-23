using System;
using System.Windows;
using System.Windows.Input;
using UserStorage.Managers;
using UserStorage.Models;
using UserStorage.Tools;

namespace UserStorage.ViewModels
{
    public enum UserEditMode
    {
        Add,
        Edit
    }

    public class UserInputViewModel : ObservableItem
    {
        public static UserEditMode EditMode = UserEditMode.Add;
        private string _name, _surname, _email;
        private DateTime _selectedDate;

        private ICommand _processCommand;

        public UserInputViewModel(Storage data)
        {
            _name = "Oleksandr";
            _surname = "Leliuk";
            _email = "slelyuk1@gmail.com";
            _selectedDate = new DateTime(2000, 2, 13);
            Model = new UserInputModel(data);

            data.UserChosen += UiUserSet;
        }


        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged();
            }
        }

        public string Surname
        {
            get => _surname;
            set
            {
                _surname = value;
                OnPropertyChanged();
            }
        }

        public string Email
        {
            get => _email;
            set
            {
                _email = value;
                OnPropertyChanged();
            }
        }

        public DateTime SelectedDate
        {
            get => _selectedDate;
            set
            {
                _selectedDate = value;
                OnPropertyChanged();
            }
        }

        public UserInputModel Model { get; private set; }

        public ICommand ProcessCommand
        {
            get
            {
                if (_processCommand == null)
                    _processCommand = new RelayCommand(ExecuteProcess);
                return _processCommand;
            }
        }

        private void ExecuteProcess(object obj)
        {
            try
            {
                switch (EditMode)
                {
                    case UserEditMode.Add:
                        Model.AddUser(Name, Surname, Email, SelectedDate);
                        break;
                    case UserEditMode.Edit:
                        Model.EditUser(Model.ChosenUser, Name, Surname, Email, SelectedDate);
                        break;
                    default:
                        throw new NotImplementedException("This UserEditMode is still not implemented !");
                }


                if (Model.IsBirthDay())
                    MessageBox.Show("Wow, it's your birthday today. Congratulations !", "Birthday");
                NavigationManager.Instance.Navigate(Models.Views.UsersView);
            }
            catch (PersonException ex)
            {
                MessageBox.Show(ex.Message, "Error !");
            }
        }

        private void UiUserSet(Person user)
        {
            if (user == null)
            {
                Name = "";
                Surname = "";
                Email = "";
                SelectedDate = DateTime.Now;
            }
            else
            {
                Name = user.Name;
                Surname = user.Surname;
                Email = user.Email;
                SelectedDate = user.BirthDate;
            }
        }
    }
}