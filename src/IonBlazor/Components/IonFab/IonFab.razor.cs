namespace IonBlazor.Components;

public sealed partial class IonFab : IonContentComponent
{
    private ElementReference _self;

    public override ElementReference IonElement => _self;

    /// <summary>
    /// If <b>true</b>, both the <see cref="IonFabButton"/> and all <see cref="IonFabList"/> inside
    /// <see cref="IonFab"/> will become active.
    /// That means <see cref="IonFabButton"/> will become a close icon and <see cref="IonFabList"/> will become visible.
    /// </summary>
    [Parameter] public bool Activated { get; set; }

    /// <summary>
    /// If <b>true</b>, the fab will display on the edge of the header if vertical is
    /// <see cref="IonFabVerticalAlignment.Top"/>, and on the edge of the footer if it is
    /// <see cref="IonFabVerticalAlignment.Bottom"/>.
    /// Should be used with a fixed slot.
    /// </summary>
    [Parameter] public bool Edge { get; set; }

    /// <summary>
    /// Where to align the fab horizontally in the viewport.<br/>
    /// See <see cref="IonFabHorizontalAlignment"/> for options
    /// </summary>
    [Parameter] public string? Horizontal { get; set; } = IonFabHorizontalAlignment.Default;

    /// <summary>
    /// Where to align the fab vertically in the viewport.<br/>
    /// See <see cref="IonFabVerticalAlignment"/> for options
    /// </summary>
    [Parameter] public string? Vertical { get; set; } = IonFabVerticalAlignment.Default;

    /// <summary>
    /// Close an active FAB list container.
    /// </summary>
    /// <returns></returns>
    public Task CloseAsync()
    {
        throw new NotImplementedException();
    }
}