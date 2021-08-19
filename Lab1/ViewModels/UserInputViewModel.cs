using System;
using System.Windows;
using System.Windows.Input;
using Lab1.Managers;
using Lab1.Models;
using Lab1.Tools;

namespace Lab1.ViewModels
{
    class UserInputViewModel : ObservableItem
    {
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
                    _processCommand = new RelayCommand(ExecuteProcess, CanProcess);
                return _processCommand;
            }
        }

        private void ExecuteProcess(object obj)
        {
            Model.SetUser(_name, _surname, _email, _selectedDate);
            if (Model.IsBirthDay())
                MessageBox.Show("Wow, it's your birthday today. Congratulations !", "Birthday");
            NavigationManager.Instance.Navigate(Models.Views.UserInfoView);
        }

        private bool CanProcess(object obj)
        {
            if (!Model.IsNameValid(_name))
                return false;
            if (!Model.IsSurnameValid(_surname))
                return false;
            if (!Model.IsEmailValid(_email))
                return false;
            if (!Model.IsDateValid(_selectedDate))
                return false;
            return true;
        }
    }
}