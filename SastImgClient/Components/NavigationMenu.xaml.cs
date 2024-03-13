using Microsoft.UI.Xaml.Controls;
using SastImgClient.Infrastructure;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace SastImgClient.Components
{
    internal sealed partial class NavigationMenu : UserControl
    {
        public NavigationMenu(INavigator navigation, Frame frame)
        {
            Navigation = navigation;

            this.InitializeComponent();

            NavView.Content = frame;
        }

        public INavigator Navigation { get; }

        private void NavigatePageTo(NavigationView sender, NavigationViewItemInvokedEventArgs args)
        {
            var item = sender.SelectedItem as NavigationViewItem;

            Navigation.NavigateTo(item.Tag as string);
        }

        private void NavigatePageBack(
            NavigationView sender,
            NavigationViewBackRequestedEventArgs args
        )
        {
            Navigation.Back();
        }
    }
}
