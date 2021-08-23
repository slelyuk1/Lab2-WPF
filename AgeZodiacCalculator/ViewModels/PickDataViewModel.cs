using System;
using System.Windows;
using System.Windows.Input;
using AgeZodiacCalculator.Models;
using AgeZodiacCalculator.Tools;

namespace AgeZodiacCalculator.ViewModels
{
    public class PickDataViewModel : ObservableItem
    {
        private DateTime _selectedDate;

        // todo why not used
        private string _westernZodiac;

        private string _chineseZodiac;

        // todo why not used
        private string _age;

        private PickDataModel Model { get; }

        private ICommand _calculateDateCommand;
        private ICommand _addBirthdayCommand;

        public PickDataViewModel()
        {
            // todo maybe DI
            Model = new PickDataModel();
            _selectedDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
        }

        // todo configure binding
        public DateTime SelectedDate
        {
            get => _selectedDate;
            set
            {
                _selectedDate = value;
                // todo use constant
                OnPropertyChanged("SelectedDate");
            }
        }

        // todo configure binding
        public string ChineseZodiac
        {
            get => _chineseZodiac;
            set
            {
                _chineseZodiac = value;
                OnPropertyChanged("ChineseZodiac");
            }
        }

        // todo configure binding
        public string WesternZodiac
        {
            get => _westernZodiac;
            set
            {
                _westernZodiac = value;
                // todo use constant
                OnPropertyChanged("WesternZodiac");
            }
        }

        // todo configure binding
        public string Age
        {
            get => _age;
            set
            {
                _age = value;
                // todo use constant
                OnPropertyChanged("Age");
            }
        }

        // todo why this exists?
        public ICommand CalculateDateCommand
        {
            get
            {
                if (_calculateDateCommand == null)
                    _calculateDateCommand = new RelayCommand(CalculateExecute, CanCalculate);
                return _calculateDateCommand;
            }
        }

        // todo why this exists?
        public ICommand AddBirthdayCommand
        {
            get
            {
                if (_addBirthdayCommand == null)
                    _addBirthdayCommand = new RelayCommand(CalculateExecute, CanCalculate);
                return _calculateDateCommand;
            }
        }

        private void CalculateExecute(object obj)
        {
            try
            {
                Age = Model.CalculateAge(SelectedDate);
                ChineseZodiac = Model.CalculateChineseSign(SelectedDate);
                WesternZodiac = Model.CalculateWesternSign(SelectedDate);
            }
            catch (ArgumentException)
            {
                MessageBox.Show("Incorrect input of date !", "Error");
                // todo make these non-string
                Age = "Unknown";
                ChineseZodiac = "Unknown";
                WesternZodiac = "Unknown";
            }
        }

        private bool CanCalculate(object obj)
        {
            if (SelectedDate.Day <= DateTime.DaysInMonth(SelectedDate.Year, SelectedDate.Month))
                return true;
            MessageBox.Show("Such date does not exist !", "Error");
            return false;
        }
    }
}