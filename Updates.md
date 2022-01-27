# Book Updates

See [.NET 6 Updates](Dotnet6Updates.md) for changes with .NET 6

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
