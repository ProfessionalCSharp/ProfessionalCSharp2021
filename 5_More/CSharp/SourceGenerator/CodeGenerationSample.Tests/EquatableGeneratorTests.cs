namespace CodeGenerationSample.Tests;

[UsesVerify]
public class EquatableGeneratorTests
{
    [Fact]
    public Task TestEquatableAttributeGenerated()
    {
        var source = """
            namespace Test.Sample;

            [CodeGenerationSample.ImplementEquatableAttribute()]
            public partial class Book
            {
                public string? Title { get; set; }
                public string? Author { get; set; }
            }
            """;
        return TestHelperEquatable.VerifyAsync(source);
    }
}