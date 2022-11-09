# Readme - Code Samples for Chapter 13, Managed and Unmanaged Memory

**Managed and Unmanaged Memory** is the last chapter of Part I, which not only shows using the `IDisposable` interface with the `using` statement and the new `using` declaration but also demonstrates using the `Span` type with managed and unmanaged memory. You can read about using Platform Invoke both with Windows and with Linux environments.

This chapter contains the following code samples:

* PointerPlayground (using pointers with C#, *unsafe* keyword)
* PointerPlayground2 (adding a struct)
* QuickArray (quick sorting using pointers, *stackalloc* keyword)
* ReferenceSemantics (ref return, ref local, ref readonly)
* SpanSample (the Span type)
* PlatformInvokeSample (invoking native methods from C#)

For code comments and issues please check [Professional C#'s GitHub Repository](https://github.com/ProfessionalCSharp/ProfessionalCSharp2021)

Please check my blog [csharp.christiannagel.com](https://csharp.christiannagel.com "csharp.christiannagel.com") for additional information for topics covered in the book.

Thank you!

## Updates with C# 10

See [Updates with C# 10](../../Dotnet6Updates.md)

### QuickArray

The Main method was replaced with top-level statements, and now uses an `unsafe` code block.

### SpanSample

Marshal.AllocHGlobal has been replaced with NativeMemory.Alloc

Marshal.FreeHGlobal has been replaced with NativeMemory.Free

### PInvokeSampleLib

Marshal.GetLastWin32Error has been replaced with Marshal.GetLastPInvokeError

### Win32InteropSample

The additional sample to show using a Source Generator to generate PInvoke functionality uses different namespaces with the new version.