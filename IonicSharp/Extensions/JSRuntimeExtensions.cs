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

    internal static async ValueTask InvokeVoidAsync(this Lazy<ValueTask<IJSObjectReference>> lazyRef, string identifier, params object?[]? args)
    {
        var ionCommonJs = await lazyRef.Value;
        await ionCommonJs.InvokeVoidAsync(identifier, args);
    }

    internal static async ValueTask InvokeVoidAsync(this Lazy<ValueTask<IJSObjectReference>> lazyRef, string identifier, CancellationToken cancellationToken, params object?[]? args)
    {
        var ionCommonJs = await lazyRef.Value;
        await ionCommonJs.InvokeVoidAsync(identifier, cancellationToken, args);
    }

    internal static async ValueTask InvokeVoidAsync(this Lazy<ValueTask<IJSObjectReference>> lazyRef, string identifier, TimeSpan timeout, params object?[]? args)
    {
        var ionCommonJs = await lazyRef.Value;
        await ionCommonJs.InvokeVoidAsync(identifier, timeout, args);
    }
    
    internal static async ValueTask<TValue> InvokeAsync<TValue>(this Lazy<ValueTask<IJSObjectReference>> lazyRef, string identifier, params object?[]? args)
    {
        var ionCommonJs = await lazyRef.Value;
        return await ionCommonJs.InvokeAsync<TValue>(identifier, args);
    }
    
    internal static async ValueTask<TValue> InvokeAsync<TValue>(this Lazy<ValueTask<IJSObjectReference>> lazyRef, string identifier, CancellationToken cancellationToken, params object?[]? args)
    {
        var ionCommonJs = await lazyRef.Value;
        return await ionCommonJs.InvokeAsync<TValue>(identifier, cancellationToken, args);
    }
    
    internal static async ValueTask<TValue> InvokeAsync<TValue>(this Lazy<ValueTask<IJSObjectReference>> lazyRef, string identifier, TimeSpan timeout, params object?[]? args)
    {
        var ionCommonJs = await lazyRef.Value;
        return await ionCommonJs.InvokeAsync<TValue>(identifier, timeout, args);
    }
    
    
}