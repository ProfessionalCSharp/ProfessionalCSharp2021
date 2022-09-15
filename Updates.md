# Book Updates and Issues

> See [.NET 6 Updates](Dotnet6Updates.md) for changes with .NET 6

## Chapter 4, Object-Oriented Programming in C#

Page 98, Rectangle.Clone method - the `Height`should be assigned to `Height`:

```csharp
public override Rectangle Clone()
{
  //...
  r.Size.Height = Size.Height;
  return r;
}
```

Page 101, at the end of the page, the `Height`should be assigned to `Height`:

```csharp
public override Rectangle Clone()
{
  //...
  r.Size.Height = Size.Height;
  return r;
}
```

Thanks to [@lriy816](https://github.com/lriy816) for reporting this issue by creating these pull requests:

* [page 98](https://github.com/ProfessionalCSharp/ProfessionalCSharp2021/pull/119)
* [page 101](https://github.com/ProfessionalCSharp/ProfessionalCSharp2021/pull/118)

## Chapter 7, Delegates, Lambdas and Events

Page 190, the event name should be `NewCarCreated` instead of `NewCarInfo`:

```csharp
  public event EventHandler<CarInfoEventArgs>? NewCarCreated;
```

[See source code - CarDealer.cs](https://github.com/ProfessionalCSharp/ProfessionalCSharp2021/blob/main/1_CS/Delegates/EventsSample/CarDealer.cs)

Thanks to [@DanielNikoofar](https://github.com/DanielNikoofar) for reporting this issue!

## Chapter 11, Tasks and Asynchronous Programming

Page 296 shows this source code:

```csharp
private readonly static Dictionary<string, string> names = new Dictionary<string, string>();
```

With C# 9 and **target-typed new expressions**, the code can be written as shown:

```csharp
private readonly static Dictionary<string, string> names = new();
```

## Chapter 12, Reflection, Metadata, and Source Generators

Page 312 (below the title *The WhatsNewAttributes Library*) has this text:

> The source code is contained in the file `WhatsNewAttributes.cs` in the *WhatsNewAttributes* project of the *WhatsNewAttributes* solution in the example code for this chapter.

It's the *ReflectionSamples* solution. This is the correct text:

> The source code is contained in the file `WhatsNewAttributes.cs` in the *WhatsNewAttributes* project of the *ReflectionSamples* solution in the example code for this chapter.

Thanks to @ShervanN for reporting this issue!

## Chapter 20, Security

Page 560, the command

> az app list --display-name ProCSharpIdentityApp --query [].appId

should be:

> az ad app list --display-name ProCSharpIdentityApp --query [].appId

## Chapter 27, Blazor

Page 792, the command

> dotnet new blazorwasm --hosted -o BlazorComponentsSample

should be

> dotnet new blazorwasm -o BlazorComponentsSample

More information: this project is created without a hosting application. Blazor.Wasm with a hosting application is created in the previous code sample.
