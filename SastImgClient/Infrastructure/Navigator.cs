using System.Collections.Generic;
using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using SastImgClient.Pages;

namespace SastImgClient.Infrastructure
{
    internal sealed partial class Navigator(IEnumerable<IPageView> pages)
        : ObservableObject,
            INavigator
    {
        private readonly Stack<string> _forwardPages = new();
        private readonly Stack<string> _backPages = new();

        private readonly Dictionary<string, IPageView> _pages = pages
            .Select(p => (p.Key, p))
            .ToDictionary();

        [ObservableProperty]
        private IPageView _currentPage;

        [ObservableProperty]
        private bool _canBack = false;

        [ObservableProperty]
        private bool _canForward = false;

        public IEnumerable<IPageView> Pages => _pages.Values;

        public void Back()
        {
            if (_backPages.TryPop(out var pageHash))
            {
                var page = _pages[pageHash];
                _forwardPages.Push(pageHash);
                CurrentPage = page;

                CheckBackAndForward();
            }
        }

        public void Forward()
        {
            if (_forwardPages.TryPop(out var pageHash))
            {
                _backPages.Push(CurrentPage.Key);
                CurrentPage = _pages[pageHash];

                CheckBackAndForward();
            }
        }

        public void NavigateTo<T>()
            where T : IPageView
        {
            var page = _pages.Values.OfType<T>().First() as IPageView;

            if (CurrentPage == page)
            {
                return;
            }

            _backPages.Push(CurrentPage.Key);
            _forwardPages.Clear();
            CurrentPage = page;

            CheckBackAndForward();
        }

        public void NavigateTo(string pageName)
        {
            var page = _pages.Values.First(p => p.Key == pageName);

            if (CurrentPage == page)
            {
                return;
            }

            _backPages.Push(CurrentPage.Key);
            _forwardPages.Clear();
            CurrentPage = page;

            CheckBackAndForward();
        }

        public void InitializePage<T>()
            where T : IPageView
        {
            CurrentPage = _pages.Values.OfType<T>().First();
        }

        private void CheckBackAndForward()
        {
            CanForward = _forwardPages.Count > 0;
            CanBack = _backPages.Count > 0;
        }
    }
}
