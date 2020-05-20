using Xamarin.Forms;

namespace Xamarin.FluentInjector.Providers
{
    interface IPageProvider<T> : IPageProvider
    {

    }

    interface IPageProvider
    {
        Page Page { get; }
        object ViewModel { get; }
    }

}