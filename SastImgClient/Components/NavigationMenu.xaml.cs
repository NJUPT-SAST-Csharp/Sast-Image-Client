using System.Collections.Frozen;
using System.Linq;
using Microsoft.UI.Xaml.Controls;
using SastImgClient.Infrastructure;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace SastImgClient.Components
{
    internal sealed partial class NavigationMenu : UserControl
    {
        private readonly FrozenSet<NavigationViewItem> _items;

        public NavigationMenu(INavigator navigation)
        {
            Navigation = navigation;
            this.InitializeComponent();

            _items = NavView
                .MenuItems.Union(NavView.FooterMenuItems)
                .Cast<NavigationViewItem>()
                .ToFrozenSet();

            NavView.SelectedItem = _items.First(
                i => (i.Tag as string) == Navigation.CurrentPage.Key
            );
        }

        public INavigator Navigation { get; }

        private void NavigatePageTo(
            NavigationView sender,
            NavigationViewSelectionChangedEventArgs args
        )
        {
            var item = args.SelectedItem as NavigationViewItem;

            Navigation.NavigateTo(item.Tag as string);
        }

        private void NavigatePageBack(
            NavigationView sender,
            NavigationViewBackRequestedEventArgs args
        )
        {
            Navigation.Back();
            NavView.SelectedItem = _items.First(
                i => (i.Tag as string) == Navigation.CurrentPage.Key
            );
        }
    }
}
