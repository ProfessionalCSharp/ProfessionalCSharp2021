using BooksLib.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Popups;
using WinRT;

namespace BooksApp.Services
{
    public class WinUIDialogService : IDialogService
    {
        public async Task ShowMessageAsync(string message)
        {
            MessageDialog dlg = new(message);

            // temporary workaround https://github.com/microsoft/microsoft-ui-xaml/issues/4167
            var handle = GetActiveWindow();
            if (handle == IntPtr.Zero)
            {
                throw new InvalidOperationException();
            }
            dlg.As<IInitializeWithWindow>().Initialize(handle);
            await dlg.ShowAsync();
        }

        [DllImport("user32.dll")]
        private static extern IntPtr GetActiveWindow();
    }

    [ComImport]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [Guid("3E68D4BD-7135-4D10-8018-9FB6D9F33FA1")]
    internal interface IInitializeWithWindow
    {
        void Initialize(IntPtr hwnd);
    }

    [ComImport]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [Guid("EECDBF0E-BAE9-4CB6-A68E-9598E1CB57BB")]
    internal interface IWindowNative
    {
        IntPtr WindowHandle { get; }
    }


}
