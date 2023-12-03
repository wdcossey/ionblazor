namespace IonicSharp.Services;

public class IonActionSheetController : IComponent, IAsyncDisposable
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

        try
        {
            await (_ionComponent?.InvokeVoidAsync("presentActionSheet", header, buttons, buttonHandler) ?? ValueTask.CompletedTask);
        }
        finally
        {
            //buttonHandler?.Dispose();
        }
    }

    public async ValueTask DisposeAsync() => await (_ionComponent?.DisposeAsync() ?? ValueTask.CompletedTask);

    public void Attach(RenderHandle renderHandle) { }
    
    public async Task SetParametersAsync(ParameterView parameters) => _ionComponent ??= await JsRuntime.ImportAsync("actionSheetController");
}