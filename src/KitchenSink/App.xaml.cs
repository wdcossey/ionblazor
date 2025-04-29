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

#if WINDOWS
        //window.TitleBar = new TitleBar
        //{
        //    BackgroundColor = Application.AccentColor,
        //    ForegroundColor = Colors.White,
        //    Title = "IonBlazor",
        //    Subtitle = "Demo",
        //    HeightRequest = 48,
        //    //Content = new SearchBar()
        //    //{
        //    //    Placeholder = "Search",
        //    //    MaximumWidthRequest = 300D,
        //    //    HorizontalOptions =  LayoutOptions.FillAndExpand,
        //    //    VerticalOptions = LayoutOptions.Center
        //    //}
        //};
#endif

        return window;
    }
}