namespace IonBlazor.Abstractions;

public abstract class IonJsContentComponent : IonJsComponent, IIonContentComponent
{
    /// <inheritdoc/>
    [Parameter]
    public virtual RenderFragment? ChildContent { get; init; }
}