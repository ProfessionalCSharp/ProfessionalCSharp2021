namespace BooksApp;

public sealed partial class MainWindow : Window
{
    public MainWindow()
    {
        this.InitializeComponent();
        this.SizeChanged += OnSizeChanged;
        ViewModel = Ioc.Default.GetRequiredService<MainWindowViewModel>();
        ViewModel.SetNavigationFrame(MainFrame);
    }

    private void OnSizeChanged(object sender, WindowSizeChangedEventArgs args)
    {
        double width = args.Size.Width;
        NavigationMessage navigation = new(new()
        {
            UseNavigation = width < 1024
        });
        WeakReferenceMessenger.Default.Send(navigation);
    }

    public MainWindowViewModel ViewModel { get; }
}
