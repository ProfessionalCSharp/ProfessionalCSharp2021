using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

#nullable enable
// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace WinUICultureDemo
{
    public sealed partial class CultureDetailUC : UserControl 
    {
        public CultureDetailUC() => this.InitializeComponent();

        public CultureData CultureData
        {
            get => (CultureData)GetValue(CultureDataProperty);
            set => SetValue(CultureDataProperty, value);
        }

        public static readonly DependencyProperty CultureDataProperty =
            DependencyProperty.Register("CultureData", typeof(CultureData), typeof(CultureDetailUC), new PropertyMetadata(null));

    }
}
#nullable restore