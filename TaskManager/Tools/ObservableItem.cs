﻿using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace TaskManager.Tools
{
    class ObservableItem : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
