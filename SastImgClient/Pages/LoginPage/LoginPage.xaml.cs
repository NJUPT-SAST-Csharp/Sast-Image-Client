using System;
using Microsoft.UI.Xaml.Controls;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace SastImgClient.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a _frame.
    /// </summary>
    public sealed partial class LoginPage : Page, IPageView
    {
        public string Key { get; } = nameof(LoginPage);

        public Type PageType { get; } = typeof(LoginPage);

        public LoginPage()
        {
            this.InitializeComponent();
        }
    }
}
