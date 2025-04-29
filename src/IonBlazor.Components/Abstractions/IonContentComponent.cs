namespace IonBlazor.Abstractions;

public abstract class IonContentComponent : IonComponent, IIonContentComponent
{
    /// <inheritdoc/>
    [Parameter]
    public virtual RenderFragment? ChildContent { get; init; }
}