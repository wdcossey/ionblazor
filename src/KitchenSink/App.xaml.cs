namespace IonicTest;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();
    }

    protected override Window CreateWindow(IActivationState? activationState)
    {
        Page page = new MainPage();
        Window window = new(page);
        return window;
    }
}