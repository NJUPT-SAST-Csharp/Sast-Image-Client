using Microsoft.UI.Xaml.Controls;
using SastImgClient.Infrastructure;

namespace SastImgClient.Pages.Settings
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    internal sealed partial class SettingsPage : Page, IPageView<SettingPageVm>
    {
        public string Key { get; } = nameof(SettingsPage);
        public SettingPageVm ViewModel { get; }

        public SettingsPage(SettingPageVm viewModel)
        {
            this.InitializeComponent();
            ViewModel = viewModel;
        }
    }
}
