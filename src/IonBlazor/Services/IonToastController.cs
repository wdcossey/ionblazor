using IonBlazor.Components;

namespace IonBlazor.Services;

public class IonToastController: ComponentBase, IAsyncDisposable
{
    [Inject] private IJSRuntime JsRuntime { get; set; } = null!;
    
    private static IJSObjectReference _ionComponent = null!;
    
    public static ValueTask PresentAsync(
        string? header = null,
        string? message = null, 
        string? position = null, 
        int duration = 1500, 
        string? icon = null,
        string? positionAnchor = null,
        bool? translucent = null,
        bool? animated = null,
        Func<IEnumerable<IIonToastButton>>? buttonsFunc = null,
        IDictionary<string, string>? htmlAttributes = null)
    {
        IEnumerable<IIonToastButton>? buttons = null;
        DotNetObjectReference<IonicEventCallback<JsonObject?>>? buttonHandler = null!;
        
        if (buttonsFunc is not null)
        {
            buttons = buttonsFunc?.Invoke();
            buttonHandler = IonicEventCallback<JsonObject?>.Create(
                async args =>
                {
                    var index = args?["index"]?.GetValue<int?>();
                    var button = buttons?.ElementAtOrDefault(index ?? -1);
                    await (button?.Handler?.Invoke(new IonToastButtonEventArgs() { Sender = null, Button = button, Index = index}) ?? ValueTask.CompletedTask);
                    // ReSharper disable once AccessToModifiedClosure
                    buttonHandler?.Dispose();
                });
        }
        
        return _ionComponent.InvokeVoidAsync("presentToast", header, message, position, duration, icon, positionAnchor, buttons, buttonHandler, translucent, animated, htmlAttributes);
    }
    
    public static ValueTask PresentAsync(
        string? header = null,
        string? message = null,
        string? position = null, 
        TimeSpan? duration  = null, 
        string? icon = null,
        string? positionAnchor = null,
        bool? translucent = null,
        bool? animated = null,
        Func<IEnumerable<IIonToastButton>>? buttonsFunc = null,
        IDictionary<string, string>? htmlAttributes = null)
    {
        var durationAsInt = (int?)duration?.TotalMilliseconds ?? 1500;
        return PresentAsync(
            header: header, 
            message: message, 
            position: position, 
            duration: durationAsInt, 
            icon: icon, 
            positionAnchor: positionAnchor,
            translucent: translucent, 
            animated: animated,
            buttonsFunc: buttonsFunc, 
            htmlAttributes: htmlAttributes);
    }

    public ValueTask DisposeAsync()
    {
        GC.SuppressFinalize(this);
        return _ionComponent.DisposeAsync();
    }
    
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);

        if (!firstRender)
            return;

        _ionComponent = await JsRuntime.ImportAsync("toastController");
    }
}