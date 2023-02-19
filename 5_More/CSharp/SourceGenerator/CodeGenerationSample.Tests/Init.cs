using System.Runtime.CompilerServices;

namespace CodeGenerationSample.Tests;

public static class ModuleInitializer
{
    [ModuleInitializer]
    public static void Init() => VerifySourceGenerators.Initialize();
}
