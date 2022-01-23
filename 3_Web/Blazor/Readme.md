# Readme - Code Samples for Chapter 27, Blazor

**Blazor** is about the newest enhancement of ASP.NET Core with Razor components, which either allows you to implement C# code running either on the server or in the client using WebAssembly. You learn about the differences between Blazor Server and Blazor WebAssembly, what the restrictions are with these technologies, and the built-in components available.

This chapter contains these samples:

* Blazor.ServerSample
* Blazor.WasmSample
* Blazor.ComponentsSample

## .NET 6 Updates

The code from the `Startup` class moved into the top-level statements using the `WebApplication` class.

New code for registration of the services (page 782):

```csharp
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<WeatherForecastService>();
```

New code for the middleware configuration (page 782) 

```csharp
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
```

## More Information

For code comments and issues please check [Professional C#'s GitHub Repository](https://github.com/ProfessionalCSharp/ProfessionalCSharp2021)

Please check my blog [csharp.christiannagel.com](https://csharp.christiannagel.com "csharp.christiannagel.com") for additional information on topics covered in the book.

Thank you!