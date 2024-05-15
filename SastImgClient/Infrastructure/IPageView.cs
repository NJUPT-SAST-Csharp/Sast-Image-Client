namespace SastImgClient.Infrastructure
{
    public interface IPageView
    {
        public string Key { get; }
    }

    public interface IPageView<TViewModel> : IPageView
        where TViewModel : IPageViewModel
    {
        public TViewModel ViewModel { get; }
    }
}
