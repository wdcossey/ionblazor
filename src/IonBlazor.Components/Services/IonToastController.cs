using System.Collections.Immutable;

namespace IonBlazor.Services;

public sealed class IonToastController: ComponentBase
{
    [Inject] private IJSRuntime JsRuntime { get; set; } = null!;

    private static IJSRuntime _jsRuntime = null!;

    public static async ValueTask PresentAsync(Action<ToastControllerOptions> configure)
    {
        ToastControllerOptions options = new();
        configure(options);

        IImmutableList<IIonToastButton>? buttons = null;
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

        await using IJSObjectReference jsComponent = await CreateComponentAsync();
        await jsComponent.InvokeVoidAsync("presentToast", options, buttons, buttonHandler, didDismissHandler);
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
        return PresentAsync(options =>
        {
            options.Header = header;
            options.Message = message;
            options.Position = position;
            options.Duration = (int?)duration?.TotalMilliseconds ?? 1500;
            options.Icon = icon;
            options.PositionAnchor = positionAnchor;
            options.Translucent = translucent;
            options.Animated = animated;
            options.ButtonsBuilder = buttons;
            options.HtmlAttributes = htmlAttributes;
            options.OnDidDismiss = onDidDismiss;
        });
    }

    protected override async Task OnParametersSetAsync()
    {
        await base.OnParametersSetAsync();
        _jsRuntime = JsRuntime;
    }

    private static async Task<IJSObjectReference> CreateComponentAsync()
    {
        IJSObjectReference result = await _jsRuntime.ImportAsync(nameof(IonToastController));

        if (result is null)
        {
            throw new InvalidOperationException($"{nameof(IonToastController)} is not initialized");
        }

        return result;
    }
}