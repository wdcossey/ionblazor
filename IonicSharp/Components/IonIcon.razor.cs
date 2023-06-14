namespace IonicSharp.Components;

public partial class IonIcon : IonSlotControl
{
    [Parameter] public string Name { get; set; } = null!;
    [Parameter] public string? Color { get; set; }
    [Parameter] public IconSize? Size { get; set; }
}

public enum IconSize 
{
    Default,
    Large,
    Small
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