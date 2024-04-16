using SastImgClient.Components;
using WinUIEx;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace SastImgClient
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a _frame.
    /// </summary>
    internal sealed partial class MainWindow : WindowEx
    {
        public MainWindow(NavigationMenu menu)
        {
            InitializeComponent();
            Content = menu;

            MinWidth = 800;
            MinHeight = 600;
        }
    }
}
