namespace CodeGenerationSample.Tests;

[UsesVerify]
public class EquatableGeneratorTests
{
    [Fact]
    public Task TestEquatableAttributeGenerated()
    {
        var source = """
            [CodeGenerationSample.ImplementEquatable]
            public partial class Book
            {
                public string? Title { get; set; }
                public string? Author { get; set; }
            }
            """;
        return TestHelperEquatable.VerifyAsync(source);
    }
}