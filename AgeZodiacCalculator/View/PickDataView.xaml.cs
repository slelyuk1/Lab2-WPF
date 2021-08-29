using System;
using System.ComponentModel;
using AgeZodiacCalculator.Info;
using AgeZodiacCalculator.Model;
using AgeZodiacCalculator.Model.Impl;
using AgeZodiacCalculator.ViewModel;

namespace AgeZodiacCalculator.View
{
    public partial class PickDataView
    {
        public PickDataView()
        {
            InitializeComponent();
            TypeConverter ageInfoConverter = TypeDescriptor.GetConverter(typeof(AgeInfo));
            TypeConverter chineseSignConverter = TypeDescriptor.GetConverter(typeof(ChineseSign));
            TypeConverter westernSignConverter = TypeDescriptor.GetConverter(typeof(WesternSign));

            IPickDataModel model = new ConverterBasedPickDataModel(DateTime.Now, chineseSignConverter, westernSignConverter);
            DataContext = new PickDateViewModel(model, ageInfoConverter, chineseSignConverter, westernSignConverter);
        }
    }
}