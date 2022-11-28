# C# 11 and .NET 7 Updates

## .NET Support Length

Chapter 1, Page 9

The names to describe the different support length of .NET changed. In the book you can read about *Current* and *Long Term Support* (LTS) monikers. The new name for *Current* is now **Standard Term Support** (STS). STS is supported for 18 months, and LTS for 36 months (the lenght didn't change).

## HostApplicationBuilder

Chapter 15, Dependency Injection and Configuration

.NET 7 includes the `HostApplicationBuilder` which simplifies creating the DI container. Instead of using `Host.ConfigureDefaultServices`, the `HostApplicationBuilder` is used to configure the DI container. The `HostApplicationBuilder` is used removes the need to pass delegates to configure services, app configuration, and logging.

## Raw String Literals

Chapter 12, Reflection, Metadata, and Source Generators, Page 328

With .NET 7, the source generator sample makes use of **raw string literals**. Thnis makes the code more readable, and the code is easier to maintain.

See the [Readme](1_CS/ReflectionAndSourceGenerators/Readme.md) for more information.

Chapter 19, Networking, page 545

The *HttpServer* code sample is changed to use C# 11 **raw string literals**.

## Platform Invoke

Chapter 13, Managed and unmanaged Memory, Page 368

With .NET 7, the `LibraryImport` attribute can be used instead of `DllImport`. This new attribute makes use of a source generator.

See [PInvokeSampleLib in More Samples](5_More/PInvoke/). This [Readme](1_CS/Memory/Readme.md)  gives information on changes needed.


