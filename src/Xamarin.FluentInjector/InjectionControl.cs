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

        #region page 

        internal static void Setup(IPageProvider provider)
        {
            // set as current page here
            provider.Page.BindingContext = provider.ViewModel;
        }
        
        internal static Page Navigate(Type viewModel)
        {
            Page page = ResolvePage(viewModel);
            navigationAction?.Invoke(page);
            return page;
        }

        /// <summary>
        /// Resolve a page from the DI container
        /// </summary>
        /// <typeparam name="TPage"></typeparam>
        /// <param name="addData">
        /// Will be called to add data into the page or viewmodel (whichever type of T is)
        /// before the viewmodel is bound to the page
        /// </param>
        /// <returns></returns>
        public static Page ResolvePage<T>(Action<T> addData = null)
        {
            void preViewModelBinding(IPageProvider pageProvider)
            {
                if (typeof(T).IsSubclassOf(typeof(Page)))
                {
                    // LOL! casting to obj so compiler will allow casting to T
                    addData?.Invoke((T)(object)pageProvider.Page);
                }
                else
                {
                    addData?.Invoke((T)pageProvider.ViewModel);
                }
            }
            return MakePageProvider(provider => { 
                return provider.GetService<IPageProvider<T>>();
            }, preViewModelBinding);
        }

        internal static Page ResolvePage(Type type)
        {
            return MakePageProvider(provider => { 
                var controlType = typeof(IPageProvider<>).MakeGenericType(type);
                return (IPageProvider)provider.GetService(controlType);
            });
        }

        private static Page MakePageProvider(Func<IServiceProvider, IPageProvider> buildPageProvider, Action<IPageProvider> preViewModelBinding = null)
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
                preViewModelBinding?.Invoke(pageProvider);
                pageProvider.Page.BindingContext = pageProvider.ViewModel;
            }
            return pageProvider.Page;
        }

        public static Page Navigate<T>(Action<T> addData = null)
        {
            Page page = ResolvePage(addData);
            navigationAction?.Invoke(page);
            return page;
        } 

        public static async Task<Page> NavigateAsync<T>(Action<T> addData = null)
        {
            Page page = ResolvePage(addData);
            if (asyncNavigationFunc != null)
                await asyncNavigationFunc(page);
            return page;
        }

        #endregion

    }
}
