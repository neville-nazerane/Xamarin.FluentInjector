using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Xamarin.FluentInjector
{
    public class InjectorViewModelBase : INotifyPropertyChanged // : BindableObject
    {

        internal Page CurrentPage { get; set; }

        public bool IsBusy { get => CurrentPage.IsBusy; set => CurrentPage.IsBusy = value; }

        public event PropertyChangedEventHandler PropertyChanged;

        public virtual Page Navigate<TViewModel>() => InjectionControl.Navigate<TViewModel>();

        public virtual async Task<Page> NavigateAsync<TViewModel>() => await InjectionControl.NavigateAsync<TViewModel>();

        public async Task<string> DisplayActionSheet(string title, string cancel, string destruction, params string[] buttons) 
            => await CurrentPage.DisplayActionSheet(title, cancel, destruction, buttons);

        public async Task DisplayAlert(string title, string message, string cancel) => await CurrentPage.DisplayAlert(title, message, cancel);

        public async Task<bool> DisplayAlert(string title, string message, string accept, string cancel) => await CurrentPage.DisplayAlert(title, message, accept, cancel);

        public async Task<string> DisplayPromptAsync(string title, string message, string accept = "OK", string cancel = "Cancel", string placeholder = null, int maxLength = -1, Keyboard keyboard = null, string initialValue = null) 
            => await CurrentPage.DisplayPromptAsync(title, message, accept, cancel, placeholder, maxLength, keyboard, initialValue);

        protected bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
        {
            if (Equals(storage, value))
                return false;

            storage = value;
            OnPropertyChanged(propertyName);
            return true;
        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
