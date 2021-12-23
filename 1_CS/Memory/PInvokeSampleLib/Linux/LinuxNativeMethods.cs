using static PInvokeSample.LinuxNativeMethods.LinkErrors;

namespace PInvokeSample;

[SupportedOSPlatform("Linux")]
internal static class LinuxNativeMethods
{
    internal enum LinkErrors
    {
        EPERM = 1,
        ENOENT = 2,
        EIO = 5,
        EACCES = 13,
        EEXIST = 17,
        EXDEV = 18,
        ENOSPC = 28,
        EROFS = 30,
        EMLINK = 31
    }

    private static Dictionary<LinkErrors, string> _errorMessages = new()
    {
        { EPERM, "On GNU/Linux and GNU/Hurd systems and some others, you cannot make links to directories.Many systems allow only privileged users to do so." },
        { ENOENT, "The file named by oldname doesn’t exist. You can’t make a link to a file that doesn’t exist." },
        { EIO, "A hardware error occurred while trying to read or write to the filesystem." },
        { EACCES, "You are not allowed to write to the directory in which the new link is to be written." },
        { EEXIST, "There is already a file named newname. If you want to replace this link with a new link, you must remove the old link explicitly first." },
        { EXDEV, "The directory specified in newname is on a different file system than the existing file." },
        { ENOSPC, "The directory or file system that would contain the new link is full and cannot be extended." },
        { EROFS, "The directory containing the new link can’t be modified because it’s on a read - only file system." },
        { EMLINK, "There are already too many links to the file named by oldname. (The maximum number of links to a file is LINK_MAX; see Section 32.6 [Limits on File System Capacity], page 904.)" }
    };


    [DllImport("libc", EntryPoint = "link", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
    private static extern int Link(string oldpath, string newpath);

    internal static void CreateHardLink(string oldFileName,
                                        string newFileName)
    {
        int result = Link(oldFileName, newFileName);
        if (result != 0)
        {
            int errorCode = Marshal.GetLastPInvokeError();
            if (!_errorMessages.TryGetValue((LinkErrors)errorCode, out string? errorText))
            {
                errorText = "No error message defined";
            }
            throw new IOException(errorText, errorCode);
        }
    }
}
