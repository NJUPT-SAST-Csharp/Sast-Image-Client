using System;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using SastImgClient.Components;
using SastImgClient.Infrastructure;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace SastImgClient
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    public partial class App : Application
    {
        private readonly IServiceCollection _services = new ServiceCollection();

        /// <summary>
        /// Initializes the singleton application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public App()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Invoked when the application is launched.
        /// </summary>
        /// <param name="args">Details about the launch request and process.</param>
        protected override void OnLaunched(LaunchActivatedEventArgs args)
        {
            var services = ConfigureServices(_services);

            var pageGroup = services
                .GetServices<IPageView>()
                .GroupBy(page => page.Key)
                .FirstOrDefault(group => group.Count() > 1);

            if (pageGroup is not null)
            {
                throw new InvalidOperationException("Duplicated page key: " + pageGroup.Key);
            }

            services.GetRequiredService<MainWindow>().Activate();
        }

        private IServiceProvider ConfigureServices(IServiceCollection services)
        {
            var types = Assembly.GetAssembly(typeof(App))!.GetTypes();

            services.AddSingleton<MainWindow>();

            services.AddSingleton<INavigator, Navigator>();
            services.AddSingleton<NavigationMenu>();

            var pages = Array.FindAll(types, type => type.BaseType == typeof(Page));

            foreach (var page in pages)
            {
                var interfaceType =
                    Array.Find(
                        page.GetInterfaces(),
                        i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IPageView<>)
                    ) ?? throw new NullReferenceException();

                // IPageView
                services.AddSingleton(typeof(IPageView), page);
                services.AddKeyedSingleton(
                    typeof(IPageView),
                    page.Name,
                    (s, name) => s.GetServices<IPageView>().First(s => s.Key == (string)name!)
                );
                services.AddSingleton(page, s => s.GetRequiredKeyedService<IPageView>(page.Name));

                // IPageView<>
                services.AddSingleton(
                    interfaceType,
                    s =>
                        s.GetServices<IPageView>()
                            .First(p => p.GetType().IsAssignableTo(interfaceType))
                );

                // IPageViewModel
                var viewmodelType = interfaceType.GetGenericArguments()[0];
                services.AddSingleton(typeof(IPageViewModel), viewmodelType);
                services.AddSingleton(
                    viewmodelType,
                    s =>
                        s.GetServices<IPageViewModel>()
                            .First(p => p.GetType().IsAssignableTo(viewmodelType))
                );
            }

            return services.BuildServiceProvider();
        }
    }
}
