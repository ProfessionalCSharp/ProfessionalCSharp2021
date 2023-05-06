# Readme - Code Samples for Chapter 19, Networking

In **Networking** you learn about foundational classes for network programming, such as the `Socket` class and how to create applications using TCP and UDP. You also use the HttpClient factory pattern to create `HttpClient` objects with automatic retries if transient errors occur.

This chapter contains the following code samples:

* Utilities
    * Utilities (using `Uri` and `IPAddress`)
* DnsLookup
    * DnsLookup (showing IP Addresses using `Dns`)
* Sockets
    * EchoServer (using sockets and pipelines to create a server)
    * EchoClient (using sockets and pipelines to create a client)
* TCP
    * TcpServer (TCP server using `TcpListener` and a custom protocol)
    * TcpClientSample (TCP client using `TcpClient`)
* UDP
    * UdpReceiver (UDP receiver using `UdpClient`)
    * UdpSender (UDP sender using `UdpClient`)
* HTTP
    * HttpServerSample (HTTP server using Kestrel)
    * HttpClientSample (HTTP client using `HttpClient`)
    * WinAppHttpClient (HTTP client with UWP app where `HttpClient` supports HTTP 2.0) 

## .NET 6 Updates

See [.NET 6 updates](../../Dotnet6Updates.md).

New with ASP.NET Core are the `WebApplication` and `WebApplicationBuilder` classes. The code in the repo has been updated to use the `WebApplication` and `WebApplicationBuilder` classes (see page 542/543):

```csharp
using HttpServerSample;

var builder = WebApplication.CreateBuilder(args);
builder.WebHost.ConfigureKestrel(kestrelOptions =>
{
    kestrelOptions.AddServerHeader = true;
    kestrelOptions.AllowResponseHeaderCompression = true;
    kestrelOptions.Limits.Http2.MaxStreamsPerConnection = 10;
    kestrelOptions.Limits.MaxConcurrentConnections = 20;
    kestrelOptions.ConfigureHttpsDefaults(httpsConfig =>
    {

    });
}).UseUrls("http://localhost:5020", "https://localhost:5021");
```

## .NET 7 Updates

The HttpServerSample is changed to use **raw string literals**.

See [.NET 7 updates](../../Dotnet7Updates.md).
 
For code comments and issues please check [Professional C#'s GitHub Repository](https://github.com/ProfessionalCSharp/ProfessionalCSharp2021)

Please check my blog [csharp.christiannagel.com](https://csharp.christiannagel.com "csharp.christiannagel.com") for additional information for topics covered in the book.

Thank you!