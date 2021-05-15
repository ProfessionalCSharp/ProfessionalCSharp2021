using Microsoft.UI.Xaml.Controls;
using Models;
using System.Collections.ObjectModel;

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
            InitializeComponent();
            DataContext = this;

            var countries = new CountryRepository().GetCountries();
            foreach (var country in countries)
            {
                Countries.Add(country);
            }
        }
    }
}
