using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Media;

namespace AttachedProperty;

public sealed partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();

        MyAttachedPropertyProvider.SetMySample(button1, "sample value");

        foreach (var item in GetChildren(grid1,
            e => MyAttachedPropertyProvider.GetMySample(e) != string.Empty))
        {
            list1.Items.Add(
              $"{item.Name}: {MyAttachedPropertyProvider.GetMySample(item)}");
        }
    }

    private IEnumerable<FrameworkElement> GetChildren(FrameworkElement element, Func<FrameworkElement, bool> pred)
    {
        int childrenCount = VisualTreeHelper.GetChildrenCount(element);
        for (int i = 0; i < childrenCount; i++)
        {
            var child = VisualTreeHelper.GetChild(element, i) as FrameworkElement;
            if (child != null && pred(child))
            {
                yield return child;
            }
        }
    }
}
