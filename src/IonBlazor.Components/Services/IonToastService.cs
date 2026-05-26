using System.Collections.Immutable;

namespace IonBlazor.Services;

/// <summary>
/// Scoped DI service for programmatically presenting an Ionic toast.
/// Register via <c>services.AddIonBlazor()</c> and inject with <c>@inject IonToastService</c>.
/// </summary>
public sealed class IonToastService(IJSRuntime jsRuntime)
{
    /// <summary>
    /// Presents a toast configured by the supplied <see cref="ToastControllerOptions"/> action.
    /// </summary>
    public async ValueTask PresentAsync(Action<ToastControllerOptions> configure)
    {
        ToastControllerOptions options = new();
        configure(options);

        IImmutableList<IIonToastButton>? buttons = null;
        DotNetObjectReference<IonicEventCallback<JsonObject?>>? buttonHandler = null;

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
                    await (button?.Handler?.Invoke(new IonToastButtonEventArgs { Sender = null, Button = button, Index = index }) ?? ValueTask.CompletedTask);
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

        await using IJSObjectReference jsComponent = await jsRuntime.ImportAsync(nameof(IonToastService));
        await jsComponent.InvokeVoidAsync("presentToast", options, buttons, buttonHandler, didDismissHandler);
    }

    /// <summary>
    /// Presents a toast with the supplied scalar parameters. Convenience wrapper around
    /// <see cref="PresentAsync(System.Action{ToastControllerOptions})"/>.
    /// </summary>
    public ValueTask PresentAsync(
        string? header = null,
        string? message = null,
        string? position = null,
        TimeSpan? duration = null,
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
}
