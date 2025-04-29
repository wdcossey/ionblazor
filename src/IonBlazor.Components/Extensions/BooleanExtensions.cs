namespace IonBlazor.Extensions;

internal static class BooleanExtensions
{
    private const string True = "true";
    private const string False = "false";
    internal const string On = "on";
    internal const string Off = "off";

    internal static string? AsString(this bool value, string @true = True, string @false = False) => value switch
    {
        true => @true,
        _ => @false
    };

    internal static string? AsString(this bool? value, string @true = True, string @false = False) => value switch
    {
        null => null,
        _ => AsString((bool)value, @true, @false)
    };
}