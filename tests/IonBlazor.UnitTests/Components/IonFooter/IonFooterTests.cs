namespace IonBlazor.UnitTests.Components;

public class IonFooterTests : IonTestContext
{
    [Fact]
    public async Task IonFooterRendersCorrectly()
    {
        // Act
        var cut = Render<IonFooter>();

        // Assert
        await Verify(cut.Markup);
    }

    [Theory]
    [InlineData(IonFooterCollapse.Fade)]
    public async Task WithCollapse_RendersCorrectly(string collapse)
    {
        VerifySettings settings = new();
        settings.UseTextForParameters($"collapse={collapse}");

        // Act
        var cut = Render<IonFooter>(parameters => parameters
            .Add(p => p.Collapse, collapse));

        // Assert
        await Verify(cut.Markup, settings);
    }

    [Theory]
    [InlineData(IonMode.iOS)]
    [InlineData(IonMode.MaterialDesign)]
    public async Task WithMode_RendersCorrectly(string mode)
    {
        VerifySettings settings = new();
        settings.UseTextForParameters($"mode={mode}");

        // Act
        var cut = Render<IonFooter>(parameters => parameters
            .Add(p => p.Mode, mode));

        // Assert
        await Verify(cut.Markup, settings);
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public async Task WithTranslucent_RendersCorrectly(bool value)
    {
        VerifySettings settings = new();
        settings.UseTextForParameters($"value={value}");

        // Act
        var cut = Render<IonFooter>(parameters => parameters
            .Add(p => p.Translucent, value));

        // Assert
        await Verify(cut.Markup, settings);
    }

    [Fact]
    public async Task WithChildContent_RendersCorrectly()
    {
        // Act
        var cut = Render<IonFooter>(parameters => parameters
            .AddChildContent("<ion-toolbar></ion-toolbar>"));

        // Assert
        await Verify(cut.Markup);
    }

    [Fact]
    public async Task WithAttributes_RendersCorrectly()
    {
        // Act
        var cut = Render<IonFooter>(parameters => parameters
            .Add(p => p.Attributes, new Dictionary<string, object>
            {
                { "id", "footer" }
            }));

        // Assert
        await Verify(cut.Markup);
    }
}
