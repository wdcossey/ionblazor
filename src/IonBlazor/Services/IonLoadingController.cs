namespace IonBlazor.Services;

public sealed class IonLoadingController: ComponentBase
{
    private static IJSRuntime _jsRuntime = null!;

    [Inject]
    private IJSRuntime JsRuntime { get; set; } = null!;

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
                onDidDismiss?.Invoke(new IonLoadingDismissEventArgs
                {
                    Sender = null,
                    Role = args?["detail"]?["role"]?.GetValue<string>(),
                    Data = args?["detail"]?["data"],
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

        IJSObjectReference jsComponent = await _jsRuntime.ImportAsync(nameof(IonLoadingController));
        var result = await jsComponent.InvokeAsync<string?>("present", message, duration, htmlAttributes, didDismissHandler, didPresentHandler);
        return result;
    }

    public static IonLoadingReference Create(Action<IonLoadingReferenceConfiguration> configure)
    {
        return CreateAsync(configure).GetAwaiter().GetResult();
    }

    public static async Task<IonLoadingReference> CreateAsync(Action<IonLoadingReferenceConfiguration> configure)
    {
        IonLoadingReferenceConfiguration configuration = new();
        configure(configuration);

        IJSObjectReference jsComponent = await _jsRuntime.ImportAsync(nameof(IonLoadingController));
        IonLoadingReference result = new(jsComponent, configuration);
        await result.CreateAsync();
        return result;
    }

    protected override void OnParametersSet()
    {
        base.OnParametersSet();
        _jsRuntime = JsRuntime;
    }
}