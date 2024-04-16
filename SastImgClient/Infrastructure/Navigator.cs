using System;
using System.Collections.Generic;
using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using SastImgClient.Pages;
using SastImgClient.Pages.Main;

namespace SastImgClient.Infrastructure
{
    internal sealed partial class Navigator : ObservableObject, INavigator
    {
        public Navigator(IEnumerable<IPageView> pages)
        {
            _pages = pages.ToList();

            var mainPage = pages.First(p => p.Key == nameof(MainPage));
            _currentPage = mainPage;
        }

        private readonly Stack<IPageView> _forwardPages = new();
        private readonly Stack<IPageView> _backPages = new();

        private readonly IReadOnlyList<IPageView> _pages;

        [ObservableProperty]
        private IPageView _currentPage;

        [ObservableProperty]
        private bool _canBack = false;

        [ObservableProperty]
        private bool _canForward = false;

        public IEnumerable<IPageView> Pages => _pages;

        public Action<IPageView, IPageView> OnNavigating { get; set; }
        public Action<IPageView, IPageView> OnNavigated { get; set; }

        public void Back()
        {
            if (_backPages.TryPop(out var page))
            {
                _forwardPages.Push(page);
                CurrentPage = page;

                CheckBackAndForward();
            }
        }

        public void Forward()
        {
            if (_forwardPages.TryPop(out var page))
            {
                _backPages.Push(CurrentPage);
                CurrentPage = page;

                CheckBackAndForward();
            }
        }

        public void NavigateTo<T>()
            where T : IPageView
        {
            var page = _pages.OfType<T>().First() as IPageView;

            if (CurrentPage == page)
            {
                return;
            }

            _backPages.Push(CurrentPage);
            _forwardPages.Clear();
            CurrentPage = page;

            CheckBackAndForward();
        }

        public void NavigateTo(string pageKey)
        {
            if (CurrentPage.Key == pageKey)
            {
                return;
            }

            var page = _pages.First(p => p.Key == pageKey);

            _backPages.Push(CurrentPage);
            _forwardPages.Clear();
            CurrentPage = page;

            CheckBackAndForward();
        }

        private void CheckBackAndForward()
        {
            CanForward = _forwardPages.Count > 0;
            CanBack = _backPages.Count > 0;
        }

        partial void OnCurrentPageChanging(IPageView oldValue, IPageView newValue)
        {
            OnNavigating?.Invoke(oldValue, newValue);
        }

        partial void OnCurrentPageChanged(IPageView oldValue, IPageView newValue)
        {
            OnNavigated?.Invoke(oldValue, newValue);
        }
    }
}
