namespace IonBlazor.UnitTests.Components;

public class IonGridTests : IonTestContext
{
    [Fact]
    public async Task IonGridRendersCorrectly()
    {
        // Act
        var cut = Render<IonGrid>();

        // Assert
        await Verify(cut.Markup);
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public async Task WithFixed_RendersCorrectly(bool value)
    {
        VerifySettings settings = new();
        settings.UseTextForParameters($"value={value}");

        // Act
        var cut = Render<IonGrid>(parameters => parameters
            .Add(p => p.Fixed, value));

        // Assert
        await Verify(cut.Markup, settings);
    }

    [Fact]
    public async Task WithChildContent_RendersCorrectly()
    {
        // Act
        var cut = Render<IonGrid>(parameters => parameters
            .AddChildContent("<ion-row></ion-row>"));

        // Assert
        await Verify(cut.Markup);
    }

    [Fact]
    public async Task WithAttributes_RendersCorrectly()
    {
        // Act
        var cut = Render<IonGrid>(parameters => parameters
            .Add(p => p.Attributes, new Dictionary<string, object>
            {
                { "id", "grid" }
            }));

        // Assert
        await Verify(cut.Markup);
    }
}
