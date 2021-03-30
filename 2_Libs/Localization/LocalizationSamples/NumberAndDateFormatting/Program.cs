using System;
using System.Globalization;

NumberFormatDemo();
DateFormatDemo();

void NumberFormatDemo()
{
    int val = 1234567890;

    // culture of the current thread
    string output = val.ToString("N");
    Console.WriteLine($"Current thread culture: {CultureInfo.CurrentCulture}: {output}");

    // use IFormatProvider
    output = val.ToString("N", new CultureInfo("fr-FR"));
    Console.WriteLine($"IFormatProvider with fr-FR culture {output}");

    // change the culture of the thread
    CultureInfo.CurrentCulture = new("de-DE");
    output = val.ToString("N");
    Console.WriteLine($"Changed culture of the thread to de-DE: {output}");
}

void DateFormatDemo()
{
    DateTime d = new(2024, 09, 17);

    // current culture
    string output = d.ToString("D");
    Console.WriteLine($"Current thread culture: {CultureInfo.CurrentCulture}: {output}");

    // use IFormatProvider
    output = d.ToString("D", new CultureInfo("fr-FR"));
    Console.WriteLine($"IFormatProvider with fr-FR culture: {output}");


    CultureInfo.CurrentCulture = new("es-ES");
    output = d.ToString("D");
    Console.WriteLine($"Changed culture of the thread {CultureInfo.CurrentCulture}: {output}");
}
