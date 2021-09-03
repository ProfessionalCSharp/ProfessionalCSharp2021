namespace PInvokeSample;

[SupportedOSPlatform("Windows")]
internal static class WindowsNativeMethods
{
    [DllImport("kernel32.dll", SetLastError = true,
      EntryPoint = "CreateHardLinkW", CharSet = CharSet.Unicode)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool CreateHardLink(
      [In, MarshalAs(UnmanagedType.LPWStr)] string newFileName,
      [In, MarshalAs(UnmanagedType.LPWStr)] string existingFileName,
      nint securityAttributes);

    internal static void CreateHardLink(string oldFileName,
                                        string newFileName)
    {
        if (!CreateHardLink(newFileName, oldFileName, IntPtr.Zero))
        {
            int errorCode = Marshal.GetLastPInvokeError();
            Win32Exception ex = new(errorCode);
            throw new IOException(ex.Message, ex);
        }
    }
}
