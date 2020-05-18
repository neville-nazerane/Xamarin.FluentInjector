using Moq;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Xamarin.FluentInjector.Tests.Fakes;
using Xunit;

namespace Xamarin.FluentInjector.Tests
{
    public class BasicInjectionTests
    {
        private readonly Mock<IApplicationConnect> _mockApp;
        private readonly IApplicationConnect _app;

        public BasicInjectionTests()
        {
            _mockApp = new Mock<IApplicationConnect>();
            _app = _mockApp.Object;

            _mockApp.SetupGet(a => a.ApplicationAssembly).Returns(GetType().Assembly);
            _mockApp.SetupProperty(a => a.MainPage);
        }

        [Fact]
        public void Default()
        {

            new InjectionBuilder(_app).Build();

            Assert.NotNull(_app.MainPage);
            Assert.IsType<MainPage>(_app.MainPage);

        }

        [Fact]
        public async Task SingletonInjection()
        {

            new InjectionBuilder(_app)
                .AddSingleton<SingletonToInject>()
                .Build();

            await InjectionControl.NavigateAsync<UsingInjectingViewModel>();

            Assert.IsType<UsingInjectingPage>(_app.MainPage);
            Assert.IsType<UsingInjectingViewModel>(_app.MainPage.BindingContext);

        }

    }
}
