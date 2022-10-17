using Microsoft.UI.Xaml;

using System.Runtime.InteropServices;
using System.Text;

using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.Storage.Streams;
using Windows.UI.Popups;

using WinRT;
using WinRT.Interop;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace WinUIAppEditor;

/// <summary>
/// An empty window that can be used on its own or navigated to within a Frame.
/// </summary>
public sealed partial class MainWindow : Window
{
    public MainWindow() => InitializeComponent();

    public async void OnOpen()
    {
        try
        {
            FileOpenPicker picker = new()
            {
                ViewMode = PickerViewMode.Thumbnail,
                SuggestedStartLocation = PickerLocationId.DocumentsLibrary
            };
            picker.FileTypeFilter.Add(".txt");
            picker.FileTypeFilter.Add(".md");

            IntPtr hwnd = WindowNative.GetWindowHandle(this);
            InitializeWithWindow.Initialize(picker, hwnd);

            StorageFile file = await picker.PickSingleFileAsync();
            if (file != null)
            {
                IRandomAccessStreamWithContentType stream = await file.OpenReadAsync();
                using DataReader reader = new(stream);
                await reader.LoadAsync((uint)stream.Size);

                text1.Text = reader.ReadString((uint)stream.Size);
            }
        }
        catch (Exception ex)
        {
            MessageDialog dlg = new(ex.Message, "Error");
            await dlg.ShowAsync();
        }
    }

    public async void OnOpenDotnet()
    {
        try
        {
            FileOpenPicker picker = new()
            {
                ViewMode = PickerViewMode.Thumbnail,
                SuggestedStartLocation = PickerLocationId.DocumentsLibrary
            };
            picker.FileTypeFilter.Add(".txt");
            picker.FileTypeFilter.Add(".md");

            IntPtr hwnd = WindowNative.GetWindowHandle(this);
            InitializeWithWindow.Initialize(picker, hwnd);

            StorageFile file = await picker.PickSingleFileAsync();
            if (file != null)
            {
                IRandomAccessStreamWithContentType wrtStream = await file.OpenReadAsync();
                Stream stream = wrtStream.AsStreamForRead();
                StreamReader reader = new(stream);
                text1.Text = await reader.ReadToEndAsync();
            }
        }
        catch (Exception ex)
        {
            MessageDialog dlg = new(ex.Message, "Error");
            await dlg.ShowAsync();
        }
    }

    public async void OnSaveDotnet()
    {
        try
        {
            FileSavePicker picker = new()
            {
                SuggestedStartLocation = PickerLocationId.DocumentsLibrary,
                SuggestedFileName = "New Document"
            };
            picker.FileTypeChoices.Add("Plain Text", new List<string>() { ".txt" });

            IntPtr hwnd = WindowNative.GetWindowHandle(this);
            InitializeWithWindow.Initialize(picker, hwnd);

            StorageFile file = await picker.PickSaveFileAsync();
            if (file != null)
            {
                using StorageStreamTransaction tx = await file.OpenTransactedWriteAsync();
                Stream stream = tx.Stream.AsStreamForWrite();
                using var writer = new StreamWriter(stream);
                byte[] preamble = Encoding.UTF8.GetPreamble();
                await stream.WriteAsync(preamble, 0, preamble.Length);
                await writer.WriteAsync(text1.Text);
                await writer.FlushAsync();
                tx.Stream.Size = (ulong)stream.Length;
                await tx.CommitAsync();
            }
        }
        catch (Exception ex)
        {
            MessageDialog dlg = new(ex.Message, "Error");
            await dlg.ShowAsync();
        }
    }

    public async void OnSave()
    {
        try
        {
            FileSavePicker picker = new()
            {
                SuggestedStartLocation = PickerLocationId.DocumentsLibrary,
                SuggestedFileName = "New Document"
            };
            picker.FileTypeChoices.Add("Plain Text", new List<string>() { ".txt" });

            IntPtr hwnd = WindowNative.GetWindowHandle(this);
            InitializeWithWindow.Initialize(picker, hwnd);

            StorageFile file = await picker.PickSaveFileAsync();
            if (file != null)
            {
                using StorageStreamTransaction tx = await file.OpenTransactedWriteAsync();
                IRandomAccessStream stream = tx.Stream;
                stream.Seek(0);
                using DataWriter writer = new(stream);
                writer.WriteString(text1.Text);
                tx.Stream.Size = await writer.StoreAsync();
                await tx.CommitAsync();
            }
        }
        catch (Exception ex)
        {
            MessageDialog dlg = new(ex.Message, "Error");
            await dlg.ShowAsync();
        }
    }

    public void OnClose()
    {
        App.Current.Exit();
    }
}
