namespace IonBlazor.Services;

public sealed class IonLoadingController: ComponentBase, IAsyncDisposable
{
    [Inject] private IJSRuntime JsRuntime { get; set; } = null!;

    private static IJSObjectReference? _ionComponent;

    public static async ValueTask PresentAsync(
        string? message = null,
        int duration = 3000,
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



        await (_ionComponent?.InvokeVoidAsync("presentLoading", message, duration, htmlAttributes, didDismissHandler, didPresentHandler) ?? ValueTask.CompletedTask);
    }

    public async ValueTask DisposeAsync()
    {
        await (_ionComponent?.DisposeAsync() ?? ValueTask.CompletedTask);
        _ionComponent = null;
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);

        if (!firstRender)
            return;

        _ionComponent = await JsRuntime.ImportAsync("loadingController");
    }
}