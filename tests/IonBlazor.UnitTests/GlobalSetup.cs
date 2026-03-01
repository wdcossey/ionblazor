using System.Runtime.CompilerServices;

namespace IonBlazor.UnitTests;

public static class GlobalSetup
{
    [ModuleInitializer]
    public static void Initialize()
    {
        VerifierSettings.ScrubLinesWithReplace(
            replaceLine: line =>
            {
                if (!line.Contains(" blazor:elementReference="))
                    return line;

                var index = line.IndexOf(" blazor:elementReference=", StringComparison.Ordinal);
                return line.Remove(index, 25 + 2 + 36);
            });
    }
}