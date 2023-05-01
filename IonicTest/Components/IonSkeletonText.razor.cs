namespace IonicTest.Components;

public partial class IonSkeletonText
{
    [Parameter] public bool Animated { get; set; } = false;

    public IonSkeletonText SetAnimated(bool value)
    {
        Animated = value;
        StateHasChanged();
        return this;
    }
}