namespace IonBlazor.Components;

public sealed partial class IonIcon : IonComponent, IIonColorComponent
{
    protected override string JsImportName => nameof(IonIcon);

    [Parameter]
    public string? Name { get; set; }

    /// <inheritdoc/>
    [Parameter]
    public string? Color { get; init; } = IonColor.Default;

    [Parameter]
    public string? Size { get; set; } = IonIconSize.Null;

    public ValueTask SetName(string value)
    {
        Name = value;
        return JsComponent.InvokeVoidAsync("setName", IonElement, value);
    }

    public ValueTask SetColor(string value)
    {
        return JsComponent.InvokeVoidAsync("setColor", IonElement, value);
    }

    public ValueTask SetSize(string value)
    {
        Size = value;
        return JsComponent.InvokeVoidAsync("setSize", IonElement, value);
    }
}