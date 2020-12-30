using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using Microsoft.AppCenter.Analytics;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace WinUIUWPAppAnalytics
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            Analytics.TrackEvent(EventNames.PageNavigation, new Dictionary<string, string> { ["Page"] = nameof(MainPage) });
        }

        private void OnButtonClick(object sender, RoutedEventArgs e)
        {
            Analytics.TrackEvent(EventNames.ButtonClicked, new Dictionary<string, string> { ["State"] = textState.Text });
        }

        private async void OnAnalyticsChanged(object sender, RoutedEventArgs e)
        {
            if (sender is CheckBox checkbox)
            {
                bool isChecked = checkbox?.IsChecked ?? true;
                await Analytics.SetEnabledAsync(isChecked);
            }
        }
    }
}
