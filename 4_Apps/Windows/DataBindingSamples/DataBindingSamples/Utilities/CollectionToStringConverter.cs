using Microsoft.UI.Xaml.Data;

namespace DataBindingSamples.Utilities;

public class CollectionToStringConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, string language)
    {
        IEnumerable<string> names = (IEnumerable<string>)value;
        return string.Join(parameter?.ToString() ?? ", ", names);
    }

    public object ConvertBack(object value, Type targetType, object parameter, string language)
    {
        throw new NotImplementedException();
    }
}
