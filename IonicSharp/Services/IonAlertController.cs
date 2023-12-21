using System.Collections;
using Microsoft.JSInterop.Implementation;

namespace IonicSharp.Services;

public class IonAlertController: ComponentBase, IAsyncDisposable
{
    [Inject] private IJSRuntime JsRuntime { get; set; } = null!;
    
    private static IJSObjectReference? _ionComponent;
    
    public static async ValueTask PresentAsync(
        string? header = null, 
        string? subHeader = null, 
        string? message = null,
        Func<IEnumerable<AlertButton>>? buttonsFunc = null,
        IDictionary<string, string> htmlAttributes = null!)
    {
        IEnumerable<AlertButton>? buttons = null;
        DotNetObjectReference<IonicEventCallback<JsonObject?>>? buttonHandler = null!;
        
        if (buttonsFunc is not null)
        {
            buttons = buttonsFunc?.Invoke();
            buttonHandler = IonicEventCallback<JsonObject?>.Create(
                async args =>
                {
                    var index = args?["index"]?.GetValue<int?>();
                    var button = buttons?.ElementAtOrDefault(index ?? -1);
                    await (button?.Handler?.Invoke(new AlertButtonEventArgs() { Sender = null, Button = button, Index = index}) ?? ValueTask.CompletedTask);
                    // ReSharper disable once AccessToModifiedClosure
                    buttonHandler?.Dispose();
                });
        }
        
        await (_ionComponent?.InvokeVoidAsync("presentAlert", header, subHeader, message, buttons, buttonHandler, htmlAttributes) ?? ValueTask.CompletedTask);
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

        _ionComponent = await JsRuntime.ImportAsync("alertController");
    }
}