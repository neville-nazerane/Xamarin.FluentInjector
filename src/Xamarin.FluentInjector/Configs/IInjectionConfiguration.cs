using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.FluentInjector.Internals;
using Xamarin.Forms;

namespace Xamarin.FluentInjector.Configs
{
    public interface IInjectionConfiguration
    {
        Task NavigateAsync(Application app, Page page);
        Page ResolvePage<T>(Action<T> addData = null);
        Page ResolvePage(Type type);
        void SetupMainPage(Application app, Page initialPage);
        void SetViewModel(IPageProvider provider);
    }
}
