using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Xamarin.FluentInjector
{
    internal class PageControl<TPage, TViewModel> : IPageControl<TViewModel>
        where TPage : Page
    {
        public Page Page { get; }
        public TViewModel ViewModel { get; }

        public PageControl(TPage page, TViewModel viewModel)
        {
            Page = page;
            ViewModel = viewModel;

            Page.BindingContext = ViewModel;
            if (ViewModel is InjectorViewModelBase vm)
                vm.CurrentPage = page;
        }

    }
}
