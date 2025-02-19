namespace IonBlazor.Services;

public sealed class IonLoadingController: ComponentBase, IAsyncDisposable
{
    [Inject] private IJSRuntime JsRuntime { get; set; } = null!;

    private static TaskCompletionSource<IJSObjectReference> _jsComponentCompletionSource = null!;

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

        IJSObjectReference jsComponent = await _jsComponentCompletionSource.Task;
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

        IJSObjectReference jsComponent = await _jsComponentCompletionSource.Task;
        IonLoadingReference result = new(jsComponent, configuration);
        return result;
    }

    public async ValueTask DisposeAsync()
    {
        if (_jsComponentCompletionSource.Task.IsCompleted)
        {
            IJSObjectReference jsComponent = await _jsComponentCompletionSource.Task;
            await jsComponent.DisposeAsync();
        }
    }

    protected override void OnInitialized()
    {
        base.OnInitialized();
        _jsComponentCompletionSource = new TaskCompletionSource<IJSObjectReference>();
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);

        if (!firstRender)
            return;

        IJSObjectReference jsComponent = await JsRuntime.ImportAsync(nameof(IonLoadingController));

        _jsComponentCompletionSource.SetResult(jsComponent);
    }
}