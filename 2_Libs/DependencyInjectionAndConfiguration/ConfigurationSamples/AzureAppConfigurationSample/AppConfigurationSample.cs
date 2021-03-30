using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureAppConfigurationSample
{
    public class AppConfigurationSample
    {
        private readonly IConfiguration _configuration;

        public AppConfigurationSample(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void Sample1()
        {
            string value1 = _configuration["Key1"];
            Console.WriteLine(value1);
        }
    }
}
