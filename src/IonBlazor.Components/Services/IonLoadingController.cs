namespace IonBlazor.Services;

public sealed class IonLoadingController: ComponentBase
{
    private static IJSRuntime _jsRuntime = null!;

    [Inject]
    private IJSRuntime JsRuntime { get; init; } = null!;

    public static async ValueTask<string?> PresentAsync(Action<IonLoadingControllerOptions> configure)
    {
        IonLoadingControllerOptions options = new();
        configure(options);

        DotNetObjectReference<IonicEventCallback<JsonObject?>>? didDismissHandler = null;
        DotNetObjectReference<IonicEventCallback<JsonObject?>>? didPresentHandler = null;

        if (options.OnDidDismiss is not null)
        {
            didDismissHandler = IonicEventCallback<JsonObject?>.Create(args =>
            {
                options.OnDidDismiss.Invoke(new IonLoadingDismissEventArgs
                {
                    Sender = null,
                    Role = args?["detail"]?["role"]?.GetValue<string>(),
                    Data = args?["detail"]?["data"]?.Deserialize<JsonElement>(),
                    HtmlAttributes = args?["htmlAttributes"]?.Deserialize<Dictionary<string, string>>()
                });

                return Task.CompletedTask;
            });
        }

        if (options.OnDidPresent is not null)
        {
            didPresentHandler = IonicEventCallback<JsonObject?>.Create(args =>
            {
                options.OnDidPresent.Invoke(new IonLoadingPresentEventArgs()
                {
                    Sender = null,
                    HtmlAttributes = args?["htmlAttributes"]?.Deserialize<Dictionary<string, string>>()
                });
                return Task.CompletedTask;
            });
        }

        IJSObjectReference jsComponent = await IonLoadingReference.CreateComponentAsync(_jsRuntime);
        var result = await jsComponent.InvokeAsync<string?>("present", options, didDismissHandler, didPresentHandler);
        return result;
    }

    public static async ValueTask<string?> PresentAsync(
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

        IJSObjectReference jsComponent = await IonLoadingReference.CreateComponentAsync(_jsRuntime);
        var result = await jsComponent.InvokeAsync<string?>("present", message, duration, htmlAttributes, didDismissHandler, didPresentHandler);
        return result;
    }

    public static IonLoadingReference Create(Action<IonLoadingControllerOptions> configure)
    {
        return CreateAsync(configure).GetAwaiter().GetResult();
    }

    public static async Task<IonLoadingReference> CreateAsync(Action<IonLoadingControllerOptions> configure)
    {
        IonLoadingControllerOptions configuration = new();
        configure(configuration);

        IonLoadingReference result = new(_jsRuntime, configuration);
        await result.CreateAsync();
        return result;
    }

    protected override async Task OnParametersSetAsync()
    {
        await base.OnParametersSetAsync();
        _jsRuntime = JsRuntime;
    }
}