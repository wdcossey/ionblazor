namespace IonBlazor.Services;

public sealed record IonLoadingControllerOptions
{
    [JsonPropertyName("spinner")]
    public string? Spinner { get; set; }

    [JsonPropertyName("message")]
    public string? Message { get; set; }

    [JsonPropertyName("cssClass")]
    public string? CssClass { get; set; }

    [JsonPropertyName("showBackdrop")]
    public bool? ShowBackdrop { get; set; }

    [JsonPropertyName("duration")]
    public uint? Duration { get; set; }

    [JsonPropertyName("translucent")]
    public bool? Translucent { get; set; }

    [JsonPropertyName("animated")]
    public bool? Animated { get; set; }

    [JsonPropertyName("backdropDismiss")]
    public bool? BackdropDismiss { get; set; }

    [JsonPropertyName("mode")]
    public string? Mode { get; set; }

    [JsonPropertyName("keyboardClose")]
    public bool? KeyboardClose { get; set; }

    [JsonPropertyName("id")]
    public string? Id { get; set; }

    [JsonPropertyName("htmlAttributes")]
    public Dictionary<string, string>? HtmlAttributes { get; set; }

    [JsonIgnore]
    public Func<object, IonLoadingDismissEventArgs, Task>? OnDidDismiss { get; set; }

    [JsonIgnore]
    public Func<object, IonLoadingPresentEventArgs, Task>? OnDidPresent { get; set; }

    /*public event EventHandler<IonLoadingDismissEventArgs>? OnDidDismiss;

    public event EventHandler<IonLoadingPresentEventArgs>? OnDidPresent;*/

    internal async Task InvokeOnDidDismiss(object? sender, IonLoadingDismissEventArgs args) => await (OnDidDismiss?.Invoke(sender ?? this, args) ?? Task.CompletedTask);

    internal async Task InvokeOnDidPresent(object? sender, IonLoadingPresentEventArgs args) => await (OnDidPresent?.Invoke(sender ?? this, args) ?? Task.CompletedTask);
}