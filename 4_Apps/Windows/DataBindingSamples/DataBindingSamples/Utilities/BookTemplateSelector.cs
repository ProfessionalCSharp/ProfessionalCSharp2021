using DataBindingSamples.Models;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace DataBindingSamples.Utilities
{
    public class BookTemplateSelector : DataTemplateSelector
    {
        public DataTemplate? WroxTemplate { get; set; }
        public DataTemplate? DefaultTemplate { get; set; }

        protected override DataTemplate? SelectTemplateCore(object item) =>
            item switch
            {
                Book { Publisher: "Wrox Press"} => WroxTemplate,
                Book => DefaultTemplate,
                _ => null
            };
    }
}
