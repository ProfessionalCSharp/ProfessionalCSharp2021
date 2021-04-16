using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Templates.Views
{
    public sealed partial class StyledListUserControl : UserControl
    {
        public ObservableCollection<Country> Countries { get; } =
            new ObservableCollection<Country>();

        public StyledListUserControl()
        {
            this.InitializeComponent();
            this.DataContext = this;

            var countries = new CountryRepository().GetCountries();
            foreach (var country in countries)
            {
                Countries.Add(country);
            }
        }
    }
}
