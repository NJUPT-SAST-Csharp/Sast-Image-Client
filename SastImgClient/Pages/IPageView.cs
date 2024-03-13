using System;

namespace SastImgClient.Pages
{
    internal interface IPageView
    {
        public string Key { get; }
        public Type PageType { get; }
    }
}
