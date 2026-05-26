namespace IonBlazor.Services;

/// <summary>
/// Scoped DI service for programmatically presenting an Ionic loading overlay.
/// Register via <c>services.AddIonBlazor()</c> and inject with <c>@inject IonLoadingService</c>.
/// </summary>
public sealed class IonLoadingService(IJSRuntime jsRuntime)
{
    /// <summary>
    /// Presents a loading overlay configured by the supplied <see cref="IonLoadingControllerOptions"/> action.
    /// </summary>
    public async ValueTask<string?> PresentAsync(Action<IonLoadingControllerOptions> configure)
    {
        IonLoadingControllerOptions options = new();
        configure(options);

        DotNetObjectReference<IonicEventCallback<JsonObject?>>? didDismissHandler = null;
        DotNetObjectReference<IonicEventCallback<JsonObject?>>? didPresentHandler = null;

        didDismissHandler = IonicEventCallback<JsonObject?>.Create(async args =>
        {
            await options.InvokeOnDidDismiss(null, new IonLoadingDismissEventArgs
            {
                Sender = null,
                Role = args?["detail"]?["role"]?.GetValue<string>(),
                Data = args?["detail"]?["data"]?.Deserialize<JsonElement>(),
                HtmlAttributes = args?["htmlAttributes"]?.Deserialize<Dictionary<string, string>>()
            });
        });

        didPresentHandler = IonicEventCallback<JsonObject?>.Create(async args =>
        {
            await options.InvokeOnDidPresent(null, new IonLoadingPresentEventArgs
            {
                Sender = null,
                HtmlAttributes = args?["htmlAttributes"]?.Deserialize<Dictionary<string, string>>()
            });
        });

        IJSObjectReference jsComponent = await IonLoadingReference.CreateComponentAsync(jsRuntime);
        var result = await jsComponent.InvokeAsync<string?>("present", options, didDismissHandler, didPresentHandler);
        return result;
    }

    /// <summary>
    /// Presents a loading overlay with the supplied scalar parameters and inline callbacks.
    /// </summary>
    public async ValueTask<string?> PresentAsync(
        string? message = null,
        int? duration = 3000,
        IDictionary<string, string>? htmlAttributes = null,
        Action<IonLoadingDismissEventArgs>? onDidDismiss = null,
        Action? onDidPresent = null)
    {
        DotNetObjectReference<IonicEventCallback<JsonObject?>>? didDismissHandler = null;
        DotNetObjectReference<IonicEventCallback<JsonObject?>>? didPresentHandler = null;

        if (onDidDismiss is not null)
        {
            didDismissHandler = IonicEventCallback<JsonObject?>.Create(args =>
            {
                onDidDismiss.Invoke(new IonLoadingDismissEventArgs
                {
                    Sender = null,
                    Role = args?["detail"]?["role"]?.GetValue<string>(),
                    Data = args?["detail"]?["data"]?.Deserialize<JsonElement>(),
                    HtmlAttributes = args?["htmlAttributes"]?.Deserialize<Dictionary<string, string>>()
                });

                return Task.CompletedTask;
            });
        }

        if (onDidPresent is not null)
        {
            didPresentHandler = IonicEventCallback<JsonObject?>.Create(args =>
            {
                onDidPresent.Invoke();
                return Task.CompletedTask;
            });
        }

        IJSObjectReference jsComponent = await IonLoadingReference.CreateComponentAsync(jsRuntime);
        var result = await jsComponent.InvokeAsync<string?>("present", message, duration, htmlAttributes, didDismissHandler, didPresentHandler);
        return result;
    }

    /// <summary>
    /// Creates an <see cref="IonLoadingReference"/> that can be presented and dismissed independently.
    /// </summary>
    public async Task<IonLoadingReference> CreateAsync(Action<IonLoadingControllerOptions> configure)
    {
        IonLoadingControllerOptions configuration = new();
        configure(configuration);

        IonLoadingReference result = new(jsRuntime, configuration);
        await result.CreateAsync();
        return result;
    }
}
