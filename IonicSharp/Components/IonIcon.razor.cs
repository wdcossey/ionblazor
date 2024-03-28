namespace IonicSharp.Components;

public partial class IonIcon : IonComponent, IIonColorComponent
{
    private ElementReference _self;
    
    public override ElementReference IonElement => _self;

    [Parameter] 
    public string Name { get; set; } = null!;
    
    /// <inheritdoc/>
    [Parameter] 
    public string? Color { get; set; }
    
    [Parameter] 
    public string? Size { get; set; }
}

public static class IconSize
{
    public const string Default = "default";
    public const string Large = "large";
    public const string Small = "small";
}

public static class IconColor 
{
    public const string Danger = "danger";
    public const string Dark = "dark";
    public const string Light = "light";
    public const string Medium = "medium";
    public const string Primary = "primary";
    public const string Secondary = "secondary";
    public const string Success = "success";
    public const string Tertiary = "tertiary";
    public const string Warning = "warning";
}