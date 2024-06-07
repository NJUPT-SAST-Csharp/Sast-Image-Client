using FoxNavigator.Pages;

namespace FoxNavigator.Behaviors
{
    public interface IBeforeNavigateBehavior<TSourcePage, TTargetPage> : IBeforeNavigatorBehavior
        where TTargetPage : IPageView
        where TSourcePage : IPageView
    {
        public void OnNavigating(TSourcePage from, TTargetPage to);
    }

    public interface IBeforeNavigatorBehavior
    {
        public bool CanNavigate { get; set; }
    }
}
