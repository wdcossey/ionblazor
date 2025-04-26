namespace IonBlazor.Services;

public sealed class IonToastController: ComponentBase, IAsyncDisposable
{
    [Inject] private IJSRuntime JsRuntime { get; set; } = null!;

    private static IJSObjectReference _jsComponent = null!;

    public static ValueTask PresentAsync(Action<ToastControllerOptions> configure)
    {
        ToastControllerOptions options = new();
        configure(options);

        IEnumerable<IIonToastButton>? buttons = null;
        DotNetObjectReference<IonicEventCallback<JsonObject?>>? buttonHandler = null!;

        if (options.ButtonsBuilder is not null)
        {
            ToastButtonBuilder toastButtonBuilder = new();
            options.ButtonsBuilder.Invoke(toastButtonBuilder);
            buttons = toastButtonBuilder.Build();
            buttonHandler = IonicEventCallback<JsonObject?>.Create(
                async args =>
                {
                    var index = args?["index"]?.GetValue<int?>();
                    IIonToastButton? button = buttons?.ElementAtOrDefault(index ?? -1);
                    await (button?.Handler?.Invoke(new IonToastButtonEventArgs() { Sender = null, Button = button, Index = index}) ?? ValueTask.CompletedTask);
                    // ReSharper disable once AccessToModifiedClosure
                    buttonHandler?.Dispose();
                });
        }

        var didDismissHandler = IonicEventCallback<JsonObject?>.Create(args =>
        {
            options.OnDidDismiss?.Invoke(new IonToastDismissEventArgs
            {
                Sender = null,
                Role = args?["detail"]?["role"]?.GetValue<string>(),
            });

            return Task.CompletedTask;
        });

        return _jsComponent.InvokeVoidAsync("presentToast", options, buttons, buttonHandler, didDismissHandler);
    }

    public static ValueTask PresentAsync(
        string? header = null,
        string? message = null,
        string? position = null,
        TimeSpan? duration  = null,
        string? icon = null,
        string? positionAnchor = null,
        bool? translucent = null,
        bool? animated = null,
        ToastControllerOptions.ButtonBuilder? buttons = null,
        IDictionary<string, string>? htmlAttributes = null,
        Action<IonToastDismissEventArgs>? onDidDismiss = null)
    {
        var durationAsInt = (int?)duration?.TotalMilliseconds ?? 1500;
        return PresentAsync(options =>
        {
            options.Header = header;
            options.Message = message;
            options.Position = position;
            options.Duration = durationAsInt;
            options.Icon = icon;
            options.PositionAnchor = positionAnchor;
            options.Translucent = translucent;
            options.Animated = animated;
            options.ButtonsBuilder = buttons;
            options.HtmlAttributes = htmlAttributes;
            options.OnDidDismiss = onDidDismiss;
        });
    }

    public ValueTask DisposeAsync()
    {
        return _jsComponent.DisposeAsync();
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);

        if (!firstRender)
            return;

        _jsComponent = await JsRuntime.ImportAsync(nameof(IonToastController));
    }
}