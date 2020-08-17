using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Xamarin.FluentInjector.Utilities
{
    public interface IInjectionControl
    {
        Task<Page> NavigateAsync<T>(Action<T> addData = null);
        Page ResolvePage<T>(Action<T> addData = null);
    }
}
