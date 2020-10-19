using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigurationSample
{
    public class SampleService2
    {
        private readonly IHostEnvironment _hostEnvironment;

        public SampleService2(IHostEnvironment hostEnvironment)
        {
            _hostEnvironment = hostEnvironment;           
        }

        public void ShowHostEnvironment()
        {
            Console.WriteLine(_hostEnvironment.EnvironmentName);
            if (_hostEnvironment.IsDevelopment())
            {
                Console.WriteLine("it's a development environment");
            }
        }

    }
}
