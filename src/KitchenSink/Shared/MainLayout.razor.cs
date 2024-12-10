#if WINDOWS
using Windows.UI.ViewManagement;
using Microsoft.Maui.Platform;
#endif

namespace IonicTest.Shared;

public partial class MainLayout
{
    private IonMenu _mainMenu = null!;
#if WINDOWS
    private readonly UISettings _windowsUiSettings = new();
#endif

    protected override void OnInitialized()
    {
        base.OnInitialized();

#if WINDOWS
        _windowsUiSettings.ColorValuesChanged += (settings, e) =>
        {
            Color color = settings.GetColorValue(UIColorType.Accent).ToColor();
            _ = SetAccentColor(color);
        };
#endif

        NavigationManager.LocationChanged += async (object? sender, LocationChangedEventArgs e) =>
        {
            await _mainMenu.CloseAsync(true);
        };
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            Color? color = Application.AccentColor ?? Color.FromArgb("#FFCF40");
            await SetAccentColor(color);
        }

        await base.OnAfterRenderAsync(firstRender);
    }

    private async Task SetAccentColor(Color color)
    {
        color.ToRgb(out var red, out var green, out var blue);

        await JsRuntime.InvokeVoidAsync("document.body.style.setProperty", "--ion-color-primary", color.ToArgbHex());
        await JsRuntime.InvokeVoidAsync("document.body.style.setProperty", "--ion-color-primary-rgb", $"{red}, {green}, {blue}");
    }
}