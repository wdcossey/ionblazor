namespace IonicSharp.Components;

public partial class IonRippleEffect : IonComponent, IIonContentComponent
{
    private ElementReference _self;

    /// <inheritdoc/>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [Parameter]
    public string? Type { get; set; } = IonRippleEffectType.Default;

    public async Task AddRippleAsync(int x, int y)
    {
        await JsRuntime.InvokeVoidAsync("addRippleToRippleEffect", _self, x, y);
    }
}

public static class IonRippleEffectType
{
    public const string? Default = null;
    public const string Bounded = "bounded";
    public const string Unbounded = "unbounded";
}