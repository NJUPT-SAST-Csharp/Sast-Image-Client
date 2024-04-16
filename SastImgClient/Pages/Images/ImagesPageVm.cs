using System;
using System.Collections.ObjectModel;
using System.IO;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SastImgClient.Core;

namespace SastImgClient.Pages.Images
{
    internal sealed partial class ImagesPageVm : ObservableObject, IPageViewModel
    {
        public ObservableCollection<ImageItem> Images { get; } = [];

        [RelayCommand]
        private void LoadImages()
        {
            var filePaths = Directory.GetFiles("D:\\Storage\\Pictures\\壁纸");

            Array.ForEach(
                filePaths,
                path =>
                    Images.Add(
                        new ImageItem
                        {
                            Title = Path.GetFileName(path),
                            Id = Random.Shared.Next(20),
                            Url = path
                        }
                    )
            );
        }

        [RelayCommand]
        private void ClearImages()
        {
            Images.Clear();
        }
    }
}
