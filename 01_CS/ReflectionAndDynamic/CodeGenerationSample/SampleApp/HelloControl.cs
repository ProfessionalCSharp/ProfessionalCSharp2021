using CodeGenerationSample;

namespace SampleApp
{
    [AddGreet]
    public partial class HelloControl
    {
        public string GreetService(string name) => Greet(name);

        private static partial string Greet(string name);
    }
}
