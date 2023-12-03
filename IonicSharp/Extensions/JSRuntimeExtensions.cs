// ReSharper disable once CheckNamespace
namespace IonicSharp;

// ReSharper disable once InconsistentNaming
internal static class JSRuntimeExtensions
{
    internal static ValueTask<IJSObjectReference> ImportAsync(this IJSRuntime jsRuntime, string fileName) => 
        jsRuntime.InvokeAsync<IJSObjectReference>("import", $"./_content/IonicSharp/{fileName}.js");

    internal static async ValueTask AttachIonListenersAsync(this IJSRuntime jsRuntime, ElementReference reference, params IonEvent[]? args)
    {
        await using var ionCommonJs = await jsRuntime.ImportAsync("common");
        await ionCommonJs.InvokeVoidAsync("attachListeners", args, reference);
    }
}