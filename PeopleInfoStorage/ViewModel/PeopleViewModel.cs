using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Windows.Data;
using System.Windows.Input;
using Microsoft.Extensions.Logging;
using PeopleInfoStorage.Model.Data;
using PeopleInfoStorage.Model.UI;
using PeopleInfoStorage.View;
using Shared.ComponentModel.Adapter;
using Shared.Model.Data;
using Shared.Tool.View.Navigator;
using Shared.Tool.ViewModel;

namespace PeopleInfoStorage.ViewModel
{
    public class PeopleViewModel : ObservableItem
    {
        private const string AllFilterProperty = "All";

        private static readonly string[] DefaultFilterProperties =
        {
            AllFilterProperty,
            nameof(PersonInfo.Name),
            nameof(PersonInfo.Surname),
            nameof(PersonInfo.Email),
            nameof(PersonInfo.WesternSign),
            nameof(PersonInfo.ChineseSign)
        };

        private readonly PeopleModel _model;
        private readonly IViewNavigator _navigator;
        private readonly TypeConverter _chineseSignConverter;
        private readonly TypeConverter _westernSignConverter;
        private readonly ILogger _logger;

        private string _filterText;

        public PeopleViewModel(
            IViewNavigator navigator,
            PeopleModel model,
            ILogger<PeopleViewModel> logger
        )
        {
            _navigator = navigator;
            _model = model;
            _logger = logger;
            _chineseSignConverter = TypeDescriptor.GetConverter(typeof(ChineseSign));
            _westernSignConverter = TypeDescriptor.GetConverter(typeof(WesternSign));

            _filterText = "";

            People = _model.People;
            PeopleView = new GenericCollectionViewAdapter<PersonInfo>(CollectionViewSource.GetDefaultView(People));
            FilterProperties = new List<string>(DefaultFilterProperties);
            SelectedProperty = FilterProperties[0];

            AddCommand = new DelegateBasedCommand(OpenInputForAdd);
            DeleteCommand = new DelegateBasedCommand(DeleteSelectedPerson, _ => _model.IsPersonChosen);
            EditCommand = new DelegateBasedCommand(OpenInputForEdit, _ => _model.IsPersonChosen);
        }

        public ObservableCollection<PersonInfo> People { get; }
        public GenericCollectionViewAdapter<PersonInfo> PeopleView { get; }
        public IList<string> FilterProperties { get; }

        public string FilterText
        {
            get => _filterText;
            set
            {
                _filterText = value;
                RunFilter(_filterText);
            }
        }

        public string SelectedProperty { get; set; }

        public ICommand AddCommand { get; }
        public ICommand DeleteCommand { get; }
        public ICommand EditCommand { get; }

        public PersonInfo? SelectedPerson
        {
            get => _model.ChosenPerson;
            set => _model.ChosenPerson = value;
        }

        public void AddPerson(PersonInfo info)
        {
            _model.AddPerson(info);
        }

        public void ReplaceSelectedPerson(PersonInfo with)
        {
            _model.EditPerson(with);
        }

        private void DeleteSelectedPerson(object obj)
        {
            _model.DeleteSelectedPerson();
        }

        private void OpenInputForAdd(object obj)
        {
            _navigator.ExecuteAndNavigate<PersonInputView, PersonInputViewModel>((_, viewModel) => viewModel.PrepareForInput());
        }

        private void OpenInputForEdit(object obj)
        {
            if (SelectedPerson == null)
            {
                _logger.LogWarning("Tried to open input for edit with null SelectedPerson");
                return;
            }

            _navigator.ExecuteAndNavigate<PersonInputView, PersonInputViewModel>((_, viewModel) => viewModel.PrepareForEdit(SelectedPerson));
        }

        private void RunFilter(string filterText)
        {
            Type filteredType = typeof(PersonInfo);

            PropertyInfo?[] filteredProperties = SelectedProperty == AllFilterProperty
                ? filteredType.GetProperties()
                : new[] {filteredType.GetProperty(SelectedProperty)};

            PeopleView.Filter = item =>
            {
                IEnumerable<string> matchedValues =
                    from stringValue in
                        from value in
                            from property in
                                filteredProperties
                            where property != null
                            select property.GetValue(item, null)
                        where value != null
                        select value switch
                        {
                            ChineseSign chineseSign => _chineseSignConverter.ConvertToString(chineseSign),
                            WesternSign westernSign => _westernSignConverter.ConvertToString(westernSign),
                            _ => value.ToString()
                        }
                    where stringValue != null && stringValue.ToLower().Contains(filterText.ToLower())
                    select stringValue;

                return matchedValues.Any();
            };
        }
    }
}