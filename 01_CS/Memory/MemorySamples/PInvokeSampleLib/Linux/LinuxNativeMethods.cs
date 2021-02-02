using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;

namespace PInvokeSample
{
    //# include <unistd.h>

    //    int link(const char* oldpath, const char* newpath);
    /*
     EACCES You are not allowed to write to the directory in which the new link is to
    be written.
    EEXIST There is already a file named newname. If you want to replace this link
    with a new link, you must remove the old link explicitly first.
    EMLINK There are already too many links to the file named by oldname. (The
    maximum number of links to a file is LINK_MAX; see Section 32.6 [Limits
    on File System Capacity], page 904.)
    ENOENT The file named by oldname doesn’t exist. You can’t make a link to a file
    that doesn’t exist.
    ENOSPC The directory or file system that would contain the new link is full and
    cannot be extended.
    Chapter 14: File System Interface 419
    EPERM On GNU/Linux and GNU/Hurd systems and some others, you cannot
    make links to directories. Many systems allow only privileged users to do
    so. This error is used to report the problem.
    EROFS The directory containing the new link can’t be modified because it’s on
    a read-only file system.
    EXDEV The directory specified in newname is on a different file system than the
    existing file.
    EIO A hardware error occurred while trying to read or write the to filesystem.

     */


    [SupportedOSPlatform("Linux")]
    internal static class LinuxNativeMethods
    {
        private static Dictionary<int, string> _errorMessages = new()
        {
            { 1, "On GNU/Linux and GNU/Hurd systems and some others, you cannot make links to directories.Many systems allow only privileged users to do so." },
            { 2, "The file named by oldname doesn’t exist. You can’t make a link to a file that doesn’t exist." },
            { 5, "A hardware error occurred while trying to read or write the to filesystem." },
            { 13, "You are not allowed to write to the directory in which the new link is to be written." },
            { 17, "There is already a file named newname. If you want to replace this link with a new link, you must remove the old link explicitly first." },
            { 18, "The directory specified in newname is on a different file system than the existing file." },
            { 28, "The directory or file system that would contain the new link is full and cannot be extended." } ,
            { 30, "The directory containing the new link can’t be modified because it’s on a read - only file system." },
            { 31, "There are already too many links to the file named by oldname. (The maximum number of links to a file is LINK_MAX; see Section 32.6 [Limits on File System Capacity], page 904.)" }
        };
        internal enum LinkErrors
        {
            EACCES = 13,
            EEXIST = 17,
            EMLINK = 31,
            ENOENT = 2,
            ENOSPC= 28,
            EPERM = 1,
            EROFS = 30,
            EXDEV = 18,
            EIO = 5
        }

        [DllImport("libc")]
        private static extern int link(string oldpath, string newpath);


        internal static void CreateHardLink(string oldFileName,
                                            string newFileName)
        {
            int result = link(newFileName, oldFileName);
            if (result != 0)
            {
                if (!_errorMessages.TryGetValue(result, out string? errorText))
                {
                    errorText = "No error message defined";
                }
                throw new IOException(errorText, result);
            }
        }
    }
}
