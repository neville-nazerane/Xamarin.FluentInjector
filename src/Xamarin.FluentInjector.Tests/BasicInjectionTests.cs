using Moq;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Xamarin.FluentInjector.Tests.Fakes;
using Xunit;

namespace Xamarin.FluentInjector.Tests
{
    public class BasicInjectionTests
    {

        [Fact]
        public void Default()
        {
            var mockApp = new Mock<IApplicationConnect>();
            var app = mockApp.Object;

            mockApp.SetupGet(a => a.ApplicationAssembly).Returns(GetType().Assembly);
            mockApp.SetupProperty(a => a.MainPage);

            new InjectionBuilder(app).Build();

            Assert.NotNull(app.MainPage);
            Assert.Equal(typeof(MainPage), app.MainPage.GetType());

        }

    }
}
