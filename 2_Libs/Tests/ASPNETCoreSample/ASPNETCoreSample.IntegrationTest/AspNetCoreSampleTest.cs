using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace ASPNETCoreSample.IntegrationTest;

public class ASPNETCoreSampleTest
    : IClassFixture<WebApplicationFactory<Program>>
{
    class WebApplication : WebApplicationFactory<Program>
    {
        protected override IHost CreateHost(IHostBuilder builder)
        {
            return base.CreateHost(builder);
        }
    }

    [Fact]
    public async Task ReturnHelloWorld()
    {
        // arrange
        var app = new WebApplication();
        var client = app.CreateClient();

        // act
        var response = await client.GetAsync("/");

        // assert
        response.EnsureSuccessStatusCode();
        string responseString = await response.Content.ReadAsStringAsync();
        Assert.Equal("Hello World!", responseString);
    }
}
