using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Xamarin.FluentInjector
{
    class PageViewModelStore<T> : PageViewModelStore
    {
        internal PageViewModelStore(Page page, Lazy<object> viewModel) : base(page, viewModel)
        {
        }
    }

    class PageViewModelStore
    {
        private readonly Lazy<object> _viewModel;

        internal Page Page { get; }

        internal object ViewModel => _viewModel.Value;

        internal PageViewModelStore(Page page, Lazy<object> viewModel)
        {
            Page = page;
            _viewModel = viewModel;
        }
    }
}
