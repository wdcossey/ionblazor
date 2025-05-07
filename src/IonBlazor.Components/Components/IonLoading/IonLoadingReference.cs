using System.Diagnostics;

namespace IonBlazor.Components;

public sealed class IonLoadingReference : IIonLoading
{
    private readonly DotNetObjectReference<IonicEventCallback<JsonObject?>>? _didDismissHandler;
    private readonly DotNetObjectReference<IonicEventCallback<JsonObject?>>? _didPresentHandler;
    private readonly string _componentId = $"ibz-loading-{Stopwatch.GetTimestamp():x}";

    private readonly IJSObjectReference? _jsComponent;

    private readonly string? _message;

    private readonly uint? _duration;
    private readonly IDictionary<string, string>? _htmlAttributes;

    internal IonLoadingReference(IJSObjectReference? jsComponent, IonLoadingControllerOptions options)
    {
        _jsComponent = jsComponent;
        _message = options.Message;
        _duration = options.Duration;
        _htmlAttributes = options.HtmlAttributes;

        _didDismissHandler = IonicEventCallback<JsonObject?>.Create(args =>
        {
            IonLoadingDismissEventArgs obj = new()
            {
                Sender = this,
                Role = args?["detail"]?["role"]?.GetValue<string>(),
                Data = args?["detail"]?["data"]?.Deserialize<JsonElement>(),
                HtmlAttributes = args?["htmlAttributes"]?.Deserialize<Dictionary<string, string>>()
            };

            options.OnDidDismiss?.Invoke(obj);

            return Task.CompletedTask;
        });

        _didPresentHandler = IonicEventCallback<JsonObject?>.Create(args =>
        {
            IonLoadingPresentEventArgs obj = new()
            {
                Sender = this,
                HtmlAttributes = args?["htmlAttributes"]?.Deserialize<Dictionary<string, string>>()
            };
            options.OnDidPresent?.Invoke(obj);
            return Task.CompletedTask;
        });
    }

    /// <summary>
    /// Dismiss the loading overlay after it has been presented.
    /// </summary>
    /// <returns></returns>
    public async ValueTask<bool> DismissAsync<TData>(TData? data = null, string? role = null) where TData : class
    {
        var result = await (_jsComponent?.InvokeAsync<bool>("dismiss", _componentId, data, role) ?? ValueTask.FromResult(false));
        return result;
    }

    /// <summary>
    /// Dismiss the loading overlay after it has been presented.
    /// </summary>
    /// <returns></returns>
    public ValueTask<bool> DismissAsync(string? role = null) => DismissAsync<object>(null, role);

    public async ValueTask CreateAsync()
    {
        var markupString = RenderMessage(_message);
        var result = await (_jsComponent?.InvokeAsync<string?>("create", _componentId, markupString, _duration, _htmlAttributes, _didDismissHandler, _didPresentHandler) ?? ValueTask.FromResult<string?>(null));
    }

    /// <summary>
    /// Present the loading overlay after it has been created.
    /// </summary>
    /// <returns></returns>
    public async ValueTask PresentAsync()
    {
        await (_jsComponent?.InvokeVoidAsync("present", _componentId) ?? ValueTask.CompletedTask);
    }

    /// <summary>
    /// Present the loading overlay after it has been created.
    /// </summary>
    /// <returns></returns>
    public async ValueTask PresentWithMessageAsync(string? message)
    {
        await (_jsComponent?.InvokeVoidAsync("presentWithMessage", _componentId, message) ?? ValueTask.CompletedTask);
    }

    /// <summary>
    /// Updates the message for the loading overlay after it has been presented.
    /// </summary>
    /// <returns></returns>
    public async ValueTask UpdateMessageAsync(string? message)
    {
        var markupString = RenderMessage(message);
        await (_jsComponent?.InvokeVoidAsync("setMessage", _componentId, markupString) ?? ValueTask.CompletedTask);
    }

    /// <inheritdoc/>
    public async ValueTask DisposeAsync()
    {
        await (_jsComponent?.InvokeVoidAsync("remove", _componentId) ?? ValueTask.CompletedTask);
        _didDismissHandler?.Dispose();
        _didPresentHandler?.Dispose();
    }

    private static string RenderMessage(string? message)
    {
        return ((MarkupString)(message ?? string.Empty)).ToString();
    }
}