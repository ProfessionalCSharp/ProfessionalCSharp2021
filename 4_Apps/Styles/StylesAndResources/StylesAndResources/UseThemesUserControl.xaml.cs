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

namespace StylesAndResources
{
    public sealed partial class UseThemesUserControl : UserControl
    {
        public UseThemesUserControl()
        {
            this.InitializeComponent();
        }

        private void OnChangeTheme(object sender, RoutedEventArgs e)
        {
            grid1.RequestedTheme = grid1.RequestedTheme == ElementTheme.Dark ?
                ElementTheme.Light : ElementTheme.Dark;
        }
    }
}
