using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Xamarin.FluentInjector
{
    public static class FluentInjectingExtensions
    {

        public static InjectionBuilder StartInjecting(this Application app) => new InjectionBuilder(app);

    }
}
