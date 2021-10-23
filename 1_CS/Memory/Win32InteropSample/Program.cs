using Windows.Win32;
using Windows.Win32.Security;

[assembly:System.Runtime.Versioning.SupportedOSPlatform("Windows")]

if (args.Length != 2)
{
    Console.WriteLine("start using [existingFile] [newFile]");
    return;
}
string existingFile = args[0];
string newFile = args[1];
SECURITY_ATTRIBUTES sa = new();
bool created = PInvoke.CreateHardLink(newFile, existingFile, ref sa);
