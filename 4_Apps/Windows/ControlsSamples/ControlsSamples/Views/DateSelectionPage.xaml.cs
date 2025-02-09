﻿using Microsoft.UI;
using Microsoft.UI.Xaml.Controls;

using System.Globalization;

using Windows.UI;

namespace ControlsSamples.Views;

public sealed partial class DateSelectionPage : Page
{
    public DateSelectionPage() => InitializeComponent();

    public DateTimeOffset MinDate { get; } = DateTimeOffset.Parse("1/1/1965", new CultureInfo("en-US"));

    private void OnDayItemChanging(CalendarView sender, CalendarViewDayItemChangingEventArgs args)
    {
        switch (args.Phase)
        {
            case 0:
                RegisterUpdateCallback();
                break;
            case 1:
                SetBlackoutDates();
                break;
            case 2:
                SetBookings();
                break;
            default:
                break;
        }

        void RegisterUpdateCallback() => args.RegisterUpdateCallback(OnDayItemChanging);

        async void SetBlackoutDates()
        {
            RegisterUpdateCallback();
            CalendarViewDayItem item = args.Item;

            await Task.Delay(500); // simulate a delay for an API call
            if (item.Date < DateTimeOffset.Now || item.Date.DayOfWeek == DayOfWeek.Saturday || item.Date.DayOfWeek == DayOfWeek.Sunday)
            {
                item.IsBlackout = true;
            }
        }

        async void SetBookings()
        {
            CalendarViewDayItem item = args.Item;
            if (item is null)
                return;

            await Task.Delay(3000); // simulate a delay for an API call

            var bookings = GetBookings().ToList();

            var booking = bookings.SingleOrDefault(b => b.day.Date == item.Date.Date);
            if (booking.bookings > 0)
            {
                List<Color> colors = [];
                for (int i = 0; i < booking.bookings; i++)
                {
                    if (item.Date.DayOfWeek == DayOfWeek.Saturday || item.Date.DayOfWeek == DayOfWeek.Sunday)
                    {
                        colors.Add(Colors.Red);
                    }
                    else
                    {
                        colors.Add(Colors.Green);
                    }
                }

                item.SetDensityColors(colors);
            }
        }
    }

    private IEnumerable<(DateTimeOffset day, int bookings)> GetBookings()
    {
        int[] bookingDays = [2, 3, 5, 8, 12, 13, 18, 21, 23, 27];
        int[] bookingsPerDay = [1, 4, 3, 6, 4, 5, 1, 3, 1, 1];

        for (int i = 0; i < 10; i++)
        {
            yield return (DateTimeOffset.Now.Date.AddDays(bookingDays[i]), bookingsPerDay[i]);
        }
    }

    private readonly List<DateTimeOffset> currentDatesSelected = [];

    private async void OnDatesChanged(CalendarView sender, CalendarViewSelectedDatesChangedEventArgs args)
    {
        currentDatesSelected.AddRange(args.AddedDates);
        args.RemovedDates.ToList().ForEach(date => currentDatesSelected.Remove(date));

        string selectedDates = string.Join(", ", currentDatesSelected.Select(d => d.ToString("d")));

        await ShowMessageAsync($"dates selected: {selectedDates}");
    }

    private async void OnDateChanged(CalendarDatePicker sender, CalendarDatePickerDateChangedEventArgs args)
    {
        await ShowMessageAsync($"date changed to: {args.NewDate}");
    }

    private async void OnDateChanged1(object sender, DatePickerValueChangedEventArgs e)
    {
        await ShowMessageAsync($"date changed to: {e.NewDate}");
    }

    private async void OnDatePicked(DatePickerFlyout sender, DatePickedEventArgs args)
    {
        await ShowMessageAsync($"date changed to: {args.NewDate}");
    }

    private async Task ShowMessageAsync(string message)
    {
        ContentDialog dlg = new()
        {
            Title = "Message",
            Content = message,
            PrimaryButtonText = "OK",
            XamlRoot = this.XamlRoot
        };
        await dlg.ShowAsync();
    }
}
