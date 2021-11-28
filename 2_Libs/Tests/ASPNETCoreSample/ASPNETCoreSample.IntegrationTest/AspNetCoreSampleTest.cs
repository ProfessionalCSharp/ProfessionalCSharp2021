using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace ASPNETCoreSample.IntegrationTest;

public class ASPNETCoreSampleTest
    : IClassFixture<WebApplicationFactory<ASPNETCoreSample.Startup>>
{
    private readonly WebApplicationFactory<ASPNETCoreSample.Startup> _factory;

    public ASPNETCoreSampleTest(WebApplicationFactory<ASPNETCoreSample.Startup> factory)
        => _factory = factory;

    [Fact]
    public async Task ReturnHelloWorld()
    {
        // arrange
        var client = _factory.CreateClient();
        // act
        var response = await client.GetAsync("/");

        // assert
        response.EnsureSuccessStatusCode();
        string responseString = await response.Content.ReadAsStringAsync();
        Assert.Equal("Hello World!", responseString);
    }
}
