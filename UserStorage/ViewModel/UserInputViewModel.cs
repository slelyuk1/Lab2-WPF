using System;
using System.Windows;
using System.Windows.Input;
using Shared.Tool;
using Shared.View.Navigator;
using UserStorage.Content;
using UserStorage.Exception;
using UserStorage.Models;
using ObservableItem = Shared.Tool.ObservableItem;

namespace UserStorage.ViewModel
{
    public enum UserEditMode
    {
        Add,
        Edit
    }

    public class UserInputViewModel : ObservableItem
    {
        public static UserEditMode EditMode = UserEditMode.Add;

        private readonly IViewNavigator<Type> _navigator;
        private string _name, _surname, _email;
        private DateTime _selectedDate;
        public ICommand ProcessCommand { get; }

        public UserInputViewModel(IViewNavigator<Type> navigator, Storage data)
        {
            _navigator = navigator;
            _name = "Oleksandr";
            _surname = "Leliuk";
            _email = "slelyuk1@gmail.com";
            _selectedDate = new DateTime(2000, 2, 13);
            Model = new UserInputModel(data);

            ProcessCommand = new DelegateBasedCommand(ExecuteProcess);
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
                {
                    MessageBox.Show("Wow, it's your birthday today. Congratulations !", "Birthday");
                }

                _navigator.Navigate(typeof(UsersContent));
            }
            catch (PersonException ex)
            {
                MessageBox.Show(ex.Message, "Error !");
            }
        }

        private void UiUserSet(PersonInfo user)
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