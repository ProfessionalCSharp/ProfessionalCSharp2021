using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace AttachedProperty
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainWindow : Window
    {
        public MainWindow()
        {
            this.InitializeComponent();

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
}
