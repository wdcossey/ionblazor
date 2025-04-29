namespace IonBlazor.Components;

public sealed class IonLoadingReferenceConfiguration
{
    public string? Message { get; set; }

    public uint? Duration { get; set; }

    public IDictionary<string, string>? HtmlAttributes { get; set; }

    public Action<IonLoadingDismissEventArgs>? OnDidDismiss { get; set; }

    public Action<IonLoadingPresentEventArgs>? OnDidPresent { get; set; }

    internal IonLoadingReferenceConfiguration() { }
}