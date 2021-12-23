using Microsoft.UI.Xaml.Data;

using System.Globalization;
using System.Text;

namespace WinUICultureDemo.Converters;

public class CalendarTypeToCalendarInformationConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, string? language)
    {
        if (value is Calendar cal)
        {
            StringBuilder calText = new(50);
            calText.Append(cal.ToString());
            calText.Remove(0, 21);
            calText.Replace("Calendar", "");
            if (cal is GregorianCalendar gregCal)
            {
                calText.Append($" {gregCal.CalendarType}");
            }
            return calText.ToString();
        }
        else
        {
            return null;
        }
    }

    public object? ConvertBack(object value, Type targetType, object parameter, string language)
    {
        throw new NotImplementedException();
    }
}
