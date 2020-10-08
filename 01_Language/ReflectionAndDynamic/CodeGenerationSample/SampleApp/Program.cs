using System;

namespace SampleApp
{


    class Program
    {
        static void Main(string[] args)
        {
            HelloWorldGenerated.HelloWorld.SayHello();

            var b = new Book();
            b.PropertyChanged += (sender, e) =>
            {
                Console.WriteLine($"property {e.PropertyName} changed");
            };
        }
    }
}
