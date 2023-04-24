using System.Text.Json.Nodes;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace IonicTest.Components;

public partial class IonButton : IonControl
{
    private ElementReference _self;
    private DotNetObjectReference<ButtonEventHelper<JsonObject?>>? _ionBlurObjectReference = null;
    private DotNetObjectReference<ButtonEventHelper<JsonObject?>>? _ionFocusObjectReference = null;
    
    [Parameter] public RenderFragment? ChildContent { get; set; }
    
    /// <summary>
    /// The type of button.
    /// </summary>
    [Parameter] public string? ButtonType { get; set; } = "button";
    
    /// <summary>
    /// The color to use from your application's color palette.
    /// Default options are:
    /// <see cref="IonicColor.Primary"/>, <see cref="IonicColor.Secondary"/>,
    /// <see cref="IonicColor.Tertiary"/>, <see cref="IonicColor.Success"/>,
    /// <see cref="IonicColor.Warning"/>, <see cref="IonicColor.Danger"/>,
    /// <see cref="IonicColor.Light"/>, <see cref="IonicColor.Medium"/>,
    /// and <see cref="IonicColor.Dark"/>. <br/>
    /// For more information on colors, see theming.
    /// </summary>
    [Parameter] public string? Color { get; set; }
    
    /// <summary>
    /// If true, the user cannot interact with the <see cref="IonButton"/>.
    /// </summary>
    [Parameter] public bool? Disabled { get; set; }
    
    /// <summary>
    /// This attribute instructs browsers to download a URL instead of navigating to it,
    /// so the user will be prompted to save it as a local file. <br/>
    /// If the attribute has a value, it is used as the pre-filled file name in the Save prompt
    /// (the user can still change the file name if they want).
    /// </summary>
    [Parameter] public string? Download { get; set; }
    
    /// <summary>
    /// Set to <see cref="ButtonExpand.Block"/> for a full-width button
    /// or to <see cref="ButtonExpand.Full"/> for a full-width button with square corners and no left or right borders.
    /// </summary>
    [Parameter] public ButtonExpand? Expand { get; set; }
    
    /// <summary>
    /// Set to <see cref="ButtonFill.Clear"/> for a transparent button that resembles a flat button,
    /// to <see cref="ButtonFill.Outline"/> for a transparent button with a border,
    /// or to <see cref="ButtonFill.Solid"/> for a button with a filled background.
    /// The default fill is <see cref="ButtonFill.Solid"/> except inside of a toolbar,
    /// where the default is <see cref="ButtonFill.Clear"/>.
    /// </summary>
    [Parameter] public ButtonFill? Fill { get; set; }
    
    /// <summary>
    /// The HTML form element or form element id. Used to submit a form when the button is not a child of the form.
    /// </summary>
    [Parameter] public string? Form { get; set; }
    
    /// <summary>
    /// Contains a URL or a URL fragment that the hyperlink points to. If this property is set, an anchor tag will be rendered.
    /// </summary>
    [Parameter] public string? Href { get; set; }

    /// <summary>
    /// The mode determines which platform styles to use.
    /// </summary>
    [Parameter]
    public string? Mode { get; set; } = IonMode.Default;
    
    /// <summary>
    /// Specifies the relationship of the target object to the link object. The value is a space-separated list of link types.
    /// </summary>
    [Parameter] public string? Rel { get; set; }
    //[Parameter] public string? routerAnimation { get; set; }
    //[Parameter] public string? routerDirection { get; set; }
    
    /// <summary>
    /// Set to <see cref="ButtonShape.Round"/> for a button with more rounded corners.
    /// </summary>
    [Parameter] public ButtonShape? Shape { get; set; }
    
    /// <summary>
    /// Set to <see cref="ButtonSize.Small"/> for a button with less height and padding,
    /// to <see cref="ButtonSize.Default"/> for a button with the default height and padding,
    /// or to <see cref="ButtonSize.Large"/> for a button with more height and padding. <br/>
    /// By default the size is unset, unless the button is inside of an item,
    /// where the size is <see cref="ButtonSize.Small"/> by default. <br/>
    /// Set the size to <see cref="ButtonSize.Default"/> inside of an item to make it a standard size button.
    /// </summary>
    [Parameter] public ButtonSize? Size { get; set; }
    
    /// <summary>
    /// If true, activates a button with a heavier font weight.
    /// </summary>
    [Parameter] public bool? Strong { get; set; }
    
    /// <summary>
    /// Specifies where to display the linked URL. <br/>
    /// Only applies when an href is provided. <br/>
    /// Special keywords: "_blank", "_self", "_parent", "_top".
    /// </summary>
    [Parameter] public string? Target { get; set; }
    
    /// <summary>
    /// The type of the button.
    /// </summary>
    [Parameter] public string Type { get; set; } = Components.IonButtonType.Button;
    
    [Parameter] public EventCallback OnClick { get; set; }
    
    /// <summary>
    /// Emitted when the button loses focus.
    /// </summary>
    [Parameter] public EventCallback OnBlur { get; set; }
    
    /// <summary>
    /// Emitted when the button has focus.
    /// </summary>
    [Parameter] public EventCallback OnFocus { get; set; }
    
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);

        if (!firstRender)
            return;
        
        _ionBlurObjectReference = DotNetObjectReference.Create(new ButtonEventHelper<JsonObject?>(async args =>
        {
            await OnBlur.InvokeAsync();
        }));
        
        _ionFocusObjectReference = DotNetObjectReference.Create(new ButtonEventHelper<JsonObject?>(async args =>
        {
            await OnFocus.InvokeAsync();
        }));

        await JsRuntime.InvokeVoidAsync("attachIonEventListener", "ionBlur", _self, _ionBlurObjectReference);
        await JsRuntime.InvokeVoidAsync("attachIonEventListener", "ionFocus", _self, _ionFocusObjectReference);

    }
     
     public class ButtonEventHelper<TArgs>
     {
         private readonly Func<TArgs, Task> _callback;

         public ButtonEventHelper(Func<TArgs, Task> callback) => _callback = callback;

         [JSInvokable]
         public Task OnCallbackEvent(TArgs args) => _callback(args);
     }
}

public static class IonMode 
{
    // ReSharper disable InconsistentNaming

    /// <summary>
    /// Automatic
    /// </summary>
    public const string? Default = null;
    
    /// <summary>
    /// Apple iOS
    /// </summary>
    public const string iOS = "ios";

    /// <summary>
    /// Google Material Design
    /// </summary>
    public const string MaterialDesign = "md";

    // ReSharper enable InconsistentNaming
}

public enum ButtonExpand {
    Block,
    Full
}

public enum ButtonFill 
{

    Clear, 
    Default,
    Outline,
    Solid
}

public enum ButtonShape 
{
    Round
}

public enum ButtonSize 
{
    Default,
    Large,
    Small
}

public static class IonButtonType 
{
    public const string Button = "button";
    public const string Reset = "reset";
    public const string Submit = "submit";
}