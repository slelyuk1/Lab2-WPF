using System;
using System.ComponentModel;
using AgeZodiacCalculator.Model;
using AgeZodiacCalculator.Model.Impl;
using AgeZodiacCalculator.ViewModel;

namespace AgeZodiacCalculator.View
{
    public partial class PickDateView
    {
        public PickDateView(TypeConverter ageInfoConverter, TypeConverter chineseSignConverter, TypeConverter westernSignConverter)
        {
            InitializeComponent();


            IPickDataModel model = new ConverterBasedPickDataModel(DateTime.Now, chineseSignConverter, westernSignConverter);
            DataContext = new PickDateViewModel(model, ageInfoConverter, chineseSignConverter, westernSignConverter);
        }
    }
}