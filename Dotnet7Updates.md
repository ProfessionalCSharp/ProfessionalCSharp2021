# C# 11 and .NET 7 Updates

## .NET Support Length

Chapter 1, Page 9

The names to describe the different support length of .NET changed. In the book you can read about *Current* and *Long Term Support* (LTS) monikers. The new name for *Current* is now **Standard Term Support** (STS). STS is supported for 18 months, and LTS for 36 months (the lenght didn't change).

## Platform Invoke

Chapter 13, Managed and unmanaged Memory, Page 368

With .NET 7, the `LibraryImport` attribute can be used instead of `DllImport`. This new attribute makes use of a source generator.

See [PInvokeSampleLib in More Samples](5_More/PInvoke/). This [Readme](1_CS/Memory/Readme.md)  gives information on changes needed.
