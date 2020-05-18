using System;
using System.Collections.Generic;
using System.Text;

namespace Xamarin.FluentInjector.Tests.Fakes
{
    class UsingInjectingViewModel
    {

        public UsingInjectingViewModel(SingletonToInject inject)
        {
            Inject = inject;
        }

        public SingletonToInject Inject { get; }
    }
}
