using Xamarin.Forms;

namespace Xamarin.FluentInjector.Providers
{
    interface IPageProvider<T>
    {
        Page Page { get; }
        object ViewModel { get; }
    }
}