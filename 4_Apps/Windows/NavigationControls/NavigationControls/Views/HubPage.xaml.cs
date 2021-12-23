using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;


namespace NavigationControls.Views;

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
