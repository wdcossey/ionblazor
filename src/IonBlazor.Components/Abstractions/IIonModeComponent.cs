namespace IonBlazor.Abstractions;

public interface IIonModeComponent
{
    /// <summary>
    /// The mode determines which platform styles to use.
    /// <see cref="IonMode"/>
    /// </summary>
    string? Mode { get; set; }
}