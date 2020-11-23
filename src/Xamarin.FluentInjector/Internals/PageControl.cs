using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.FluentInjector.Configs;
using Xamarin.FluentInjector.Utilities;
using Xamarin.Forms;

namespace Xamarin.FluentInjector.Internals
{
    class PageControl : IPageControl
    {

        internal Page _page;
        private readonly IInjectionConfiguration _configuration;

        public object Page => _page;

        public PageControl(IInjectionConfiguration configuration)
        {
            _configuration = configuration;
        }

        public Task<string> DisplayActionSheet(string title, string cancel, string destruction, params string[] buttons)
            => _page.DisplayActionSheet(title, cancel, destruction, buttons);

        public Task DisplayAlert(string title, string message, string cancel)
            => _page.DisplayAlert(title, message, cancel);

        public Task<bool> DisplayAlert(string title, string message, string accept, string cancel)
            => _page.DisplayAlert(title, message, accept, cancel);

        public Task<string> DisplayPromptAsync(string title, string message, string accept = "OK", string cancel = "Cancel", string placeholder = null, int maxLength = -1, Keyboard keyboard = null, string initialValue = "")
            => _page.DisplayPromptAsync(title, message, accept, cancel, placeholder, maxLength, keyboard, initialValue);



        public Page ResolvePage<T>(Action<T> addData = null) => _configuration.ResolvePage(addData);

        public async Task<Page> NavigateAsync<T>(Action<T> addData = null)
        {
            var page = _configuration.ResolvePage(addData);
            await _configuration.NavigateAsync(_configuration.App, page);
            return page;
        }

        public Task PopAsync() => _page.Navigation.PopAsync();
        public Task PopModalAsync() => _page.Navigation.PopModalAsync();
        public Task PopToRootAsync() => _page.Navigation.PopToRootAsync();

        public async Task<Page> PushModalAsync<T>(Action<T> addData = null)
        {
            var page = ResolvePage(addData);
            await _page.Navigation.PushModalAsync(page);
            return page;
        }

    }
}
