namespace FoxNavigator.Pages
{
    public interface ISubPageView : IPageView { }

    public interface ISubPageView<TViewModel> : IPageView<TViewModel>
        where TViewModel : IPageViewModel { }
}
