namespace IonBlazor.Services;

public sealed class IonLoadingController: ComponentBase, IAsyncDisposable
{
    [Inject] private IJSRuntime JsRuntime { get; set; } = null!;

    private static IJSObjectReference? _jsComponent;

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

        var result = await (_jsComponent?.InvokeAsync<string?>("present", message, duration, htmlAttributes, didDismissHandler, didPresentHandler) ?? ValueTask.FromResult<string?>(null));
        return result;
    }

    public static IonLoadingReference Create(Action<IonLoadingReferenceConfiguration> configure)
    {
        IonLoadingReferenceConfiguration configuration = new();
        configure(configuration);

        IonLoadingReference result = new(_jsComponent, configuration);
        return result;
    }

    public async ValueTask DisposeAsync()
    {
        await (_jsComponent?.DisposeAsync() ?? ValueTask.CompletedTask);
        _jsComponent = null;
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);

        if (!firstRender)
            return;

        _jsComponent = await JsRuntime.ImportAsync(nameof(IonLoadingController));
    }
}