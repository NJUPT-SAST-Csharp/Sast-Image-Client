using System;
using System.Collections.Generic;
using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using FoxNavigator.Behaviors;
using FoxNavigator.Pages;
using Microsoft.Extensions.DependencyInjection;

namespace FoxNavigator
{
    public sealed partial class Navigator(IServiceProvider services) : ObservableObject, INavigator
    {
        private readonly Stack<IPageView> _forwardPages = new();
        private readonly Stack<IPageView> _backPages = new();
        private readonly IServiceProvider _services = services;

        [ObservableProperty]
        private IPageView _currentPage = null!;

        [ObservableProperty]
        private bool _canBack = false;

        [ObservableProperty]
        private bool _canForward = false;

        public event PageChangedEventHandler? OnPageChanged;

        public IReadOnlyCollection<IPageView> Pages => _services.GetServices<IPageView>().ToList();

        public void Back()
        {
            if (_backPages.TryPop(out var page))
            {
                _forwardPages.Push(page);
                CurrentPage = page;
            }
        }

        public void Forward()
        {
            if (_forwardPages.TryPop(out var page))
            {
                _backPages.Push(CurrentPage);
                CurrentPage = page;
            }
        }

        public bool NavigateTo<T>()
            where T : IPageView
        {
            var page = _services.GetRequiredService<T>();

            if (CanNavigate(page) == false)
            {
                return false;
            }

            _backPages.Push(CurrentPage);
            _forwardPages.Clear();
            CurrentPage = page;

            return true;
        }

        public bool NavigateTo(string pageKey)
        {
            var page = _services.GetRequiredKeyedService<IPageView>(pageKey);

            if (CanNavigate(page) == false)
            {
                return false;
            }

            _backPages.Push(CurrentPage);
            _forwardPages.Clear();
            CurrentPage = page;

            return true;
        }

        public void ClearNavigationHistory()
        {
            _backPages.Clear();
            _forwardPages.Clear();
            UpdateStacks();
        }

        private bool CanNavigate(IPageView page)
        {
            var type = typeof(IBeforeNavigateBehavior<,>).MakeGenericType(
                [CurrentPage.GetType(), page.GetType()]
            );

            var tag = _services
                .GetServices(type)
                .Cast<IBeforeNavigatorBehavior>()
                .All(behavior => behavior.CanNavigate);

            return true;
        }

        partial void OnCurrentPageChanged(IPageView? oldValue, IPageView newValue)
        {
            UpdateStacks();
            OnPageChanged?.Invoke(this, new(oldValue!, newValue));
        }

        private void UpdateStacks()
        {
            CanForward = _forwardPages.Count > 0;
            CanBack = _backPages.Count > 0;
        }

        public void Initialize(string pageName)
        {
            if (CurrentPage is not null)
            {
                return;
            }
            CurrentPage = _services.GetRequiredKeyedService<IPageView>(pageName);
        }

        public void Initialize<T>()
            where T : IPageView
        {
            if (CurrentPage is not null)
            {
                return;
            }
            CurrentPage = _services.GetRequiredService<T>();
        }
    }
}
