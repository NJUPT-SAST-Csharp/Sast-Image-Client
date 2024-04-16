// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace SastImgClient.Core
{
    public sealed class ImageItem
    {
        public string Title { get; init; } = string.Empty;
        public long Id { get; init; }
        public string Url { get; init; } = string.Empty;
    }
}
