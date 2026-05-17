namespace IonBlazor.UnitTests.Components;

public class IonRowTests : IonTestContext
{
    [Fact]
    public async Task IonRowRendersCorrectly()
    {
        // Act
        var cut = Render<IonRow>();

        // Assert
        await Verify(cut.Markup);
    }

    [Fact]
    public async Task WithChildContent_RendersCorrectly()
    {
        // Act
        var cut = Render<IonRow>(parameters => parameters
            .AddChildContent("<ion-col></ion-col>"));

        // Assert
        await Verify(cut.Markup);
    }

    [Fact]
    public async Task WithAttributes_RendersCorrectly()
    {
        // Act
        var cut = Render<IonRow>(parameters => parameters
            .Add(p => p.Attributes, new Dictionary<string, object>
            {
                { "id", "row" }
            }));

        // Assert
        await Verify(cut.Markup);
    }
}
