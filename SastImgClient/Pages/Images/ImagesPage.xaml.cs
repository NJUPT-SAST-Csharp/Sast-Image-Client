using Microsoft.UI.Xaml.Controls;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace SastImgClient.Pages.Images
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    internal sealed partial class ImagesPage : Page, IPageView<ImagesPageVm>
    {
        public string Key { get; } = nameof(ImagesPage);
        public ImagesPageVm ViewModel { get; } = new();

        public ImagesPage(ImagesPageVm viewModel)
        {
            this.InitializeComponent();
            ViewModel = viewModel;
        }
    }
}
