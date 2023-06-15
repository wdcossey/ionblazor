namespace IonicSharp.Components;

public partial class IonRippleEffect : IonComponent
{
    private ElementReference _self;
    
    [Parameter] public RenderFragment? ChildContent { get; set; }
    
    [Parameter] public IonRippleEffectType Type { get; set; } = IonRippleEffectType.Bounded;

    public async Task AddRippleAsync(int x, int y)
    {
        await JsRuntime.InvokeVoidAsync("addRippleToRippleEffect", _self, x, y);
    }
}

public enum IonRippleEffectType
{
    Bounded,
    Unbounded
}