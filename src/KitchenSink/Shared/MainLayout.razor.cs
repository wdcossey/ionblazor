#if WINDOWS
using Windows.UI.ViewManagement;
using Microsoft.Maui.Platform;
#endif

namespace IonicTest.Shared;

public partial class MainLayout
{
    private IonToggle _darkToggle = null!;
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

        NavigationManager.LocationChanged += (object? sender, LocationChangedEventArgs e) =>
        {

        };
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            Color? color = Application.AccentColor ?? Color.FromArgb("#FFCF40");
            await SetAccentColor(color);

            var @checked = await JsRuntime.InvokeAsync<bool>("prefersDarkColorScheme");
            _ = _darkToggle.SetChecked(@checked).AsTask();
        }

        await base.OnAfterRenderAsync(firstRender);
    }

    private async Task SetAccentColor(Color color)
    {
        color.ToRgb(out var red, out var green, out var blue);

        await JsRuntime.InvokeVoidAsync("document.body.style.setProperty", "--ion-color-primary", color.ToArgbHex());
        await JsRuntime.InvokeVoidAsync("document.body.style.setProperty", "--ion-color-primary-rgb", $"{red}, {green}, {blue}");
    }


    private void DarkModeChange(IonToggleChangeEventArgs args)
    {
        JsRuntime.InvokeVoidAsync("document.documentElement.classList.toggle", "ion-palette-dark", args.Checked);
    }
}