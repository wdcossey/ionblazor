using System.Runtime.CompilerServices;

namespace IonBlazor.UnitTests;

public static class GlobalSetup
{
    private const string ElementReference = "blazor:elementReference";
    private const string IonBlazorModalId = "ibz-id";

    [ModuleInitializer]
    public static void Initialize()
    {
        VerifierSettings.ScrubLinesWithReplace(
            replaceLine: line =>
            {
                if (line.Contains(ElementReference))
                {
                    //var index = line.IndexOf(ElementReference, StringComparison.Ordinal);
                    //line = line.Remove(index, ElementReference.Length + 2 + 36);
                    if (line.Contains(ElementReference))
                    {
                        line = System.Text.RegularExpressions.Regex.Replace(
                            line,
                            $"""
                             \s{ElementReference}=".*?"
                             """,
                            string.Empty);
                    }

                }

                if (line.Contains(IonBlazorModalId))
                {
                    line = System.Text.RegularExpressions.Regex.Replace(
                        line,
                        $"""
                         {IonBlazorModalId}="ibz-\w+-[0-9a-f]+"
                         """,
                        $"{IonBlazorModalId}");
                }

                return line;
            });
    }
}