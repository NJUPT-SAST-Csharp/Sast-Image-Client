using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using FoxNavigator.Pages;
using SastImgClient.Core;

namespace SastImgClient.Pages.Images
{
    internal sealed partial class ImagesPageVm : ObservableObject, IPageViewModel
    {
        public ObservableCollection<ImageItem> Images { get; } = [];
    }
}
