using System;
using FoxNavigator.Pages;

namespace FoxNavigator.Behaviors
{
    public sealed class PageChangedEventArgs(IPageView from, IPageView to) : EventArgs
    {
        public IPageView From { get; } = from;
        public IPageView To { get; } = to;
    }
}
