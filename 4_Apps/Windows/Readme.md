# Readme - Code Samples for Chapter 29, Windows Apps

**Windows Apps** gives you foundational information on XAML, including dependency properties and attached properties. You learn how to create custom markup extensions and about the control categories available with WinUI, including advanced techniques such as adaptive triggers and deferred loading.

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

## Additonal Samples

* [Custom Behavior](../../5_More/WinUI/BehaviorSample/)
* [XamlUiCommand](5_More/WinUI/WinUIAppEditor)

See [WinUI](../../WinUI.md) for information what's needed to create, build, and run WinUI applications.

To create WinUI applications, use this template with Visual Studio: **Blank App, Packaged (WinUI 3 in Desktop)**

## Workarounds with Project Reunion 0.8:

`MessageDialog` needs a Windows initialization (as shown in the XAMLIntro sample)

See [MessageDialog is no longer functional](https://github.com/microsoft/microsoft-ui-xaml/issues/4167)

## More
 
For code comments and issues please check [Professional C#'s GitHub Repository](https://github.com/ProfessionalCSharp/ProfessionalCSharp2021)

Please check my blog [csharp.christiannagel.com](https://csharp.christiannagel.com "csharp.christiannagel.com") for additional information for topics covered in the book.

Thank you!