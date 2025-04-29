namespace IonBlazor.Abstractions;

public interface IIonContentComponent : IIonComponent
{
    RenderFragment? ChildContent { get; init; }
}