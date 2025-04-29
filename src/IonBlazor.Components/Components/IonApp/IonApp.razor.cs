namespace IonBlazor.Components;

public sealed partial class IonApp: IonContentComponent, IIonModeComponent
{
    protected override string JsImportName => nameof(IonApp);

    /// <inheritdoc />
    [Parameter] public string? Mode { get; set; }

    /// <summary>
    /// Used to set focus on an element that uses <b>ion-focusable</b>. Do not use this if focusing the element as a result of a keyboard event as the focus utility should handle this for us. This method should be used when we want to programmatically focus an element as a result of another user action. (Ex: We focus the first element inside of a popover when the user presents it, but the popover is not always presented as a result of keyboard action.)
    /// </summary>
    /// <returns></returns>
    public async ValueTask SetFocusAsync(params ElementReference[] elements)
        => await JsComponent.InvokeVoidAsync("setFocus", IonElement, elements);
}