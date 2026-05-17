namespace IonBlazor.Components;

public sealed record IonModalDragMoveEventArgs
{
    public IonModal? Sender { get; internal init; }

    /// <summary>
    /// The current Y position of the modal.
    /// <br/><br/>
    /// This can be used to determine how far the modal has been dragged.
    /// </summary>
    public decimal CurrentY { get; internal init; }

    /// <summary>
    /// The change in Y position since the gesture started.<br/><br/>
    /// This can be used to determine the direction of the drag.
    /// </summary>
    public decimal DeltaY { get; internal init; }

    /// <summary>
    /// The velocity of the drag in the Y direction.
    /// <br/><br/>
    /// This can be used to determine how fast the modal is being dragged.
    /// </summary>
    public decimal VelocityY { get; internal init; }

    /// <summary>
    /// A number between 0 and 1.
    ///
    /// In a sheet modal, progress represents the relative position between
    /// the lowest and highest defined breakpoints.
    /// <br/><br/>
    /// In a card modal, it measures the relative position between the
    /// bottom of the screen and the top of the modal when it is fully
    /// open.
    /// <br/><br/>
    /// This can be used to style content based on how far the modal has
    /// been dragged.
    /// </summary>
    public decimal Progress { get; internal init; }

    /// <summary>
    /// If the modal is a sheet modal, this will be the breakpoint that
    /// the modal will snap to if the user lets go of the modal at the
    /// current moment.
    /// <br/><br/>
    /// If it's a card modal, this property will not be included in the
    /// event payload.
    /// <br/><br/>
    /// This can be used to style content based on where the modal will
    /// snap to upon release.
    /// </summary>
    public decimal? SnapBreakpoint { get; internal init; }
}
