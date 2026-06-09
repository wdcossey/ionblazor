using System.Runtime.CompilerServices;

namespace IonBlazor.UnitTests;

public static class GlobalSetup
{
    private const string ElementReference = "blazor:elementReference";

    [ModuleInitializer]
    public static void Initialize()
    {
        VerifierSettings.ScrubLinesWithReplace(
            replaceLine: line =>
            {
                if (line.Contains(ElementReference))
                {
                    line = System.Text.RegularExpressions.Regex.Replace(
                        line,
                        $"""
                         \s{ElementReference}=".*?"
                         """,
                        string.Empty);
                }

                return line;
            });
    }
}