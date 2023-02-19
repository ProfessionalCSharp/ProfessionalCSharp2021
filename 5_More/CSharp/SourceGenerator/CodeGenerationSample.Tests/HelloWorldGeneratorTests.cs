namespace CodeGenerationSample.Tests;

[UsesVerify]
public class HelloWorldGeneratorTests
{
    [Fact]
    public Task TestHelloWorldGenerated()
    {
        var source = """
            namespace Test;
            public class TestClass
            {
                public static void Main()
                {
                    Console.WriteLine("Test");
                }
            }
            """;
        return TestHelperHello.VerifyAsync(source);
    }
}