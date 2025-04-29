namespace IonBlazor.Services;

public sealed class IonActionSheetController : ComponentBase, IAsyncDisposable
{
    [Inject] private IJSRuntime JsRuntime { get; set; } = null!;

    private static IJSObjectReference? _jsComponent;

    public static async ValueTask PresentAsync(Action<ActionSheetControllerOptions> configure)
    {
        ActionSheetControllerOptions options = new();
        configure(options);

        IEnumerable<IActionSheetButton>? buttons = null;
        DotNetObjectReference<IonicEventCallback<JsonObject?>>? buttonHandler = null!;

        if (options.ButtonsBuilder is not null)
        {
            ActionSheetButtonBuilder buttonBuilder = new();
            options.ButtonsBuilder.Invoke(buttonBuilder);
            buttons = buttonBuilder.Build();

            buttonHandler = IonicEventCallback<JsonObject?>.Create(
                async args =>
                {
                    var index = args?["index"]?.GetValue<int?>();
                    var button = buttons?.ElementAtOrDefault(index ?? -1);
                    await (button?.Handler?.Invoke(button, index) ?? ValueTask.CompletedTask);
                    // ReSharper disable once AccessToModifiedClosure
                    buttonHandler?.Dispose();
                });
        }

        await (_jsComponent?.InvokeVoidAsync("present", options, buttons, buttonHandler) ?? ValueTask.CompletedTask);
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

        _jsComponent = await JsRuntime.ImportAsync(nameof(IonActionSheetController));
    }
}