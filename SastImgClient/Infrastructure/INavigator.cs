using System.Collections.Generic;
using System.ComponentModel;

namespace SastImgClient.Infrastructure
{
    internal interface INavigator : INotifyPropertyChanged
    {
        public void Initialize(string pageName);
        public void Initialize<T>()
            where T : IPageView;

        public bool NavigateTo(string pageName);
        public bool NavigateTo<T>()
            where T : IPageView;

        public void Back();
        public void Forward();
        public void ClearNavigationHistory();

        public IReadOnlyCollection<IPageView> Pages { get; }
        public IPageView CurrentPage { get; }

        public bool CanBack { get; }
        public bool CanForward { get; }

        public event PageChangedEventHandler? OnPageChanged;
    }
}
