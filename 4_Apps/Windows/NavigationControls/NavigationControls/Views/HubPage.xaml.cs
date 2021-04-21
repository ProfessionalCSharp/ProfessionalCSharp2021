using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace NavigationControls.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class HubPage : Page
    {
        public HubPage() => InitializeComponent();

        public void OnHeaderClick(object sender, HubSectionHeaderClickEventArgs e)
        {
            Info = e.Section.Tag as string;
        }

        public string? Info
        {
            get => (string)GetValue(InfoProperty);
            set => SetValue(InfoProperty, value);
        }

        public static readonly DependencyProperty InfoProperty =
            DependencyProperty.Register("Info", typeof(string), typeof(HubPage), new PropertyMetadata(string.Empty));
    }
}
