using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Xamarin.FluentInjector.Providers
{

    class PageViewModelProvider<T, TPage, TViewModel> : IPageProvider<T>
        where TPage : Page
    {

        private readonly Lazy<object> _viewModel;

        public Page Page { get; }

        public object ViewModel => _viewModel.Value;


        public PageViewModelProvider(TPage page, IServiceProvider serviceProvider)
        {
            Page = page;
            _viewModel = new Lazy<object>(() => serviceProvider.GetService<TViewModel>());
        }
    }



}
