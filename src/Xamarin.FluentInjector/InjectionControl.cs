using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Xamarin.FluentInjector.Controls;
using Xamarin.FluentInjector.Providers;
using Xamarin.Forms;

namespace Xamarin.FluentInjector
{
    public static class InjectionControl
    {

        internal static IServiceProvider _provider;
        internal static IServiceCollection _services;

        internal static Action<Page> navigationAction;
        internal static Func<Page, Task> asyncNavigationFunc;

        public static T Resolve<T>() => _provider.GetService<T>();
        public static object Resolve(Type type) => _provider.GetService(type);

        public static Page ResolvePage<T>()
        {
            return MakePageProvider(provider => { 
                return provider.GetService<IPageProvider<T>>();
            });
        }

        internal static Page ResolvePage(Type type)
        {
            return MakePageProvider(provider => { 
                var controlType = typeof(IPageProvider<>).MakeGenericType(type);
                return (IPageProvider)provider.GetService(controlType);
            });
        }

        private static Page MakePageProvider(Func<IServiceProvider, IPageProvider> buildPageProvider)
        {
            var scope = _provider.CreateScope();
            var provider = scope.ServiceProvider;
            var pageProvider = buildPageProvider(provider);

            // below casting would fail if unit testing mocks IPageControl
            if (provider.GetService<IPageControl>() is PageControl pageControl)
            {
                pageControl._page = pageProvider.Page;
            }

            if (pageProvider.ViewModel != null)
            {
                pageProvider.Page.BindingContext = pageProvider.ViewModel;
            }
            return pageProvider.Page;
        }

        internal static void Setup(IPageProvider provider)
        {
            // set as current page here
            provider.Page.BindingContext = provider.ViewModel;
        }

        public static Page Navigate<T>()
        {
            Page page = ResolvePage<T>();
            navigationAction?.Invoke(page);
            return page;
        } 

        internal static Page Navigate(Type viewModel)
        {
            Page page = ResolvePage(viewModel);
            navigationAction?.Invoke(page);
            return page;
        }

        public static async Task<Page> NavigateAsync<T>()
        {
            Page page = ResolvePage<T>();
            if (asyncNavigationFunc != null)
                await asyncNavigationFunc(page);
            return page;
        }

    }
}
