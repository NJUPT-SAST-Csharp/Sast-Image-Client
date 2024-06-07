using CommunityToolkit.Mvvm.ComponentModel;
using FoxNavigator.Pages;
using Microsoft.UI.Xaml;
using Windows.Storage;

namespace SastImgClient.Pages.Settings
{
    internal sealed partial class SettingsPageVm : ObservableObject, IPageViewModel
    {
        public SettingsPageVm() { }

        [ObservableProperty]
        private ElementTheme _currentTheme = ElementTheme.Default;

        partial void OnCurrentThemeChanged(ElementTheme value)
        {
            ApplicationData.Current.LocalSettings.Values["Theme"] = value.ToString();
        }
    }
}
