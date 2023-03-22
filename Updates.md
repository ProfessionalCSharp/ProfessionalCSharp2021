# Book Updates and Issues

* See [.NET 6 Updates](Dotnet6Updates.md) for changes with .NET 6
* See [.NET 7 Updates](Dotnet7Updates.md) for changes with .NET 7

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

Thanks to [@ShervanN](https://github.com/ShervanN) for reporting this issue!

## Chapter 15, Dependency Injection and Configuration

Page 394, the code snippet in the book misses a variable name. The variable `services` should be added:

```csharp
static ServiceProvider GetServiceProvider()
{
  ServiceCollection services = new();  // Here the variable name is missing in the book
  services.AddSingleton<IGreetingService, GreetingService>();
  services.AddTransient<HomeController>();
  return services.BuildServiceProvider();
}
```

## Chapter 16, Diagnostics and Metrics

Page 422, the code snippet in the book misses a variable name. The variable `httpClient` should be added:

```csharp
public NetworkService(HttpClient httpClient, ILogger<NetworkService> logger)
{
  _httpClient = httpClient;
  _logger = logger;
  _logger.LogTrace("ILogger injected into {service}", nameof(NetworkService));
}
```

Thanks to [@ShervanN](https://github.com/ShervanN) for reporting this issue!

## Chapter 17, Parallel Programming

Page 451, the code snippet can be simplified - retrieving the result is done two times, and `Result` is already blocking:

The previous code was:

```csharp
public static void TaskWithResultDemo()
{
  Task<(int Result, int Remainder)> t1 = new(TaskWithResult, (8, 3));
  t1.Start();
  Console.WriteLine(t1.Result);
  t1.Wait();
  Console.WriteLine($"result from task: {t1.Result.Result}, and remainder: {t1.Result.Remainder}");
}

The new code is:

```csharp
public static void TaskWithResultDemo()
{
  Task<(int Result, int Remainder)> t1 = new(TaskWithResult, (8, 3));
  t1.Start();
  Console.WriteLine($"result from task: {t1.Result.Result}, and remainder: {t1.Result.Remainder}");
}
```

Thanks to [@ShervanN](https://github.com/ShervanN) for reporting this issue!

Page 461, Using the Timer Class

The code in the book contains too many braces.

This code:

```csharp
private static void ThreadingTimer()
{
  void TimeAction(object? o) =>
    Console.WriteLine($"System.Threading.Timer {DateTime.Now:T}");

  using Timer t1 = new(
    TimeAction,
    null,
    dueTime: TimeSpan.FromSeconds(2),
    period: TimeSpan.FromSeconds(3)))
  {
    Task.Delay(15000).Wait();
  }
}
```

should be (remove the braces with the using declaration):

```csharp
private static void ThreadingTimer()
{
  void TimeAction(object? o) =>
    Console.WriteLine($"System.Threading.Timer {DateTime.Now:T}");

  using Timer t1 = new(
    TimeAction,
    null,
    dueTime: TimeSpan.FromSeconds(2),
    period: TimeSpan.FromSeconds(3));

  Task.Delay(15000).Wait();
}
```

With the new version, this code has been updated to top-level statements without the `ThreadingTimer` method (see the [current source code](https://github.com/ProfessionalCSharp/ProfessionalCSharp2021/blob/main/2_Libs/Parallel/ParallelSamples/TimersSample/Program.cs)

```csharp
static void TimeAction(object? o) =>
    Console.WriteLine($"System.Threading.Timer {DateTime.Now:T}");

using Timer t1 = new(
    TimeAction, 
    null, 
    dueTime: TimeSpan.FromSeconds(2),
    period: TimeSpan.FromSeconds(3));

await Task.Delay(15000);
```

Thanks to [@ShervanN](https://github.com/ShervanN) for reporting this issue!

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
