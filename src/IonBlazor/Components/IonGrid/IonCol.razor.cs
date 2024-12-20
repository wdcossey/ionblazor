namespace IonBlazor.Components;

public sealed partial class IonCol : IonContentComponent
{
    private ElementReference _self;

    public override ElementReference IonElement => _self;

    [CascadingParameter(Name = nameof(Parent))] public IIonComponent? Parent { get; init; }

    /// <summary>
    /// The amount to offset the column, in terms of how many columns it should shift to the end of the total available.
    /// </summary>
    [Parameter] public string? Offset { get; set; }

    /// <summary>
    /// The amount to offset the column for lg screens, in terms of how many columns it should shift to the end of the
    /// total available.
    /// </summary>
    [Parameter] public string? OffsetLg { get; set; }

    /// <summary>
    /// The amount to offset the column for md screens, in terms of how many columns it should shift to the end of the
    /// total available.
    /// </summary>
    [Parameter] public string? OffsetMd { get; set; }

    /// <summary>
    /// The amount to offset the column for sm screens, in terms of how many columns it should shift to the end of the
    /// total available.
    /// </summary>
    [Parameter] public string? OffsetSm { get; set; }

    /// <summary>
    /// The amount to offset the column for xl screens, in terms of how many columns it should shift to the end of the
    /// total available.
    /// </summary>
    [Parameter] public string? OffsetXl { get; set; }

    /// <summary>
    /// The amount to offset the column for xs screens, in terms of how many columns it should shift to the end of the
    /// total available.
    /// </summary>
    [Parameter] public string? OffsetXs { get; set; }

    /// <summary>
    /// The amount to pull the column, in terms of how many columns it should shift to the start of the total available.
    /// </summary>
    [Parameter] public string? Pull { get; set; }

    /// <summary>
    /// The amount to pull the column for lg screens, in terms of how many columns it should shift to the start of the
    /// total available.
    /// </summary>
    [Parameter] public string? PullLg { get; set; }

    /// <summary>
    /// The amount to pull the column for md screens, in terms of how many columns it should shift to the start of the
    /// total available.
    /// </summary>
    [Parameter] public string? PullMd { get; set; }

    /// <summary>
    /// The amount to pull the column for sm screens, in terms of how many columns it should shift to the start of the
    /// total available.
    /// </summary>
    [Parameter] public string? PullSm { get; set; }

    /// <summary>
    /// The amount to pull the column for xl screens, in terms of how many columns it should shift to the start of the
    /// total available.
    /// </summary>
    [Parameter] public string? PullXl { get; set; }

    /// <summary>
    /// The amount to pull the column for xs screens, in terms of how many columns it should shift to the start of the
    /// total available.
    /// </summary>
    [Parameter] public string? PullXs { get; set; }

    /// <summary>
    /// The amount to push the column, in terms of how many columns it should shift to the end of the total available.
    /// </summary>
    [Parameter] public string? Push { get; set; }

    /// <summary>
    /// The amount to push the column for lg screens, in terms of how many columns it should shift to the end of the
    /// total available.
    /// </summary>
    [Parameter] public string? PushLg { get; set; }

    /// <summary>
    /// The amount to push the column for md screens, in terms of how many columns it should shift to the end of the
    /// total available.
    /// </summary>
    [Parameter] public string? PushMd { get; set; }

    /// <summary>
    /// The amount to push the column for sm screens, in terms of how many columns it should shift to the end of the
    /// total available.
    /// </summary>
    [Parameter] public string? PushSm { get; set; }

    /// <summary>
    /// The amount to push the column for xl screens, in terms of how many columns it should shift to the end of the
    /// total available.
    /// </summary>
    [Parameter] public string? PushXl { get; set; }

    /// <summary>
    /// The amount to push the column for xs screens, in terms of how many columns it should shift to the end of the
    /// total available.
    /// </summary>
    [Parameter] public string? PushXs { get; set; }

    /// <summary>
    /// The size of the column, in terms of how many columns it should take up out of the total available.
    /// If "<b>auto</b>" is passed, the column will be the size of its content.
    /// </summary>
    [Parameter] public string? Size { get; set; }

    /// <summary>
    /// The size of the column for lg screens, in terms of how many columns it should take up out of the total
    /// available. If "<b>auto</b>" is passed, the column will be the size of its content.
    /// </summary>
    [Parameter] public string? SizeLg { get; set; }

    /// <summary>
    /// The size of the column for md screens, in terms of how many columns it should take up out of the total
    /// available. If "<b>auto</b>" is passed, the column will be the size of its content.
    /// </summary>
    [Parameter] public string? SizeMd { get; set; }

    /// <summary>
    /// The size of the column for sm screens, in terms of how many columns it should take up out of the total
    /// available. If "<b>auto</b>" is passed, the column will be the size of its content.
    /// </summary>
    [Parameter] public string? SizeSm { get; set; }

    /// <summary>
    /// The size of the column for xl screens, in terms of how many columns it should take up out of the total
    /// available. If "<b>auto</b>" is passed, the column will be the size of its content.
    /// </summary>
    [Parameter] public string? SizeXl { get; set; }

    /// <summary>
    /// The size of the column for xs screens, in terms of how many columns it should take up out of the total
    /// available. If "<b>auto</b>" is passed, the column will be the size of its content.
    /// </summary>
    [Parameter] public string? SizeXs { get; set; }

}