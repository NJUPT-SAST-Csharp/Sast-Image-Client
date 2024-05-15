using Microsoft.UI.Xaml.Controls;
using SastImgClient.Infrastructure;

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
            Media.MediaPlayer.Volume = 0.5;
            ViewModel = viewmodel;
        }

        public string Key { get; } = nameof(VideoPage);
        public VideoPageVm ViewModel { get; }
    }
}
