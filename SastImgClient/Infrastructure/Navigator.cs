using System;
using System.Collections.Generic;
using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media.Animation;
using SastImgClient.Pages;

namespace SastImgClient.Infrastructure
{
    internal sealed partial class Navigator : ObservableObject, INavigator
    {
        public Navigator(IEnumerable<IPageView> pages, Frame frame)
        {
            _pages = pages.ToList();
            _frame = frame;

            var mainPage = pages.First(p => p.Key == nameof(MainPage));
            _currentPage = mainPage;
            _frame.Content = mainPage;
        }

        private readonly Frame _frame;

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

        public void Back()
        {
            if (_backPages.TryPop(out var page))
            {
                _forwardPages.Push(page);
                CurrentPage = page;

                _frame.Navigate(page.PageType, null, _frameTransitionInfo);

                CheckBackAndForward();
            }
        }

        public void Forward()
        {
            if (_forwardPages.TryPop(out var page))
            {
                _backPages.Push(CurrentPage);
                CurrentPage = page;

                _frame.Navigate(page.PageType, null, _frameTransitionInfo);

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

            _frame.Navigate(page.PageType, null, _frameTransitionInfo);

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

            _frame.Navigate(page.PageType, null, _frameTransitionInfo);

            CheckBackAndForward();
        }

        public void NavigateTo(Type pageType)
        {
            if (CurrentPage.PageType == pageType)
            {
                return;
            }

            var page = _pages.First(p => p.PageType == pageType);
            _backPages.Push(CurrentPage);
            _forwardPages.Clear();
            CurrentPage = page;

            _frame.Navigate(page.PageType, null, _frameTransitionInfo);

            CheckBackAndForward();
        }

        private void CheckBackAndForward()
        {
            CanForward = _forwardPages.Count > 0;
            CanBack = _backPages.Count > 0;
        }

        private void InitFrame()
        {
            _frame.Content = _pages.First(p => p.Key == nameof(MainPage));
            _frame.IsNavigationStackEnabled = false;
            _frame.ContentTransitions =
            [
                new NavigationThemeTransition()
                {
                    DefaultNavigationTransitionInfo = new SlideNavigationTransitionInfo()
                }
            ];
        }

        private SlideNavigationTransitionInfo _frameTransitionInfo =>
            new() { Effect = (SlideNavigationTransitionEffect)Random.Shared.Next(0, 3) };
    }
}
