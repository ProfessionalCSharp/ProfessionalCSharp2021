namespace PInvokeSample;

public static class FileUtility
{
    public static void CreateHardLink(string oldFileName,
                                      string newFileName)
    {
        if (OperatingSystem.IsWindows())
        {
            WindowsNativeMethods.CreateHardLink(oldFileName, newFileName);
        }
        else if (OperatingSystem.IsLinux())
        {
            LinuxNativeMethods.CreateHardLink(oldFileName, newFileName);
        }
        else
        {
            throw new PlatformNotSupportedException();
        }
    }
}
