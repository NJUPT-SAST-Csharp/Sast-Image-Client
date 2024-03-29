﻿using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using SastImgClient.Components;
using SastImgClient.Infrastructure;
using SastImgClient.Pages;

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
        private Window m_window;

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
            ConfigureServices();
            var provider = _services.BuildServiceProvider();

            m_window = new MainWindow(provider.GetRequiredService<NavigationMenu>());

            m_window.Activate();
        }

        private void ConfigureServices()
        {
            _services.AddSingleton<INavigator, Navigator>();

            _services.AddSingleton<LoginPageVm>();
            _services.AddSingleton<MainPageVm>();

            _services.AddSingleton<Frame>();
            _services.AddSingleton<NavigationMenu>();
            _services.AddSingleton<IPageView, LoginPage>();
            _services.AddSingleton<IPageView, MainPage>();
        }
    }
}
