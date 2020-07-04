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
            public Application Source { get; }

            public Page MainPage { get => Source.MainPage; set => Source.MainPage = value; }
            public Assembly ApplicationAssembly => Source.GetType().Assembly;


            public ApplicationConnect(Application app)
            {
                Source = app;
            }

        }

    }
}
