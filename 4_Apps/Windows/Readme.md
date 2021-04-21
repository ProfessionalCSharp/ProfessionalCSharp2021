# Readme - Code Samples for Chapter 29, Windows Apps

The sample code for this chapter contains this solution:

* HelloWindows (Startup, MainPage, events)
* IntroXAML
    * DataLib (library used by Windows App)
    * XAMLIntro (elements and attributes)
    * DependencyObjectSample (dependency properties)
    * RoutedEvents
    * AttachedProperty
    * MarkupExtensions
    * CustomMarkupExtension
* ControlsSamples (Viewbox, TextBox, CalendarView, ProrgressBar, Slider...)
* DataBindingSamples (compiled binding)
* NavigationControls (Hub, TabView, NavigationView)
* LayoutSamples (Grid, VariableSizedWrapGrid, RelativePanel, Adaptive Trigger, deferred loading)

See [WinUI](../../WinUI.md) for information what's needed to create, build, and run WinUI applications.

To create WinUI applications, use this template with Visual Studio: **Blank App, Packaged (WinUI 3 in Desktop)**

## Workarounds with Project Reunion 0.5.5:

`MessageDialog` needs a Windows initialization (as shown in the IntroXAML sample)
**See https://github.com/microsoft/microsoft-ui-xaml/issues/4167**

Before the release of the May-2021 update of .NET 5, add this ItemGroup to the project file:

```xml
  <ItemGroup>
    <FrameworkReference Update="Microsoft.Windows.SDK.NET.Ref" RuntimeFrameworkVersion="10.0.18362.16" />
    <FrameworkReference Update="Microsoft.Windows.SDK.NET.Ref" TargetingPackVersion="10.0.18362.16" />
  </ItemGroup>
```

## More
 
For code comments and issues please check [Professional C#'s GitHub Repository](https://github.com/ProfessionalCSharp/ProfessionalCSharp7)

Please check my blog [csharp.christiannagel.com](https://csharp.christiannagel.com "csharp.christiannagel.com") for additional information for topics covered in the book.

Thank you!