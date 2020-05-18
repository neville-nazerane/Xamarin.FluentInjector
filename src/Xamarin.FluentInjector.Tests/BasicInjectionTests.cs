using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Xamarin.FluentInjector.Tests
{
    public class BasicInjectionTests : TestBase
    {

        [Fact]
        public void Default()
        {

            app.StartInjecting()
                .Build();

        }

    }
}
