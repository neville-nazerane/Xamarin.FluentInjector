using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Xamarin.FluentInjector.Providers
{

    /// <summary>
    /// This needs to be used when the page has no view model. 
    /// The view model property in this case would be null
    /// </summary>
    /// <typeparam name="TPage"></typeparam>
    class PageProvider<TPage> : IPageProvider<TPage>
        where TPage : Page
    {

        public Page Page { get; }
        public object ViewModel { get; }

        public PageProvider(TPage page)
        {
            Page = page;
        }


    }
}
