using System;
using System.ComponentModel;
using System.Windows.Input;
using AgeZodiacCalculator.Info;
using AgeZodiacCalculator.Model;
using Shared.Tool;

namespace AgeZodiacCalculator.ViewModel
{
    internal class PickDateViewModel : ObservableItem
    {
        private IPickDateModel Model { get; }
        private readonly TypeConverter _ageInfoConverter;
        private readonly TypeConverter _chineseSignConverter;
        private readonly TypeConverter _westernSignConverter;

        private string _ageProperty = "";
        private string _chineseSignProperty = "";
        private string _westernSignProperty = "";

        public ICommand CalculateCommand { get; }

        public PickDateViewModel(IPickDateModel model,
            TypeConverter ageInfoConverter,
            TypeConverter chineseSignConverter,
            TypeConverter westernSignConverter)
        {
            Model = model;
            _ageInfoConverter = ageInfoConverter;
            _chineseSignConverter = chineseSignConverter;
            _westernSignConverter = westernSignConverter;
            CalculateCommand = new DelegateBasedCommand(CalculateExecute, CanCalculate);
        }

        public DateTime SelectedDate
        {
            get => Model.SelectedDate;
            set
            {
                Model.SelectedDate = value;
                OnPropertyChanged();
            }
        }

        public string AgeProperty
        {
            get => _ageProperty;
            set
            {
                _ageProperty = value;
                OnPropertyChanged();
            }
        }

        public string WesternSignProperty
        {
            get => _westernSignProperty;
            set
            {
                _westernSignProperty = value;
                OnPropertyChanged();
            }
        }

        public string ChineseSignProperty
        {
            get => _chineseSignProperty;
            set
            {
                _chineseSignProperty = value;
                OnPropertyChanged();
            }
        }

        private void CalculateExecute(object obj)
        {
            AgeInfo? ageInfo = Model.CalculateAge();
            ChineseSign? chineseSign = Model.CalculateChineseSign();
            WesternSign? westernSign = Model.CalculateWesternSign();

            // todo think about logical decisions
            AgeProperty = ageInfo != null ? _ageInfoConverter.ConvertToString(ageInfo) ?? "Unknown" : "Unknown";
            ChineseSignProperty = chineseSign != null ? _chineseSignConverter.ConvertToString(chineseSign) ?? "Unknown" : "Unknown";
            WesternSignProperty = westernSign != null ? _westernSignConverter.ConvertToString(westernSign) ?? "Unknown" : "Unknown";
        }

        private bool CanCalculate(object obj)
        {
            if (SelectedDate.Day <= DateTime.DaysInMonth(SelectedDate.Year, SelectedDate.Month))
            {
                return true;
            }

            SelectedDate = DateTime.Now;
            return false;
        }
    }
}