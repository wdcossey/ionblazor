namespace IonBlazor.Services;

public sealed class IonToastController: ComponentBase, IAsyncDisposable
{
    [Inject] private IJSRuntime JsRuntime { get; set; } = null!;

    private static IJSObjectReference _jsComponent = null!;

    public static ValueTask PresentAsync(
        string? header = null,
        string? message = null,
        string? position = null,
        int duration = 1500,
        string? icon = null,
        string? positionAnchor = null,
        bool? translucent = null,
        bool? animated = null,
        Func<IEnumerable<IIonToastButton>>? buttonsFunc = null,
        IDictionary<string, string>? htmlAttributes = null,
        Action<IonToastDismissEventArgs>? onDidDismiss = null)
    {
        IEnumerable<IIonToastButton>? buttons = null;
        DotNetObjectReference<IonicEventCallback<JsonObject?>>? buttonHandler = null!;

        if (buttonsFunc is not null)
        {
            buttons = buttonsFunc?.Invoke();
            buttonHandler = IonicEventCallback<JsonObject?>.Create(
                async args =>
                {
                    var index = args?["index"]?.GetValue<int?>();
                    var button = buttons?.ElementAtOrDefault(index ?? -1);
                    await (button?.Handler?.Invoke(new IonToastButtonEventArgs() { Sender = null, Button = button, Index = index}) ?? ValueTask.CompletedTask);
                    // ReSharper disable once AccessToModifiedClosure
                    buttonHandler?.Dispose();
                });
        }

        var didDismissHandler = IonicEventCallback<JsonObject?>.Create(args =>
        {
            onDidDismiss?.Invoke(new IonToastDismissEventArgs
            {
                Sender = null,
                Role = args?["detail"]?["role"]?.GetValue<string>(),
            });

            return Task.CompletedTask;
        });

        return _jsComponent.InvokeVoidAsync("presentToast", header, message, position, duration, icon, positionAnchor, buttons, buttonHandler, translucent, animated, htmlAttributes, didDismissHandler);
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
        Func<IEnumerable<IIonToastButton>>? buttonsFunc = null,
        IDictionary<string, string>? htmlAttributes = null,
        Action<IonToastDismissEventArgs>? onDidDismiss = null)
    {
        var durationAsInt = (int?)duration?.TotalMilliseconds ?? 1500;
        return PresentAsync(
            header: header,
            message: message,
            position: position,
            duration: durationAsInt,
            icon: icon,
            positionAnchor: positionAnchor,
            translucent: translucent,
            animated: animated,
            buttonsFunc: buttonsFunc,
            htmlAttributes: htmlAttributes,
            onDidDismiss: onDidDismiss);
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