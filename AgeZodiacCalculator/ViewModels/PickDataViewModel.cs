using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using AgeZodiacCalculator.Annotations;
using System.Windows.Input;
using AgeZodiacCalculator.Models;
using AgeZodiacCalculator.Tools;

namespace AgeZodiacCalculator.ViewModels
{
    class PickDataViewModel : ObservableItem
    {
        private DateTime _selectedDate;
        private string _westernZodiac;
        private string _chineseZodiac;
        private string _age;

        public PickDataModel Model { get; private set; }

        private ICommand _calculateDateCommand;
        private ICommand _addBirthdayCommand;

        public PickDataViewModel()
        {
            Model = new PickDataModel();
            _selectedDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
        }


        public DateTime SelectedDate
        {
            get => _selectedDate;
            set
            {
                _selectedDate = value;
                OnPropertyChanged("SelectedDate");
            }
        }

        public string ChineseZodiac
        {
            get => _chineseZodiac;
            set
            {
                _chineseZodiac = value;
                OnPropertyChanged("ChineseZodiac");
            }
        }

        public string WesternZodiac
        {
            get => _westernZodiac;
            set
            {
                _westernZodiac = value;
                OnPropertyChanged("WesternZodiac");
            }
        }

        public string Age
        {
            get => _age;
            set
            {
                _age = value;
                OnPropertyChanged("Age");
            }
        }

        public ICommand CalculateDateCommand
        {
            get
            {
                if (_calculateDateCommand == null)
                    _calculateDateCommand = new RelayCommand(CalulateExecute, CanCalculate);
                return _calculateDateCommand;
            }
        }

        public ICommand AddBirthdayCommand
        {
            get
            {
                if (_addBirthdayCommand == null)
                    _addBirthdayCommand = new RelayCommand(CalulateExecute, CanCalculate);
                return _calculateDateCommand;
            }
        }

        public void CalulateExecute(object obj)
        {
            try
            {
                Age = Model.CalculateAge(SelectedDate);
                ChineseZodiac = Model.CalculateChineseSign(SelectedDate);
                WesternZodiac = Model.CalculateWesternSign(SelectedDate);
            }
            catch (ArgumentException e)
            {
                MessageBox.Show("Incorrect input of date !","Error");
                Age = "Unknown";
                ChineseZodiac = "Unknown";
                WesternZodiac = "Unknown";
            }
        }

        public bool CanCalculate(object obj)
        {
            if (SelectedDate.Day <= DateTime.DaysInMonth(SelectedDate.Year, SelectedDate.Month))
                return true;
            MessageBox.Show("Such date does not exist !","Error");
            return false;
        }
    }
}