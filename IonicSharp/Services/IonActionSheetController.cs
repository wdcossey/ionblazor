namespace IonicSharp.Services;

public class IonActionSheetController : ComponentBase, IAsyncDisposable
{
    [Inject] private IJSRuntime JsRuntime { get; set; } = null!;
    
    private static IJSObjectReference? _ionComponent;
    
    public static async ValueTask PresentAsync<TButtonData>(
        string? header = null, 
        Func<IEnumerable<ActionSheetButton<TButtonData>>>? buttonsFunc = null)
        where TButtonData : class, IActionSheetButtonData
    {
        IEnumerable<ActionSheetButton<TButtonData>>? buttons = null;
        DotNetObjectReference<IonicEventCallback<JsonObject?>>? buttonHandler = null!;
        
        if (buttonsFunc is not null)
        {
            buttons = buttonsFunc?.Invoke();
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

        await (_ionComponent?.InvokeVoidAsync("presentActionSheet", header, buttons, buttonHandler) ?? ValueTask.CompletedTask);
    }

    public async ValueTask DisposeAsync()
    {
        GC.SuppressFinalize(this);
        await (_ionComponent?.DisposeAsync() ?? ValueTask.CompletedTask);
        _ionComponent = null;
    }
    
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);

        if (!firstRender)
            return;

        _ionComponent = await JsRuntime.ImportAsync("actionSheetController");
    }
}