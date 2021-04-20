using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Services.Maps;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace NavigationControls.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class TabViewPage : Page
    {
        public ObservableCollection<TabViewItem> Items { get; } = new();

        public TabViewPage()
        {
            this.InitializeComponent();
            this.Loaded += OnLoaded;
        }

        private int _tabNumber = 0;
        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            for (int i = 1; i < 4; i++)
            {
                tabView.TabItems.Add(CreateNewTab(i));
                _tabNumber = i;
            }
        }

        private TabViewItem CreateNewTab(int index)
        {
            TabViewItem newItem = new() { Header = $"Header {index}", Tag = $"Tag{index}", IconSource = new SymbolIconSource() { Symbol = Symbol.Document } };
            Frame frame = new();
            frame.Navigate(typeof(TabPage), $"Content {index}");
            newItem.Content = frame;
            return newItem;
        }

        private void OnAddTab(TabView sender, object args)
        {
            var newTabItem = CreateNewTab(++_tabNumber);
            tabView.TabItems.Add(newTabItem);
        }

        private void OnTabClose(TabView sender, TabViewTabCloseRequestedEventArgs args)
        {
            tabView.TabItems.Remove(args.Tab);
        }
    }
}
