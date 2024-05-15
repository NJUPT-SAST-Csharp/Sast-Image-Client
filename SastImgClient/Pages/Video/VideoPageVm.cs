using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using SastImgClient.Core;
using SastImgClient.Infrastructure;

namespace SastImgClient.Pages.Video
{
    internal sealed partial class VideoPageVm : ObservableObject, IPageViewModel
    {
        public ObservableCollection<VideoItem> VideoList { get; } =
            [
                new(1, new(@"https://thumbsnap.com/i/c58fyyEE.mp4"), "Cook"),
                new(2, new(@"https://thumbsnap.com/i/RiDSjVNR.mp4"), "Mosquito"),
                new(3, new(@"https://thumbsnap.com/i/Tnuqx3Kr.mp4"), "Drive"),
                new(4, new(@"https://thumbsnap.com/i/rLR6sWJC.mp4"), "Gun")
            ];
    }
}
