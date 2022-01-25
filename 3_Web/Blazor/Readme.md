# Readme - Code Samples for Chapter 27, Blazor

**Blazor** is about the newest enhancement of ASP.NET Core with Razor components, which either allows you to implement C# code running either on the server or in the client using WebAssembly. You learn about the differences between Blazor Server and Blazor WebAssembly, what the restrictions are with these technologies, and the built-in components available.

This chapter contains these samples:

* Blazor.ServerSample
* Blazor.WasmSample
* Blazor.ComponentsSample

## Issues

Page 792, the Blazor.ComponentsSample project is created without a hosted API:

dotnet new blazorwasm -o Blazor.ComponentsSample

## .NET 6 Updates

### WebApplication class

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

### Layout for the _Host

The _Host file now uses a layout page (_Layout.cshtml). The HTML code generated for the file _Host.cshtml moved to this layout page (page 783).

### Top-level statements for the middleware configuration with the server-side of Blazor.Wasm

Updated code for page 788

```csharp
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();


app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();
```



## More Information

For code comments and issues please check [Professional C#'s GitHub Repository](https://github.com/ProfessionalCSharp/ProfessionalCSharp2021)

Please check my blog [csharp.christiannagel.com](https://csharp.christiannagel.com "csharp.christiannagel.com") for additional information on topics covered in the book.

Thank you!