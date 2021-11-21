using System;
using System.Windows;
using System.Windows.Input;
using Shared.Tool;
using Shared.View.Navigator;
using UserStorage.Content;
using UserStorage.Exception;
using UserStorage.Models;

namespace UserStorage.ViewModel
{
    public enum ProcessingMode
    {
        Add,
        Edit,
        View
    }

    public class UserInputViewModel : ObservableItem
    {
        private static readonly UserInputModel DummyModel = new();

        private ProcessingMode _processingMode;
        private readonly IViewNavigator<Type> _navigator;
        private UserInputModel? _model;

        public UserInputViewModel(IViewNavigator<Type> navigator)
        {
            _navigator = navigator;
            _processingMode = ProcessingMode.Add;
            ProcessCommand = new DelegateBasedCommand(ExecuteProcess);
        }

        private UserInputModel Model
        {
            get => _model ?? DummyModel;
            set
            {
                _model = value;
                OnPropertyChanged(nameof(Name));
                OnPropertyChanged(nameof(Surname));
                OnPropertyChanged(nameof(Email));
                OnPropertyChanged(nameof(BirthDate));
            }
        }

        public ICommand ProcessCommand { get; }

        public void PrepareForEdit(PersonInfo toEdit)
        {
            Model = new UserInputModel(toEdit);
            _processingMode = ProcessingMode.Edit;
        }

        public void PrepareForInput()
        {
            Model = new UserInputModel();
            _processingMode = ProcessingMode.Add;
        }

        public string Name
        {
            get => Model.Name;
            set
            {
                Model.Name = value;
                OnPropertyChanged();
            }
        }

        public string Surname
        {
            get => Model.Surname;
            set
            {
                Model.Surname = value;
                OnPropertyChanged();
            }
        }

        public string Email
        {
            get => Model.Email;
            set
            {
                Model.Email = value;
                OnPropertyChanged();
            }
        }

        public DateTime BirthDate
        {
            get => Model.BirthDate;
            set
            {
                Model.BirthDate = value;
                OnPropertyChanged();
            }
        }

        private void ExecuteProcess(object obj)
        {
            try
            {
                PersonInfo newPersonInfo = Model.MakeInfo();
                switch (_processingMode)
                {
                    case ProcessingMode.Add:
                        _navigator.ExecuteAndNavigate<UsersViewModel>(typeof(UsersView), viewModel =>
                            viewModel.AddPerson(newPersonInfo)
                        );
                        break;
                    case ProcessingMode.Edit:
                        _navigator.ExecuteAndNavigate<UsersViewModel>(typeof(UsersView), viewModel =>
                            viewModel.EditPerson(newPersonInfo)
                        );
                        break;
                    default:
                        throw new NotImplementedException("This UserEditMode is still not implemented!");
                }

                if (newPersonInfo.IsBirthday)
                {
                    MessageBox.Show("Wow, it's your birthday today. Congratulations!", "Birthday");
                }
            }
            catch (PersonException ex)
            {
                MessageBox.Show(ex.Message, "Error!");
            }
        }
    }
}