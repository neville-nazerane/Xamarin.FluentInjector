using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.FluentInjector.Configs;
using Xamarin.FluentInjector.Utilities;
using Xamarin.Forms;

namespace Xamarin.FluentInjector.Internals
{
    internal class InjectionControl : IInjectionControl
    {
        private readonly IInjectionConfiguration _configuration;

        public InjectionControl(IInjectionConfiguration configuration)
        {
            _configuration = configuration;
        }

        public Page ResolvePage<T>(Action<T> addData = null) => _configuration.ResolvePage(addData);

        public async Task<Page> NavigateAsync<T>(Action<T> addData = null)
        {
            var page = _configuration.ResolvePage(addData);
            await _configuration.NavigateAsync(_configuration.App, page);
            return page;
        }

    }
}
