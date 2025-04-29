namespace IonBlazor.Components;

public sealed partial class IonRippleEffect : IonContentComponent
{
    protected override string JsImportName => nameof(IonRippleEffect);

    /// <summary>
    /// Sets the type of ripple-effect:<br/>
    /// <ul>
    /// <li><b>bounded</b>: the ripple effect expands from the user's click position</li>
    /// <li><b>unbounded</b>: the ripple effect expands from the center of the button and overflows the container</li>
    /// </ul><br/>
    /// NOTE: Surfaces for bounded ripples should have the overflow property set to hidden, while surfaces for unbounded ripples should have it set to visible.
    /// </summary>
    [Parameter]
    public string? Type { get; set; } = IonRippleEffectType.Default;

    /// <summary>
    /// Adds the ripple effect to the parent element.
    /// </summary>
    public async Task AddRippleAsync(int x, int y) =>
        await JsComponent.InvokeVoidAsync("addRipple", IonElement, x, y);
}