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

namespace Transitions.Views
{
    public sealed partial class PaneTransitionUserControl : UserControl
    {
        public PaneTransitionUserControl()
        {
            this.InitializeComponent();
        }

        private void OnShow(object sender, RoutedEventArgs e)
        {
            popup1.IsOpen = true;
            popup2.IsOpen = true;
            popup3.IsOpen = true;
        }

        private void OnHide(object sender, RoutedEventArgs e)
        {
            popup1.IsOpen = false;
            popup2.IsOpen = false;
            popup3.IsOpen = false;
        }

    }
}
