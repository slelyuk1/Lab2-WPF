using System;
using System.Windows;
using System.Windows.Input;
using PeopleInfoStorage.Exception;
using PeopleInfoStorage.Model.Data;
using PeopleInfoStorage.Model.UI;
using PeopleInfoStorage.View;
using Shared.Tool.ViewModel;
using Shared.View.Navigator;

namespace PeopleInfoStorage.ViewModel
{
    internal enum ProcessingMode
    {
        Add,
        Edit,
        View
    }

    public class PersonInputViewModel : ObservableItem
    {
        private static readonly PeopleInputModel DummyModel = new();

        private ProcessingMode _processingMode;
        private readonly IViewNavigator<Type> _navigator;
        private PeopleInputModel? _model;

        public PersonInputViewModel(IViewNavigator<Type> navigator)
        {
            _navigator = navigator;
            _processingMode = ProcessingMode.Add;
            ProcessCommand = new DelegateBasedCommand(ExecuteProcess);
        }

        private PeopleInputModel Model
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
            Model = new PeopleInputModel(toEdit);
            _processingMode = ProcessingMode.Edit;
        }

        public void PrepareForInput()
        {
            Model = new PeopleInputModel();
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
                        _navigator.ExecuteAndNavigate<PeopleViewModel>(typeof(PeopleView), viewModel =>
                            viewModel.AddPerson(newPersonInfo)
                        );
                        break;
                    case ProcessingMode.Edit:
                        _navigator.ExecuteAndNavigate<PeopleViewModel>(typeof(PeopleView), viewModel =>
                            viewModel.ReplaceSelectedPerson(newPersonInfo)
                        );
                        break;
                    default:
                        throw new NotImplementedException("This EditMode is still not implemented!");
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