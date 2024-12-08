namespace IonBlazor.Components;

public partial class IonRippleEffect : IonComponent, IIonContentComponent
{
    private ElementReference _self;
    private readonly Func<int,int,ValueTask> _addRippleJsWrapper;

    public override ElementReference IonElement => _self;

    /// <inheritdoc/>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    /// <summary>
    ///
    /// </summary>
    [Parameter]
    public string? Type { get; set; } = IonRippleEffectType.Default;

    public IonRippleEffect()
    {
        _addRippleJsWrapper = async (x, y) => await JsComponent.InvokeVoidAsync("addRipple", _self, x, y);
    }

    /// <summary>
    /// Adds the ripple effect to the parent element.
    /// </summary>
    public async Task AddRippleAsync(int x, int y) => await _addRippleJsWrapper(x, y);
}

public static class IonRippleEffectType
{
    public const string? Default = null;
    public const string Bounded = "bounded";
    public const string Unbounded = "unbounded";
}