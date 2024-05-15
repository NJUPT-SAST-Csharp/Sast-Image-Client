using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using SastImgClient.Core;
using SastImgClient.Infrastructure;

namespace SastImgClient.Pages.Images
{
    internal sealed partial class ImagesPageVm : ObservableObject, IPageViewModel
    {
        public ObservableCollection<ImageItem> Images { get; } = [];
    }
}
