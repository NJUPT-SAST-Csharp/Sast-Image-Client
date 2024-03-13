using System;
using Microsoft.UI.Xaml.Controls;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace SastImgClient.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a _frame.
    /// </summary>
    public sealed partial class MainPage : Page, IPageView
    {
        public string Key { get; } = nameof(MainPage);

        public Type PageType { get; } = typeof(MainPage);

        public MainPage()
        {
            InitializeComponent();
        }
    }
}
