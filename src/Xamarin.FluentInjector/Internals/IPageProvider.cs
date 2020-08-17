using Xamarin.Forms;

namespace Xamarin.FluentInjector.Internals
{
    interface IPageProvider<T> : IPageProvider
    {

    }

    public interface IPageProvider
    {
        Page Page { get; }
        object ViewModel { get; }
    }

}