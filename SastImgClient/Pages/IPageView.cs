using CommunityToolkit.Mvvm.ComponentModel;

namespace SastImgClient.Pages
{
    internal interface IPageView
    {
        public string Key { get; }
    }

    internal interface IPageView<TViewModel> : IPageView
        where TViewModel : ObservableObject, IPageViewModel
    {
        public TViewModel ViewModel { get; }
    }
}
