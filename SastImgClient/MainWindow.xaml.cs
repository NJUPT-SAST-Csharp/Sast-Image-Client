using System.Linq;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using SastImgClient.Infrastructure;
using SastImgClient.Pages;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace SastImgClient
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    internal sealed partial class MainWindow : Window
    {
        public MainWindow(MainWindowVm vm, INavigator navigation)
        {
            ViewModel = vm;
            Navigation = navigation;
            Navigation.InitializePage<MainPage>();

            this.InitializeComponent();
        }

        internal INavigator Navigation { get; }
        internal MainWindowVm ViewModel { get; }

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

            sender.SelectedItem = sender
                .MenuItems.Union(sender.FooterMenuItems)
                .Cast<NavigationViewItem>()
                .First(item => item.Tag as string == Navigation.CurrentPage.Key);
        }
    }
}
