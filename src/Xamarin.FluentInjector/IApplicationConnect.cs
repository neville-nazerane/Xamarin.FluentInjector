using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Xamarin.Forms;

namespace Xamarin.FluentInjector
{
    public interface IApplicationConnect
    {

        Application Source { get; }

        Page MainPage { get; set; }

        Assembly ApplicationAssembly { get; }

    }
}
