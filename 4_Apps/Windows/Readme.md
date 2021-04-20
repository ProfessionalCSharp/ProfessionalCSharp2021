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
* ControlsSamples (Viewbox, TextBox)
* ParallaxViewSample
* DateSelectionSample (CalendarView, CalendarDatePicker, DatePickerFlyout, ProgressBar, Slider)
* DataBindingSamples (compiled binding)
* PageNavigation
* NavigationControls (Hub, Pivot, NavigationView)
* LayoutSamples (Grid, VariableSizedWrapGrid, RelativePanel, Adaptive Trigger, deferred loading)

extra:
* ReflectionDataBindingSamples (Binding)

See [WinUI](../../WinUI.md) for information what's needed to create, build, and run WinUI applications.

To create WinUI applications, use this template with Visual Studio: **Blank App, Packaged (WinUI 3 in Desktop)**

Some workarounds with Project Reunion 0.5.5:

> MessageDialog needs a Windows initialization (as shown in the IntroXAML sample)

> Before the release of the May-2021 update of .NET 5, add this ItemGroup to the project file:

```xml
  <ItemGroup>
    <FrameworkReference Update="Microsoft.Windows.SDK.NET.Ref" RuntimeFrameworkVersion="10.0.18362.16" />
    <FrameworkReference Update="Microsoft.Windows.SDK.NET.Ref" TargetingPackVersion="10.0.18362.16" />
  </ItemGroup>
```

 
For code comments and issues please check [Professional C#'s GitHub Repository](https://github.com/ProfessionalCSharp/ProfessionalCSharp7)

Please check my blog [csharp.christiannagel.com](https://csharp.christiannagel.com "csharp.christiannagel.com") for additional information for topics covered in the book.

Thank you!