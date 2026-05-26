namespace IonBlazor.Services;

/// <summary>
/// <b>Deprecated.</b> Replaced by the injectable <see cref="IonAlertService"/>. Register via
/// <c>services.AddIonBlazor()</c> and inject with <c>@inject IonAlertService</c>. See the README
/// "Controllers → Services" migration section for details.
/// </summary>
[Obsolete(
    "IonAlertController has been replaced by the injectable IonAlertService. " +
    "Register via services.AddIonBlazor() and inject with @inject IonAlertService. " +
    "Remove <IonAlertController /> from your layout/App.razor. " +
    "See README 'Controllers → Services' for the migration path.",
    error: true)]
public sealed class IonAlertController
{
    [Obsolete("Use IonAlertService.PresentAsync via DI. See README migration.", error: true)]
    public static ValueTask PresentAsync(Action<AlertControllerOptions> configure) => ValueTask.CompletedTask;
}