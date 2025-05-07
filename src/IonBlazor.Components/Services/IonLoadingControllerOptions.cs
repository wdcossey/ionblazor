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
    public Action<IonLoadingDismissEventArgs>? OnDidDismiss { get; set; }

    [JsonIgnore]
    public Action<IonLoadingPresentEventArgs>? OnDidPresent { get; set; }
}