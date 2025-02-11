using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using Microsoft.UI.Xaml.Shapes;

using System.Runtime.InteropServices.WindowsRuntime;

using Windows.UI;

namespace LayoutSamples.Views;

public sealed partial class VariableSizedWrapGridPage : Page
{
    public VariableSizedWrapGridPage() => InitializeComponent();

    protected override void OnNavigatedTo(NavigationEventArgs e)
    {
        Grid[] items = [..
            Enumerable.Range(1, 30).Select(i =>
            {
                byte[] colorBytes = new byte[3];
                Random.Shared.NextBytes(colorBytes);
                Rectangle rect = new()
                {
                    Height = Random.Shared.Next(40, 150),
                    Width = Random.Shared.Next(40, 150),
                    Fill = new SolidColorBrush(new Color
                    {
                        R = colorBytes[0],
                        G = colorBytes[1],
                        B = colorBytes[2],
                        A = 255
                    })
                };
                TextBlock textBlock = new()
                {
                    Text = i.ToString(),
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center
                };
                Grid grid = new();
                grid.Children.Add(rect);
                grid.Children.Add(textBlock);
                return grid;
            })];

        foreach (var item in items)
        {
            grid1.Children.Add(item);
            Rectangle? rect = item.Children.First() as Rectangle;
            if (rect is not null && rect.Width > 50)
            {
                int columnSpan = ((int)rect.Width / 50) + 1;
                VariableSizedWrapGrid.SetColumnSpan(item, columnSpan);
                int rowSpan = ((int)rect.Height / 50) + 1;
                VariableSizedWrapGrid.SetRowSpan(item, rowSpan);
            }
        }
        base.OnNavigatedTo(e);
    }
}
