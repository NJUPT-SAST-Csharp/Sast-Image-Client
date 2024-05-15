using System;

namespace SastImgClient.Core
{
    public sealed class VideoItem(long id, Uri source, string title)
    {
        public long Id { get; init; } = id;
        public Uri Source { get; init; } = source;
        public string Title { get; init; } = title;
    }
}
