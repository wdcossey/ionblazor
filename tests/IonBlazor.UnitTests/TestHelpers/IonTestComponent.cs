using Microsoft.AspNetCore.Components;

namespace IonBlazor.UnitTests.TestHelpers;

internal class IonTestComponent : ComponentBase, IIonComponent
{
    public ElementReference IonElement { get; } = new("ion-test-component");

    private IonTestComponent() { }

    internal static IonTestComponent Create()
    {
        return new IonTestComponent();
    }
}