using System;
using System.Runtime.InteropServices;
using FoxNavigator.Pages;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Windows.Storage;

namespace SastImgClient.Pages.Settings
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    internal sealed partial class SettingsPage : Page, IPageView<SettingsPageVm>
    {
        public string Key { get; } = nameof(SettingsPage);
        public SettingsPageVm ViewModel { get; }

        public SettingsPage(SettingsPageVm viewModel, SettingsPageVm settings)
        {
            this.InitializeComponent();
            ViewModel = viewModel;
            Settings = settings;
        }

        internal SettingsPageVm Settings { get; }

        private void ThemeChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count == 0 || e.AddedItems[0] is null)
                return;
            string value = Buttons.SelectedItem.ToString() ?? string.Empty;
            bool result = Enum.TryParse(value, out ElementTheme theme);
            if (result == false)
            {
                theme = ShouldSystemUseDarkMode() ? ElementTheme.Dark : ElementTheme.Light;
            }
            if (theme != Settings.CurrentTheme)
                Settings.CurrentTheme = theme;

            ApplicationData.Current.LocalSettings.Values["Theme"] = value;
        }

        [DllImport("UXTheme.dll", SetLastError = true, EntryPoint = "#138")]
        public static extern bool ShouldSystemUseDarkMode();
    }
}
