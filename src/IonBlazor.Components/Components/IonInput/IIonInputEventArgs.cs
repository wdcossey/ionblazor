namespace IonBlazor.Components;

public interface IIonInputEventArgs
{
    string? Value { get; }

    IonInputEvent Event { get; }
}