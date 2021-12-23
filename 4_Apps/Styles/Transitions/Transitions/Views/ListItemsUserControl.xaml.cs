using Microsoft.UI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Shapes;

namespace Transitions.Views;

public sealed partial class ListItemsUserControl : UserControl
{
    public ListItemsUserControl() => InitializeComponent();

    private void OnAdd(object sender, RoutedEventArgs e)
    {
        list1.Items.Add(CreateRectangle());
        list2.Items.Add(CreateRectangle());
    }

    private Rectangle CreateRectangle() =>
      new Rectangle
      {
          Width = 90,
          Height = 40,
          Margin = new Thickness(5),
          Fill = new SolidColorBrush { Color = Colors.Blue }
      };

    private void OnRemove(object sender, RoutedEventArgs e)
    {
        if (list1.Items.Count > 0)
        {
            list1.Items.RemoveAt(0);
            list2.Items.RemoveAt(0);
        }
    }
}
