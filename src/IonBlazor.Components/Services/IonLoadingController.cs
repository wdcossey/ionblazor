namespace IonBlazor.Services;

/// <summary>
/// <b>Deprecated.</b> Replaced by the injectable <see cref="IonLoadingService"/>. Register via
/// <c>services.AddIonBlazor()</c> and inject with <c>@inject IonLoadingService</c>. See the README
/// "Controllers → Services" migration section for details.
/// </summary>
[Obsolete(
    "IonLoadingController has been replaced by the injectable IonLoadingService. " +
    "Register via services.AddIonBlazor() and inject with @inject IonLoadingService. " +
    "Remove <IonLoadingController /> from your layout/App.razor. " +
    "See README 'Controllers → Services' for the migration path.",
    error: true)]
public sealed class IonLoadingController
{
    [Obsolete("Use IonLoadingService.PresentAsync via DI. See README migration.", error: true)]
    public static ValueTask<string?> PresentAsync(Action<IonLoadingControllerOptions> configure) => ValueTask.FromResult<string?>(null);

    [Obsolete("Use IonLoadingService.PresentAsync via DI. See README migration.", error: true)]
    public static ValueTask<string?> PresentAsync(
        string? message = null,
        int? duration = 3000,
        IDictionary<string, string>? htmlAttributes = null,
        Action<IonLoadingDismissEventArgs>? onDidDismiss = null,
        Action? onDidPresent = null) => ValueTask.FromResult<string?>(null);

    [Obsolete("Use IonLoadingService.CreateAsync via DI. The synchronous Create wrapper has been removed; call CreateAsync directly. See README migration.", error: true)]
    public static IonLoadingReference Create(Action<IonLoadingControllerOptions> configure) => throw new NotSupportedException();

    [Obsolete("Use IonLoadingService.CreateAsync via DI. See README migration.", error: true)]
    public static Task<IonLoadingReference> CreateAsync(Action<IonLoadingControllerOptions> configure) => throw new NotSupportedException();
}