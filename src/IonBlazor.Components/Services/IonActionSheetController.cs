namespace IonBlazor.Services;

/// <summary>
/// <b>Deprecated.</b> Replaced by the injectable <see cref="IonActionSheetService"/>. Register via
/// <c>services.AddIonBlazor()</c> and inject with <c>@inject IonActionSheetService</c>. See the
/// README "Controllers → Services" migration section for details.
/// </summary>
[Obsolete(
    "IonActionSheetController has been replaced by the injectable IonActionSheetService. " +
    "Register via services.AddIonBlazor() and inject with @inject IonActionSheetService. " +
    "Remove <IonActionSheetController /> from your layout/App.razor. " +
    "See README 'Controllers → Services' for the migration path.",
    error: true)]
public sealed class IonActionSheetController
{
    [Obsolete("Use IonActionSheetService.PresentAsync via DI. See README migration.", error: true)]
    public static ValueTask PresentAsync(Action<ActionSheetControllerOptions> configure) => ValueTask.CompletedTask;
}