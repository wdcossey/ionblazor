namespace IonBlazor.Components;

public sealed record IonBreadcrumbsCollapsedClickEventArgs
{
    public IonBreadcrumbs? Sender { get; internal init; }
    public IReadOnlyList<CollapsedBreadcrumb>? CollapsedBreadcrumbs { get; internal init; }
}

public sealed record CollapsedBreadcrumb
{
    public bool? Active { get; init; }
    public bool? Collapsed { get; init; }
    public bool? Disabled { get; init; }
    public string? Download { get; init; }
    public string? Href { get; init; }
    public bool? Last { get; init; }
    public string? TextContent { get; init; }
}
