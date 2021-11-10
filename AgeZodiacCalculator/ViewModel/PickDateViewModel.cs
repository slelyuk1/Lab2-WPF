using System;
using AgeZodiacCalculator.Info;
using AgeZodiacCalculator.Model;
using Shared.Tool;

namespace AgeZodiacCalculator.ViewModel
{
    internal class PickDateViewModel : ObservableItem
    {
        private readonly IPickDateModel _model;

        public PickDateViewModel(IPickDateModel model)
        {
            _model = model;
        }

        public DateTime SelectedDate
        {
            get => _model.SelectedDate;
            set
            {
                _model.SelectedDate = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(Age));
                OnPropertyChanged(nameof(ChineseSign));
                OnPropertyChanged(nameof(WesternSign));
            }
        }

        public AgeInfo? Age => _model.Age;

        public ChineseSign? ChineseSign => _model.ChineseSign;

        public WesternSign? WesternSign => _model.WesternSign;
    }
}