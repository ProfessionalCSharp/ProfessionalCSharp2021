using Microsoft.Extensions.Configuration;
using System;

namespace ConfigurationSample
{
    public class InnerConfig
    {
        public string Key5 { get; set; }
    }

    public class TypedConfig
    {
        public string Key3 { get; set; }
        public string Key4 { get; set; }
        public InnerConfig InnerConfig { get; set; }

        public override string ToString() =>
            $"values: {Key3} {Key4} {InnerConfig.Key5}";
    }

    public class SampleService
    {
        private readonly IConfiguration _configuration;

        public SampleService(IConfiguration configuration)
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
            var typedConfig = section.Get<TypedConfig>(binder => binder.BindNonPublicProperties = true);
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
    }
}
