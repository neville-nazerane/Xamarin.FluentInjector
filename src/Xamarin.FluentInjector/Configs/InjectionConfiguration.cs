using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.FluentInjector.Internals;
using Xamarin.FluentInjector.Utilities;
using Microsoft.Extensions.DependencyInjection;
using Xamarin.Forms;
using System.Threading.Tasks;

namespace Xamarin.FluentInjector.Configs
{

    internal class InternalInjectionConfiguration : InjectionConfiguration { }

    public class InjectionConfiguration : IInjectionConfiguration
    {

        //public Page InitialPage { get; internal set; }

        internal IApplicationConnect _app;
        internal IServiceProvider _provider;

        public virtual void SetViewModel(IPageProvider provider) => provider.Page.BindingContext = provider.ViewModel;

        public virtual Page ResolvePage<T>(Action<T> addData = null)
        {
            void preViewModelBinding(IPageProvider pageProvider)
            {
                if (addData != null)
                {
                    if (typeof(T) == pageProvider.Page.GetType())
                        // LOL! casting to obj so compiler will allow casting to T
                        addData((T)(object)pageProvider.Page);

                    else
                        addData((T)pageProvider.ViewModel);
                }
            }
            return MakePageProvider(provider => {
                return provider.GetService<IPageProvider<T>>();
            }, preViewModelBinding);
        }

        public virtual Page ResolvePage(Type type)
        {
            return MakePageProvider(provider => {
                var controlType = typeof(IPageProvider<>).MakeGenericType(type);
                return (IPageProvider)provider.GetService(controlType);
            });
        }

        public virtual Task NavigateAsync(Application app, Page page)
        {
            app.MainPage = page;
            return Task.CompletedTask;
        }

        public virtual void SetupMainPage(Application app, Page initialPage) => _app.MainPage = initialPage;

        private Page MakePageProvider(Func<IServiceProvider, IPageProvider> buildPageProvider, Action<IPageProvider> preViewModelBinding = null)
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
                SetViewModel(pageProvider);
            }
            return pageProvider.Page;
        }

    }
}
