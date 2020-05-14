using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Xamarin.FluentInjector
{
    public class InjectionBuilder
    {
        private readonly Application _app;
        private readonly ServiceCollection _services;
        private Assembly pageAssembly;
        private Assembly viewModelAssembly;
        private Page defaultPage;
        private bool shouldSetDefaultPage = true;

        internal InjectionBuilder(Application app)
        {
            _app = app;
            _services = new ServiceCollection();
            pageAssembly = _app.GetType().Assembly;
            viewModelAssembly = _app.GetType().Assembly;
            InjectionControl.navigationAction = p => _app.MainPage = p;
            InjectionControl.asyncNavigationFunc = p =>
            {
                _app.MainPage = p;
                return Task.CompletedTask;
            };
        }

        // ??? no clue why I did this!
        //public InjectionBuilder(object app)
        //{
        //    _services = new ServiceCollection();
        //    pageAssembly = app.GetType().Assembly;
        //    viewModelAssembly = app.GetType().Assembly;
        //}

        #region adding singleton

        public InjectionBuilder AddSingleton<TService>(Func<IServiceProvider, TService> implementationFactory) 
            where TService : class
        {
            _services.AddSingleton(implementationFactory);
            return this;
        }
        
        public InjectionBuilder AddSingleton<TService>(TService service)
            where TService : class
        {
            _services.AddSingleton(service);
            return this;
        }

        public InjectionBuilder AddSingleton<TService, TImplementation>()
            where TService : class
            where TImplementation : class, TService
        {
            _services.AddSingleton<TService, TImplementation>();
            return this;
        }
        
        public InjectionBuilder AddSingleton(Type serviceType)
        {
            _services.AddSingleton(serviceType);
            return this;
        }

        public InjectionBuilder AddSingleton<TService>()
            where TService : class
        {
            _services.AddSingleton<TService>();
            return this;
        }

        #endregion

        #region adding transients

        public InjectionBuilder AddTransient<TService>(Func<IServiceProvider, TService> implementationFactory)
                where TService : class
        {
            _services.AddTransient(implementationFactory);
            return this;
        }

        public InjectionBuilder AddTransient<TService>()
            where TService : class
        {
            _services.AddTransient<TService>();
            return this;
        }

        public InjectionBuilder AddTransient<TService, TImplementation>()
            where TService : class
            where TImplementation : class, TService
        {
            _services.AddTransient<TService, TImplementation>();
            return this;
        }

        public InjectionBuilder AddTransient(Type serviceType)
        {
            _services.AddTransient(serviceType);
            return this;
        }

        #endregion

        #region other services

        public InjectionBuilder AddHttpClient<TClient>(Action<HttpClient> action)
            where TClient : class
        {
            _services.AddHttpClient<TClient>(action);
            return this;
        }

        public InjectionBuilder AddHttpClient<TClient, TImplementation>(Action<HttpClient> action)
            where TClient : class
            where TImplementation : class, TClient
        {
            _services.AddHttpClient<TClient, TImplementation>(action);
            return this;
        }


        #endregion

        #region override assemblies
        public InjectionBuilder SetPageAssembly(Assembly assembly)
        {
            pageAssembly = assembly;
            return this;
        }
        
        public InjectionBuilder SetViewModelAssembly(Assembly assembly)
        {
            viewModelAssembly = assembly;
            return this;
        }


        #endregion

        #region override pages

        public InjectionBuilder SetDefaultPage(Page page)
        {
            defaultPage = page;
            return this;
        }

        public InjectionBuilder DisableSettingDefaultPage()
        {
            shouldSetDefaultPage = false;
            return this;
        }

        #endregion

        #region navigation override 
        public InjectionBuilder OverrideNavigate(Action<Page> action)
        {
            InjectionControl.navigationAction = action;
            return this;
        }

        public InjectionBuilder OverrideAsyncNavigate(Func<Page, Task> func)
        {
            InjectionControl.asyncNavigationFunc = func;
            return this;
        }

        #endregion

        public void Build()
        {

            var types = pageAssembly.GetTypes();
            var filtered = types.Where(t => typeof(Page).IsAssignableFrom(t));
            Dictionary<string, Type> pages = new Dictionary<string, Type>();

            // fetching pages
            foreach (Type page in pageAssembly.GetTypes().Where(t => typeof(Page).IsAssignableFrom(t)))
            {
                string name = page.Name;
                if (name.EndsWith("page", StringComparison.InvariantCultureIgnoreCase) || name.EndsWith("view", StringComparison.InvariantCultureIgnoreCase))
                    name = name.Substring(0, name.Length - 4);
                if (pages.ContainsKey(name))
                    throw new AmbiguousMatchException($"The page name '{name}' is used more than once.");
                pages[name] = page;

                _services.AddTransient(page);
            }


            // fetching view models
           var viewModels = viewModelAssembly.GetTypes()
                                   .Where(t => t.Name.EndsWith("viewmodel", StringComparison.InvariantCultureIgnoreCase)
                                               || t.Name.EndsWith("pagemodel", StringComparison.InvariantCultureIgnoreCase));
            int count = viewModels.Count();
            foreach (Type vm in viewModels)
            {
                _services.AddScoped(vm);

                string pageName = vm.Name.Substring(0, vm.Name.Length - 9);
                if (!pages.ContainsKey(pageName))
                    throw new KeyNotFoundException($"No matching page with name '{pageName}' found for view model ${vm.Name}");

                Type controlService = typeof(IPageControl<>).MakeGenericType(vm);
                Type controlImplimentation = typeof(PageControl<,>).MakeGenericType(pages[pageName], vm);
                _services.AddScoped(controlService, controlImplimentation);

            }
            InjectionControl._services = _services;
            InjectionControl._provider = _services.BuildServiceProvider();
            if (shouldSetDefaultPage)
            {
                if (defaultPage == null)
                {
                    // check for viewmodel with name "main"
                    Type foundViewModel = viewModels.SingleOrDefault(v => v.Name.Equals("mainviewmodel", StringComparison.InvariantCultureIgnoreCase)
                                                                        || v.Name.Equals("mainpagemodel", StringComparison.InvariantCultureIgnoreCase));
                    if (foundViewModel != null)
                        InjectionControl.Navigate(foundViewModel);
                    else
                    {
                        if (pages.ContainsKey("Main"))
                            InjectionControl.navigationAction?.Invoke((Page)InjectionControl.Resolve(pages["Main"]));
                    }
                }
                else _app.MainPage = defaultPage;
            }
        }

    }
}
