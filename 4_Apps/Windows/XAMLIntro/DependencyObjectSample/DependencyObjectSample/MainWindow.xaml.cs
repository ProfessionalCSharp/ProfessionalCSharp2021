using Microsoft.UI.Xaml;

namespace DependencyObjectSample;

public sealed partial class MainWindow : Window
{
    public MainWindow() => this.InitializeComponent();

    private void myButton_Click(object sender, RoutedEventArgs e) => myButton.Content = "Clicked";
}
