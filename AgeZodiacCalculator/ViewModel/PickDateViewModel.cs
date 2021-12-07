using System;
using AgeZodiacCalculator.Model.Data;
using AgeZodiacCalculator.Model.UI;
using Microsoft.Extensions.Logging;
using Shared.Model.Data;
using Shared.Tool.ViewModel;

namespace AgeZodiacCalculator.ViewModel
{
    public class PickDateViewModel : ObservableItem
    {
        private readonly ILogger<PickDateViewModel> _logger;
        private readonly IPickDateModel _model;

        public PickDateViewModel(ILogger<PickDateViewModel> logger, IPickDateModel model)
        {
            _logger = logger;
            _model = model;
        }

        public DateTime SelectedDate
        {
            get => _model.SelectedDate;
            set
            {
                _logger.LogTrace("Selected date is changing from {PreviousDate} to {SelectedDate}", _model.SelectedDate, value);
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