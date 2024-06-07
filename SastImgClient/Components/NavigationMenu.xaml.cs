using System.Collections.Frozen;
using System.Linq;
using FoxNavigator;
using FoxNavigator.Behaviors;
using Microsoft.UI.Xaml.Controls;
using SastImgClient.Pages.Main;
using SastImgClient.Pages.Settings;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace SastImgClient.Components
{
    internal sealed partial class NavigationMenu : UserControl
    {
        private readonly FrozenSet<NavigationViewItem> _items;

        private NavigationViewItem? _previousItem = null;

        internal SettingsPageVm Settings { get; }

        public NavigationMenu(INavigator navigation, SettingsPageVm settings)
        {
            Settings = settings;
            this.InitializeComponent();

            _items = NavView
                .MenuItems.Union(NavView.FooterMenuItems)
                .Cast<NavigationViewItem>()
                .ToFrozenSet();

            Navigation = navigation;
            Navigation.Initialize<MainPage>();
            Navigation.OnPageChanged += OnPageChanged;

            var item = _items.First(i => (i.Tag as string) == Navigation.CurrentPage.Key);
            NavView.SelectedItem = item;

            _previousItem = item;
        }

        public INavigator Navigation { get; }

        private void NavigatePageBack(
            NavigationView sender,
            NavigationViewBackRequestedEventArgs args
        )
        {
            Navigation.Back();
        }

        private void NavigationPageTo(
            NavigationView sender,
            NavigationViewItemInvokedEventArgs args
        )
        {
            if ((NavigationViewItem)sender.SelectedItem == _previousItem)
            {
                return;
            }

            var item = (NavigationViewItem)sender.SelectedItem;

            Navigation.NavigateTo((string)item.Tag);
        }

        void OnPageChanged(object? sender, PageChangedEventArgs args)
        {
            NavView.SelectedItem = _items.FirstOrDefault(i =>
                (i.Tag as string) == Navigation.CurrentPage.Key
            );

            _previousItem = (NavigationViewItem?)NavView.SelectedItem;
        }
    }
}
