using System;
using System.Collections.Generic;
using System.ComponentModel;
using SastImgClient.Pages;

namespace SastImgClient.Infrastructure
{
    internal interface INavigator : INotifyPropertyChanged
    {
        public void NavigateTo(string pageName);
        public void NavigateTo(Type pageType);
        public void NavigateTo<T>()
            where T : IPageView;

        public void Back();
        public void Forward();

        public IEnumerable<IPageView> Pages { get; }
        public IPageView CurrentPage { get; }
        public bool CanBack { get; }
        public bool CanForward { get; }
    }
}
