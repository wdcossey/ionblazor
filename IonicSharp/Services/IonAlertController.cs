using System.Collections;

namespace IonicSharp.Services;

public class IonAlertController: IComponent, IAsyncDisposable
{
    [Inject] private IJSRuntime JsRuntime { get; set; } = null!;
    
    private static IJSObjectReference? _ionComponent;
    
    public static async ValueTask PresentAsync(
        string? header = null, 
        string? subHeader = null, 
        string? message = null, 
        Func<IEnumerable>? buttonsFunc = null)
    {
        await (_ionComponent?.InvokeVoidAsync("presentAlert", header, subHeader, message) ?? ValueTask.CompletedTask);
    }

    public async ValueTask DisposeAsync() => await (_ionComponent?.DisposeAsync() ?? ValueTask.CompletedTask);

    public void Attach(RenderHandle renderHandle) { }
    
    public async Task SetParametersAsync(ParameterView parameters) => _ionComponent ??= await JsRuntime.ImportAsync("alertController");
}