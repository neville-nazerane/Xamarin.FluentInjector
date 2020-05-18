using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Xamarin.Forms;

namespace Xamarin.FluentInjector
{
    public static class FluentInjectingExtensions
    {

        public static InjectionBuilder StartInjecting(this Application app) 
                                            => new InjectionBuilder(new ApplicationConnect(app));

        class ApplicationConnect : IApplicationConnect
        {
            private readonly Application _app;

            public Page MainPage { get => _app.MainPage; set => _app.MainPage = value; }
            public Assembly ApplicationAssembly => _app.GetType().Assembly;

            public ApplicationConnect(Application app)
            {
                _app = app;
            }

        }

    }
}
