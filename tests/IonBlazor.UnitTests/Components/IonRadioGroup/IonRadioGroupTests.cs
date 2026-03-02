namespace IonBlazor.UnitTests.Components;

public class IonRadioGroupTests : IonTestContext
{
    // ---------------------------------------------------------------------------
    // Render tests
    // ---------------------------------------------------------------------------

    [Fact]
    public async Task IonRadioGroupRendersCorrectly()
    {
        var cut = Render<IonRadioGroup>();
        await Verify(cut.Markup);
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public async Task WithAllowEmptySelection_RendersCorrectly(bool value)
    {
        VerifySettings settings = new();
        settings.UseTextForParameters($"value={value}");

        var cut = Render<IonRadioGroup>(parameters => parameters
            .Add(p => p.AllowEmptySelection, value));

        await Verify(cut.Markup, settings);
    }

    [Fact]
    public async Task WithValue_RendersCorrectly()
    {
        var cut = Render<IonRadioGroup>(parameters => parameters
            .Add(p => p.Value, "option-1"));

        await Verify(cut.Markup);
    }

    [Fact]
    public async Task WithChildContent_RendersCorrectly()
    {
        var cut = Render<IonRadioGroup>(parameters => parameters
            .AddChildContent("<ion-radio value=\"a\">Option A</ion-radio>"));

        await Verify(cut.Markup);
    }

    [Fact]
    public async Task WithAttributes_RendersCorrectly()
    {
        var cut = Render<IonRadioGroup>(parameters => parameters
            .Add(p => p.Attributes, new Dictionary<string, object>
            {
                { "id", "radio-group" }
            }));

        await Verify(cut.Markup);
    }
}
