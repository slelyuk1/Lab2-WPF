using System.ComponentModel;
using System.Runtime.CompilerServices;

// todo recall what happens here
namespace AgeZodiacCalculator.Tools
{
    public class ObservableItem : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}