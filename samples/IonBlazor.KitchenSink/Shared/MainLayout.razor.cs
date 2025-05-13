#if WINDOWS
using Windows.UI.ViewManagement;
using Microsoft.Maui.Platform;
#endif

namespace IonicTest.Shared;

public partial class MainLayout
{

    private readonly Dictionary<bool, string> _systemThemeIconNames = new()
    {
        { true, "color-palette" },
        { false, "color-palette-outline" }
    };

    private readonly Dictionary<bool, string> _darkThemeIconNames = new()
    {
        { true, "sunny-outline" },
        { false, "moon-outline" }
    };

    private string? _systemThemeIconName;
    private string? _themeIconName;
    private bool _useSystemTheme;
    private bool _isDarkMode;
    private readonly Color _defaultColor = Color.FromRgb(112, 42, 247);

    private IonContent _mainContent = null!;

#if WINDOWS
    private readonly UISettings _windowsUiSettings = new();
#endif

    [Inject] private IJSRuntime JsRuntime { get; init; } = null!;
    [Inject] private NavigationManager NavigationManager { get; init; } = null!;

    protected override void OnInitialized()
    {
        base.OnInitialized();

        NavigationManager.LocationChanged += (sender, args) =>
        {
            _ = _mainContent.ScrollToTopAsync(500).AsTask();
        };

#if WINDOWS
        _windowsUiSettings.ColorValuesChanged += (settings, e) =>
        {
            if (_useSystemTheme is false)
                return;

            var color = settings.GetColorValue(UIColorType.Accent).ToColor();
            _ = SetAccentColor(color);
        };
#endif
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);

        if (!firstRender)
        {
            return;
        }

        var color = _useSystemTheme ? Application.AccentColor ?? _defaultColor : _defaultColor;
        _systemThemeIconName = _systemThemeIconNames[_useSystemTheme];
        await SetAccentColor(color);

        _isDarkMode = await JsRuntime.InvokeAsync<bool>("prefersDarkColorScheme");
        _themeIconName = _darkThemeIconNames[_isDarkMode];

        StateHasChanged();
    }

#if WINDOWS
    private async Task SetAccentColor(Color color)
    {
        color.ToRgb(out var red, out var green, out var blue);
        await JsRuntime.InvokeVoidAsync("document.body.style.setProperty", "--ion-color-primary", color.ToArgbHex());
        await JsRuntime.InvokeVoidAsync("document.body.style.setProperty", "--ion-color-primary-rgb", $"{red}, {green}, {blue}");
    }
#endif


    private async Task ToggleDarkMode()
    {
        _isDarkMode = !_isDarkMode;
        _themeIconName = _darkThemeIconNames[_isDarkMode];
        await JsRuntime.InvokeVoidAsync("document.documentElement.classList.toggle", "ion-palette-dark", _isDarkMode);
    }

    private async Task ToggleSystemTheme()
    {
        _useSystemTheme = !_useSystemTheme;
        _systemThemeIconName = _systemThemeIconNames[_useSystemTheme];

        var color = _useSystemTheme
            ? Application.AccentColor ?? _defaultColor
            : _defaultColor;

        await SetAccentColor(color);
    }
}