// ReSharper disable once CheckNamespace

using IonBlazor.Components;

namespace IonBlazor.Extensions;

// ReSharper disable once InconsistentNaming
internal static class JSRuntimeExtensions
{
    //await JsRuntime.InvokeAsync<IJSObjectReference>("import", "./_content/IonBlazor/Components/IonModal.razor.js");
    internal static Task<IJSObjectReference> ImportAsync(this IJSRuntime jsRuntime, string fileName) =>
        jsRuntime.InvokeAsync<IJSObjectReference>("import", $"./_content/IonBlazor/{fileName}.js").AsTask();

    internal static async ValueTask AttachIonListenersAsync(this IJSRuntime jsRuntime, ElementReference reference, params IonEvent[]? args)
    {
        await using var ionCommonJs = await jsRuntime.ImportAsync("common");
        await ionCommonJs.InvokeVoidAsync("attachListeners", args, reference).AsTask();
    }

    internal static async ValueTask InvokeVoidAsync(this Lazy<Task<IJSObjectReference>> lazyRef, string identifier, params object?[]? args)
    {
        var ionCommonJs = await lazyRef.Value;
        await ionCommonJs.InvokeVoidAsync(identifier, args);
    }

    internal static async ValueTask InvokeVoidAsync(this Lazy<Task<IJSObjectReference>> lazyRef, string identifier, CancellationToken cancellationToken, params object?[]? args)
    {
        var ionCommonJs = await lazyRef.Value;
        await ionCommonJs.InvokeVoidAsync(identifier, cancellationToken, args);
    }

    internal static async ValueTask InvokeVoidAsync(this Lazy<Task<IJSObjectReference>> lazyRef, string identifier, TimeSpan timeout, params object?[]? args)
    {
        var ionCommonJs = await lazyRef.Value;
        await ionCommonJs.InvokeVoidAsync(identifier, timeout, args);
    }

    internal static async ValueTask<TValue> InvokeAsync<TValue>(this Lazy<Task<IJSObjectReference>> lazyRef, string identifier, params object?[]? args)
    {
        var ionCommonJs = await lazyRef.Value;
        return await ionCommonJs.InvokeAsync<TValue>(identifier, args);
    }

    internal static async ValueTask<TValue> InvokeAsync<TValue>(this Lazy<Task<IJSObjectReference>> lazyRef, string identifier, CancellationToken cancellationToken, params object?[]? args)
    {
        var ionCommonJs = await lazyRef.Value;
        return await ionCommonJs.InvokeAsync<TValue>(identifier, cancellationToken, args);
    }

    internal static async ValueTask<TValue> InvokeAsync<TValue>(this Lazy<Task<IJSObjectReference>> lazyRef, string identifier, TimeSpan timeout, params object?[]? args)
    {
        var ionCommonJs = await lazyRef.Value;
        return await ionCommonJs.InvokeAsync<TValue>(identifier, timeout, args);
    }


}
