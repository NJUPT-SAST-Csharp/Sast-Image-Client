using FoxNavigator.Pages;
using Microsoft.UI.Xaml.Controls;

namespace SastImgClient.Pages.Video
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    internal sealed partial class VideoPage : Page, ISubPageView<VideoPageVm>
    {
        public VideoPage(VideoPageVm viewmodel)
        {
            this.InitializeComponent();
            ViewModel = viewmodel;
        }

        public string Key { get; } = nameof(VideoPage);
        public VideoPageVm ViewModel { get; }
    }
}
