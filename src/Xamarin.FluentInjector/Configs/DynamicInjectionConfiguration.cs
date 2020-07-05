using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Xamarin.FluentInjector.Internals;
using Xamarin.Forms;

namespace Xamarin.FluentInjector.Configs
{
    internal class DynamicInjectionConfiguration : IInjectionConfiguration
    {

        internal IInjectionConfiguration Source { get; set; }

        public DynamicInjectionConfiguration()
        {
            Source = new InternalInjectionConfiguration();

            navigateAsync = Source.NavigateAsync;
            setupMainPage = Source.SetupMainPage;
            setViewModel = Source.SetViewModel;
        }

        internal Func<Application, Page, Task> navigateAsync;
        public Task NavigateAsync(Application app, Page page) => navigateAsync(app, page);

        public Page ResolvePage<T>(Action<T> addData = null) => Source.ResolvePage(addData);

        public Page ResolvePage(Type type) => Source.ResolvePage(type);

        internal Action<Application, Page> setupMainPage;
        public void SetupMainPage(Application app, Page page) => setupMainPage(app, page);

        internal Action<IPageProvider> setViewModel;
        public void SetViewModel(IPageProvider provider) => setViewModel(provider);

    }
}
