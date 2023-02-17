if (!OperatingSystem.IsWindows())
{
    Console.WriteLine("COM Interop is Windows only");
    return;
}

string progId = "CNILearn.Calculator";

var type = Type.GetTypeFromProgID(progId);
if (type is null) throw new InvalidOperationException($"Type {progId} not registered");

object? obj = Activator.CreateInstance(type);
if (obj is not null)
{
    dynamic calc = obj;
    Console.WriteLine(calc.Add(38, 4));
    Console.WriteLine(calc.Subtract(46, 4));
}
