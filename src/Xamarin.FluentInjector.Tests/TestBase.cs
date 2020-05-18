using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Xamarin.FluentInjector.Tests
{
    public abstract class TestBase
    {

        protected Application app;

        public TestBase()
        {
            app = new Application();
        }

    }
}
