using System;

namespace SastImgClient.Infrastructure
{
    public sealed class PageChangedEventArgs(IPageView from, IPageView to) : EventArgs
    {
        public IPageView From { get; } = from;
        public IPageView To { get; } = to;
    }
}
