using System.Diagnostics;

namespace IonBlazor.Components;

public sealed class IonLoadingReference : IIonLoading
{
    private readonly DotNetObjectReference<IonicEventCallback<JsonObject?>>? _didDismissHandler;
    private readonly DotNetObjectReference<IonicEventCallback<JsonObject?>>? _didPresentHandler;

    private readonly Lazy<Task<IJSObjectReference>> _lazyJsComponent;
    private readonly IonLoadingControllerOptions _options;

    internal IonLoadingReference(IJSRuntime jsRuntime, IonLoadingControllerOptions options)
    {
        _lazyJsComponent = new Lazy<Task<IJSObjectReference>>(() => CreateComponentAsync(jsRuntime));

        _options = options;
        if (string.IsNullOrWhiteSpace(_options.Id))
        {
            _options.Id = $"ibz-loading-{Stopwatch.GetTimestamp():x}";
        }

        _didDismissHandler = IonicEventCallback<JsonObject?>.Create(args =>
        {
            IonLoadingDismissEventArgs eventArgs = new()
            {
                Sender = this,
                Role = args?["detail"]?["role"]?.GetValue<string>(),
                Data = args?["detail"]?["data"]?.Deserialize<JsonElement>(),
                HtmlAttributes = args?["htmlAttributes"]?.Deserialize<Dictionary<string, string>>()
            };

            options.OnDidDismiss?.Invoke(eventArgs);

            return Task.CompletedTask;
        });

        _didPresentHandler = IonicEventCallback<JsonObject?>.Create(args =>
        {
            IonLoadingPresentEventArgs eventArgs = new()
            {
                Sender = this,
                HtmlAttributes = args?["htmlAttributes"]?.Deserialize<Dictionary<string, string>>()
            };
            options.OnDidPresent?.Invoke(eventArgs);
            return Task.CompletedTask;
        });
    }

    /// <summary>
    /// Dismiss the loading overlay after it has been presented.
    /// </summary>
    /// <returns></returns>
    public async ValueTask<bool> DismissAsync<TData>(TData? data = null, string? role = null) where TData : class
    {
        var result = await _lazyJsComponent.InvokeAsync<bool>("dismiss", _options.Id, data, role);
        return result;
    }

    /// <summary>
    /// Dismiss the loading overlay after it has been presented.
    /// </summary>
    /// <returns></returns>
    public ValueTask<bool> DismissAsync(string? role = null) => DismissAsync<object>(null, role);

    public async ValueTask<string?> CreateAsync()
    {
        var result = await _lazyJsComponent.InvokeAsync<string?>("create", _options, _didDismissHandler, _didPresentHandler);
        return result;
    }

    /// <summary>
    /// Present the loading overlay after it has been created.
    /// </summary>
    /// <returns></returns>
    public async ValueTask PresentAsync()
    {
        await _lazyJsComponent.InvokeVoidAsync("present", _options.Id);
    }

    /// <summary>
    /// Present the loading overlay after it has been created.
    /// </summary>
    /// <returns></returns>
    public async ValueTask PresentWithMessageAsync(string? message)
    {
        await _lazyJsComponent.InvokeVoidAsync("presentWithMessage", _options.Id, message);
    }

    /// <summary>
    /// Updates the message for the loading overlay after it has been presented.
    /// </summary>
    /// <returns></returns>
    public async ValueTask UpdateMessageAsync(string? message)
    {
        await _lazyJsComponent.InvokeVoidAsync("updateMessage", _options.Id, message);
    }

    /// <inheritdoc/>
    public async ValueTask DisposeAsync()
    {
        await _lazyJsComponent.InvokeVoidAsync("remove", _options.Id);
        await _lazyJsComponent.DisposeAsync();
        _didDismissHandler?.Dispose();
        _didPresentHandler?.Dispose();
    }

    private static string RenderMessage(string? message)
    {
        return ((MarkupString)(message ?? string.Empty)).ToString();
    }

    internal static async Task<IJSObjectReference> CreateComponentAsync(IJSRuntime jsRuntime)
    {
        IJSObjectReference result = await jsRuntime.ImportAsync(nameof(IonLoadingController));

        if (result is null)
        {
            throw new InvalidOperationException($"{nameof(IonLoadingController)} is not initialized");
        }

        return result;
    }
}