using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;

namespace NavigationControls.Views;

public sealed partial class TabPage : Page
{
    public TabPage() => InitializeComponent();

    protected override void OnNavigatedTo(NavigationEventArgs e)
    {
        Text = e.Parameter?.ToString() ?? "No parameter";
    }

    public string Text
    {
        get { return (string)GetValue(TextProperty); }
        set { SetValue(TextProperty, value); }
    }

    public static readonly DependencyProperty TextProperty =
        DependencyProperty.Register("Text", typeof(string), typeof(TabPage), new PropertyMetadata(null));

}
