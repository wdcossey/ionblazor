// ReSharper disable once CheckNamespace
namespace IonicSharp;

internal static class IonComponentExtensions
{
    internal static async ValueTask AttachIonListenersAsync(
        this IonComponent component, 
        ElementReference reference,
        params IonEvent[]? args) =>
        await component.JsRuntime.AttachIonListenersAsync(reference, args);
}