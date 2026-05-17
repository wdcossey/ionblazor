namespace IonBlazor.Components.Abstractions;

public interface IIonList : IIonContentComponent, IIonModeComponent
{
    public IIonComponent? Parent { get; }

    /// <summary>
    /// If <b>true</b>, the list will have margin around it and rounded corners.
    /// </summary>
    bool? Inset { get; }

    /// <summary>
    /// How the bottom border should be displayed on all items.
    /// </summary>
    string? Lines { get; }

    /// <summary>
    /// If <see cref="IonItemSliding"/> are used inside the list, this method closes any open sliding item.
    /// Returns true if an actual <see cref="IonItemSliding"/> is closed.
    /// </summary>
    /// <returns></returns>
    ValueTask<bool> CloseSlidingItemsAsync();
}