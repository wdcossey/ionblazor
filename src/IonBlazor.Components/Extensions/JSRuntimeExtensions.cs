// ReSharper disable once CheckNamespace

namespace IonBlazor.Extensions;

// ReSharper disable once InconsistentNaming
internal static class JSRuntimeExtensions
{
    //await JsRuntime.InvokeAsync<IJSObjectReference>("import", "./_content/IonBlazor/Components/IonModal.razor.js");
    internal static Task<IJSObjectReference> ImportAsync(this IJSRuntime jsRuntime, string fileName) =>
        jsRuntime.InvokeAsync<IJSObjectReference>("import", $"./_content/IonBlazor/{fileName}.js").AsTask();

    internal static async ValueTask AttachIonListenersAsync(this IJSRuntime jsRuntime, ElementReference reference, params IonEvent[]? args)
    {
        await using IJSObjectReference jsModule = await jsRuntime.ImportAsync("common");
        await jsModule.InvokeVoidAsync("attachListeners", args, reference).AsTask();
    }

    internal static async ValueTask InvokeVoidAsync(this Lazy<Task<IJSObjectReference>> lazyRef, string identifier, params object?[]? args)
    {
        IJSObjectReference jsModule = await lazyRef.Value;
        await jsModule.InvokeVoidAsync(identifier, args);
    }

    internal static async ValueTask InvokeVoidAsync(this Task<IJSObjectReference>? moduleTask, string identifier, params object?[]? args)
    {
        if (moduleTask is null)
            return;

        IJSObjectReference jsModule = await moduleTask;
        await jsModule.InvokeVoidAsync(identifier, args);
    }

    internal static async ValueTask InvokeVoidAsync(this Lazy<Task<IJSObjectReference>> lazyRef, string identifier, CancellationToken cancellationToken, params object?[]? args)
    {
        IJSObjectReference jsModule = await lazyRef.Value;
        await jsModule.InvokeVoidAsync(identifier, cancellationToken, args);
    }

    internal static async ValueTask InvokeVoidAsync(this Lazy<Task<IJSObjectReference>> lazyRef, string identifier, TimeSpan timeout, params object?[]? args)
    {
        IJSObjectReference jsModule = await lazyRef.Value;
        await jsModule.InvokeVoidAsync(identifier, timeout, args);
    }

    internal static async ValueTask<TValue> InvokeAsync<TValue>(this Lazy<Task<IJSObjectReference>> lazyRef, string identifier, params object?[]? args)
    {
        IJSObjectReference jsModule = await lazyRef.Value;
        return await jsModule.InvokeAsync<TValue>(identifier, args);
    }

    internal static async ValueTask<TValue?> InvokeAsync<TValue>(this Task<IJSObjectReference>? moduleTask, string identifier, params object?[]? args)
    {
        if (moduleTask is null)
            return default;

        IJSObjectReference jsModule = await moduleTask;
        return await jsModule.InvokeAsync<TValue>(identifier, args);
    }

    internal static async ValueTask<TValue> InvokeAsync<TValue>(this Lazy<Task<IJSObjectReference>> lazyRef, string identifier, CancellationToken cancellationToken, params object?[]? args)
    {
        IJSObjectReference jsModule = await lazyRef.Value;
        return await jsModule.InvokeAsync<TValue>(identifier, cancellationToken, args);
    }

    internal static async ValueTask<TValue> InvokeAsync<TValue>(this Lazy<Task<IJSObjectReference>> lazyRef, string identifier, TimeSpan timeout, params object?[]? args)
    {
        IJSObjectReference jsModule = await lazyRef.Value;
        return await jsModule.InvokeAsync<TValue>(identifier, timeout, args);
    }

    internal static async ValueTask DisposeAsync(this Lazy<Task<IJSObjectReference>> lazyRef)
    {
        IJSObjectReference jsModule = await lazyRef.Value;
        await jsModule.DisposeAsync();
    }


}