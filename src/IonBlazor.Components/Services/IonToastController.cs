namespace IonBlazor.Services;

/// <summary>
/// <b>Deprecated.</b> Replaced by the injectable <see cref="IonToastService"/>. Register via
/// <c>services.AddIonBlazor()</c> and inject with <c>@inject IonToastService</c>. See the README
/// "Controllers → Services" migration section for details.
/// </summary>
[Obsolete(
    "IonToastController has been replaced by the injectable IonToastService. " +
    "Register via services.AddIonBlazor() and inject with @inject IonToastService. " +
    "Remove <IonToastController /> from your layout/App.razor. " +
    "See README 'Controllers → Services' for the migration path.",
    error: true)]
public sealed class IonToastController
{
    [Obsolete("Use IonToastService.PresentAsync via DI. See README migration.", error: true)]
    public static ValueTask PresentAsync(Action<ToastControllerOptions> configure) => ValueTask.CompletedTask;

    [Obsolete("Use IonToastService.PresentAsync via DI. See README migration.", error: true)]
    public static ValueTask PresentAsync(
        string? header = null,
        string? message = null,
        string? position = null,
        TimeSpan? duration = null,
        string? icon = null,
        string? positionAnchor = null,
        bool? translucent = null,
        bool? animated = null,
        ToastControllerOptions.ButtonBuilder? buttons = null,
        IDictionary<string, string>? htmlAttributes = null,
        Action<IonToastDismissEventArgs>? onDidDismiss = null) => ValueTask.CompletedTask;
}