// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

using Microsoft.UI.Xaml.Controls;
using SastImgClient.Infrastructure;

namespace SastImgClient.Pages.Profile
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a _frame.
    /// </summary>
    internal sealed partial class ProfilePage : Page, IPageView<ProfilePageVm>
    {
        public string Key { get; } = nameof(ProfilePage);

        public ProfilePageVm ViewModel { get; }

        public ProfilePage(ProfilePageVm viewmodel)
        {
            ViewModel = viewmodel;
            InitializeComponent();
        }
    }
}
