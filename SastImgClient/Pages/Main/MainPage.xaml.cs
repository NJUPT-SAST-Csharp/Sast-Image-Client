// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

using Microsoft.UI.Xaml.Controls;
using SastImgClient.Infrastructure;
using SastImgClient.Pages.Video;

namespace SastImgClient.Pages.Main
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a _frame.
    /// </summary>
    internal sealed partial class MainPage : Page, IPageView<MainPageVm>
    {
        private readonly INavigator _navigator;

        public string Key { get; } = nameof(MainPage);

        public MainPageVm ViewModel { get; } = new();

        public MainPage(MainPageVm viewmodel, INavigator navigator)
        {
            ViewModel = viewmodel;
            _navigator = navigator;
            InitializeComponent();
        }

        private void Button_Click(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
        {
            _navigator.NavigateTo<VideoPage>();
        }
    }
}
