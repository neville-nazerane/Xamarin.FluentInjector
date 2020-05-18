using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;

namespace Xamarin.FluentInjector
{
    class LazyViewModel<TViewModel>
    {
        private readonly Lazy<TViewModel> _viewModel;

        public TViewModel ViewModel => _viewModel.Value;

        public LazyViewModel(IServiceProvider serviceProvider)
        {
            _viewModel = new Lazy<TViewModel>(() => serviceProvider.GetService<TViewModel>());
        }

    }
}
