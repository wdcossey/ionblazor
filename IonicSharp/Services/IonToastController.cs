using System.Collections;

namespace IonicSharp.Services;

public class IonToastController: IComponent, IAsyncDisposable
{
    [Inject] private IJSRuntime JsRuntime { get; set; } = null!;
    
    private static IJSObjectReference? _ionComponent;
    
    public static async ValueTask PresentAsync(
        string message, 
        string? position, 
        int duration = 1500, 
        string? icon = null,
        string? positionAnchor = null, 
        Func<IEnumerable>? buttonsFunc = null)
    {
        await (_ionComponent?.InvokeVoidAsync("presentToast", message, position, duration, icon, positionAnchor) ?? ValueTask.CompletedTask);
    }
    
    public static async ValueTask PresentAsync(
        string message, 
        string? position, 
        TimeSpan? duration, 
        string? icon = null,
        string? positionAnchor = null, 
        Func<IEnumerable>? buttonsFunc = null)
    {
        var durationAsInt = (int?)duration?.TotalMilliseconds ?? 1500;
        await PresentAsync(
            message: message, 
            position: position, 
            duration: durationAsInt, 
            icon: icon, 
            positionAnchor: positionAnchor);
    }

    public async ValueTask DisposeAsync() => await (_ionComponent?.DisposeAsync() ?? ValueTask.CompletedTask);

    public void Attach(RenderHandle renderHandle) { }
    
    public async Task SetParametersAsync(ParameterView parameters) => _ionComponent ??= await JsRuntime.ImportAsync("toastController");
}