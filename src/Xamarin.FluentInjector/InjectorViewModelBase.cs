using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Xamarin.FluentInjector
{

    public class InjectorViewModelBase : INotifyPropertyChanged
    {
        public IPageControl PageControl { get; }

        public event PropertyChangedEventHandler PropertyChanged;

        public InjectorViewModelBase(IPageControl pageControl)
        {
            PageControl = pageControl;
        }

        protected virtual bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
        {
            if (Equals(storage, value))
                return false;

            storage = value;
            OnPropertyChanged(propertyName);
            return true;
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
