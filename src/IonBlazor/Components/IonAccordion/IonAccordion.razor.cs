namespace IonBlazor.Components;

public sealed partial class IonAccordion : IonContentComponent, IIonModeComponent
{
    [CascadingParameter(Name = nameof(Parent))] public IIonComponent? Parent { get; init; }

    /// <summary>
    /// If <b>true</b>, the accordion cannot be interacted with.
    /// </summary>
    [Parameter]
    public bool? Disabled { get; init; }

    /// <inheritdoc/>
    [Parameter]
    public string? Mode { get; set; } = IonMode.Default;

    /// <summary>
    /// If <b>true</b>, the accordion cannot be interacted with, but does not alter the opacity.
    /// </summary>
    [Parameter]
    public bool? Readonly { get; init; }

    /// <summary>
    /// The toggle icon to use. This icon will be rotated when the accordion is expanded or collapsed.
    /// </summary>
    [Parameter]
    public string? ToggleIcon { get; init; }

    /// <summary>
    /// The slot inside of <see cref="IonItem"/> to place the toggle icon.<br/>
    /// Defaults to <see cref="IonAccordionToggleIconSlot.End"/>.
    /// </summary>
    [Parameter]
    public string? ToggleIconSlot { get; init; } = IonAccordionToggleIconSlot.Default;

    /// <summary>
    /// The value of the accordion. Defaults to an autogenerated value.<br/>
    /// Default: <i>ion-accordion-${accordionIds++}</i>
    /// </summary>
    [Parameter]
    public string? Value { get; init; }
}