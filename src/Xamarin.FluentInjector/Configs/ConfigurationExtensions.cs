using System;
using System.Collections.Generic;
using System.Text;

namespace Xamarin.FluentInjector.Configs
{

    internal static class ConfigurationExtensions
    {

        internal static bool IsExternallyDefined(this IInjectionConfiguration configuration)
            => configuration is InternalInjectionConfiguration;

    }
}
