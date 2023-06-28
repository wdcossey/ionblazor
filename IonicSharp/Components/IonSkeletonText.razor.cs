namespace IonicSharp.Components;

public partial class IonSkeletonText : IonComponent
{
    /// <summary>
    /// 
    /// </summary>
    [Parameter]
    public bool Animated { get; set; } = false;

    public IonSkeletonText SetAnimated(bool value)
    {
        Animated = value;
        StateHasChanged();
        return this;
    }
}