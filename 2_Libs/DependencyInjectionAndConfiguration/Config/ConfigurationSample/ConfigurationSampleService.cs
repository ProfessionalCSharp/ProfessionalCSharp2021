using Microsoft.Extensions.Configuration;
using System;

public class ConfigurationSampleService
{
    private readonly IConfiguration _configuration;

    public ConfigurationSampleService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public void ShowConfiguration()
    {
        string value1 = _configuration.GetValue<string>("Key1");
        Console.WriteLine(value1);
        string value1b = _configuration["Key1"];
        Console.WriteLine(value1b);
        string value2 = _configuration.GetSection("Section1")["Key2"];
        Console.WriteLine(value2);
        string connectionString = _configuration.GetConnectionString("BooksConnection");
        Console.WriteLine(connectionString);
        Console.WriteLine();
    }

    public void ShowTypedConfiguration()
    {
        Console.WriteLine(nameof(ShowTypedConfiguration));
        var section = _configuration.GetSection("SomeTypedConfig");
        var typedConfig = section.Get<StronglyTypedConfig>(binder => binder.BindNonPublicProperties = true);
        Console.WriteLine(typedConfig);
        Console.WriteLine();
    }

    public void ShowCustomConfiguration()
    {
        Console.WriteLine(nameof(ShowCustomConfiguration));
        var value = _configuration["KeyCustom1"];
        Console.WriteLine(value);
        Console.WriteLine();
    }

    public void ShowDynamicValue()
    {
        Console.WriteLine(nameof(ShowDynamicValue));
        bool exit = false;
        do
        {
            Console.WriteLine("Press return for the next iteration, 'exit' to exit");
            string? input = Console.ReadLine();
            if (input?.ToLower() == "exit")
            {
                exit = true;
            }
            var value = _configuration["dynamicvalue"];
            Console.WriteLine(value);
        } while (!exit);
    }

    public void ShowDynamicChangedValue()
    {
        var changeToken = _configuration.GetReloadToken();
        var d = changeToken.RegisterChangeCallback(o =>
        {
            Console.WriteLine($":value changed");
            var value = _configuration["dynamicvalue"];
            Console.WriteLine(value);
        }, null);
    }
}
