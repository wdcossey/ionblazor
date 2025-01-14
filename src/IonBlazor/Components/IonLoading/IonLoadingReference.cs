namespace IonBlazor.Components;

public sealed class IonLoadingReference : IIonLoading
{
    private readonly DotNetObjectReference<IonicEventCallback<JsonObject?>>? _didDismissHandler;
    private readonly DotNetObjectReference<IonicEventCallback<JsonObject?>>? _didPresentHandler;
    private string _componentId = null!;

    private readonly IJSObjectReference? _jsComponent;

    private readonly string? _message;

    private readonly uint? _duration;
    private readonly IDictionary<string, string>? _htmlAttributes;

    public IonLoadingReference(IJSObjectReference? jsComponent, IonLoadingReferenceConfiguration config)
    {
        _jsComponent = jsComponent;
        _message = config.Message;
        _duration = config.Duration;
        _htmlAttributes = config.HtmlAttributes;

        _didDismissHandler = IonicEventCallback<JsonObject?>.Create(args =>
        {
            IonLoadingDismissEventArgs obj = new()
            {
                Sender = this,
                Role = args?["detail"]?["role"]?.GetValue<string>(),
                Data = args?["detail"]?["data"],
                HtmlAttributes = args?["htmlAttributes"]?.Deserialize<Dictionary<string, string>>()
            };

            config.OnDidDismiss?.Invoke(obj);

            return Task.CompletedTask;
        });

        _didPresentHandler = IonicEventCallback<JsonObject?>.Create(args =>
        {
            IonLoadingPresentEventArgs obj = new()
            {
                Sender = this,
                HtmlAttributes = args?["htmlAttributes"]?.Deserialize<Dictionary<string, string>>()
            };
            config.OnDidPresent?.Invoke(obj);
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

    /// <summary>
    /// Present the loading overlay after it has been created.
    /// </summary>
    /// <returns></returns>
    public async ValueTask PresentAsync()
    {
        MarkupString markupString = (MarkupString)(_message ?? string.Empty);
        var result = await (_jsComponent?.InvokeAsync<string?>("present", markupString.ToString(), _duration, _htmlAttributes, _didDismissHandler, _didPresentHandler) ?? ValueTask.FromResult<string?>(null));
        _componentId = result!;
    }

    /// <summary>
    /// Set the message for the loading overlay after it has been presented.
    /// </summary>
    /// <returns></returns>
    public async ValueTask SetMessageAsync(string? message)
    {
        MarkupString markupString = (MarkupString)(_message ?? string.Empty);
        await (_jsComponent?.InvokeVoidAsync("setMessage", _componentId, markupString.ToString()) ?? ValueTask.CompletedTask);
    }

    public ValueTask DisposeAsync()
    {
        _didDismissHandler?.Dispose();
        _didPresentHandler?.Dispose();
        return ValueTask.CompletedTask;
    }
}