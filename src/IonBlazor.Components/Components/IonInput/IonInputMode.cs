namespace IonBlazor.Components;

public static class IonInputMode
{
    public const string? Undefined = null;
    public const string Decimal = "decimal";
    public const string Email = "email";
    public const string None = "none";
    public const string Numeric = "numeric";
    public const string Search = "search";
    public const string Tel = "tel";
    public const string Text = "text";
    public const string Url = "url";
}

[Obsolete("Renamed to IonInputMode.", true)]
public static class IonInputInputMode;