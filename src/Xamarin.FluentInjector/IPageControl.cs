using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Xamarin.FluentInjector
{
    interface IPageControl<TViewModel> : IPageProvider
    {
        TViewModel ViewModel { get; }
    }
}
