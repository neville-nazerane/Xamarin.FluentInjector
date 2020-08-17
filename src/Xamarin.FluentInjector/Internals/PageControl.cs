using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.FluentInjector.Utilities;
using Xamarin.Forms;

namespace Xamarin.FluentInjector.Internals
{
    class PageControl : IPageControl
    {

        internal Page _page;

        public object Page => _page;

        public Task<string> DisplayActionSheet(string title, string cancel, string destruction, params string[] buttons)
            => _page.DisplayActionSheet(title, cancel, destruction, buttons);

        public Task DisplayAlert(string title, string message, string cancel)
            => _page.DisplayAlert(title, message, cancel);

        public Task<bool> DisplayAlert(string title, string message, string accept, string cancel)
            => _page.DisplayAlert(title, message, accept, cancel);

        public Task<string> DisplayPromptAsync(string title, string message, string accept = "OK", string cancel = "Cancel", string placeholder = null, int maxLength = -1, Keyboard keyboard = null, string initialValue = "")
            => _page.DisplayPromptAsync(title, message, accept, cancel, placeholder, maxLength, keyboard, initialValue);


    }
}
