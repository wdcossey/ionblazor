#if WINDOWS
using Windows.UI.ViewManagement;
using Microsoft.Maui.Platform;
#endif

namespace IonicTest.Shared;

public partial class MainLayout
{
    private IonToggle _themeToggle = null!;
    private IonToggle _darkToggle = null!;
    private bool _useSystemTheme = false;
    private readonly Color _defaultColor = Color.FromRgb(112, 42, 247);

#if WINDOWS
    private readonly UISettings _windowsUiSettings = new();
#endif

    protected override void OnInitialized()
    {
        base.OnInitialized();

#if WINDOWS
        _windowsUiSettings.ColorValuesChanged += (settings, e) =>
        {
            if (_useSystemTheme is false)
                return;

            Color color = settings.GetColorValue(UIColorType.Accent).ToColor();
            _ = SetAccentColor(color);
        };
#endif
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            Color color = _useSystemTheme ? Application.AccentColor ?? _defaultColor : _defaultColor;
            await SetAccentColor(color);

            var @checked = await JsRuntime.InvokeAsync<bool>("prefersDarkColorScheme");
            _ = _darkToggle.SetChecked(@checked).AsTask();
        }

        await base.OnAfterRenderAsync(firstRender);
    }

#if WINDOWS
    private async Task SetAccentColor(Color color)
    {
        color.ToRgb(out var red, out var green, out var blue);
        await JsRuntime.InvokeVoidAsync("document.body.style.setProperty", "--ion-color-primary", color.ToArgbHex());
        await JsRuntime.InvokeVoidAsync("document.body.style.setProperty", "--ion-color-primary-rgb", $"{red}, {green}, {blue}");
    }
#endif


    private async Task DarkModeChange(IonToggleChangeEventArgs args)
    {
        await JsRuntime.InvokeVoidAsync("document.documentElement.classList.toggle", "ion-palette-dark", args.Checked);
    }

    private async Task ThemeChange(IonToggleChangeEventArgs args)
    {
        _useSystemTheme = args.Checked is true;

        Color color = args.Checked is true
            ? Application.AccentColor ?? _defaultColor
            : _defaultColor;

        await SetAccentColor(color);
    }
}