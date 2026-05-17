using FluentAssertions;

namespace IonBlazor.UnitTests.Components;

public class IonCheckboxTests : IonTestContext
{
    // ---------------------------------------------------------------------------
    // Render tests
    // ---------------------------------------------------------------------------

    [Fact]
    public async Task IonCheckboxRendersCorrectly()
    {
        var cut = Render<IonCheckbox>();
        await Verify(cut.Markup);
    }

    [Theory]
    [InlineData(IonMode.iOS)]
    [InlineData(IonMode.MaterialDesign)]
    public async Task WithMode_RendersCorrectly(string mode)
    {
        VerifySettings settings = new();
        settings.UseTextForParameters($"mode={mode}");

        var cut = Render<IonCheckbox>(parameters => parameters
            .Add(p => p.Mode, mode));

        await Verify(cut.Markup, settings);
    }

    [Theory]
    [InlineData(IonColor.Primary)]
    [InlineData(IonColor.Secondary)]
    [InlineData(IonColor.Danger)]
    public async Task WithColor_RendersCorrectly(string color)
    {
        VerifySettings settings = new();
        settings.UseTextForParameters($"color={color}");

        var cut = Render<IonCheckbox>(parameters => parameters
            .Add(p => p.Color, color));

        await Verify(cut.Markup, settings);
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public async Task WithChecked_RendersCorrectly(bool value)
    {
        VerifySettings settings = new();
        settings.UseTextForParameters($"value={value}");

        var cut = Render<IonCheckbox>(parameters => parameters
            .Add(p => p.Checked, value));

        await Verify(cut.Markup, settings);
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public async Task WithDisabled_RendersCorrectly(bool value)
    {
        VerifySettings settings = new();
        settings.UseTextForParameters($"value={value}");

        var cut = Render<IonCheckbox>(parameters => parameters
            .Add(p => p.Disabled, value));

        await Verify(cut.Markup, settings);
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public async Task WithIndeterminate_RendersCorrectly(bool value)
    {
        VerifySettings settings = new();
        settings.UseTextForParameters($"value={value}");

        var cut = Render<IonCheckbox>(parameters => parameters
            .Add(p => p.Indeterminate, value));

        await Verify(cut.Markup, settings);
    }

    [Fact]
    public async Task WithChildContent_RendersCorrectly()
    {
        var cut = Render<IonCheckbox>(parameters => parameters
            .AddChildContent("Accept terms"));

        await Verify(cut.Markup);
    }

    [Fact]
    public async Task WithAttributes_RendersCorrectly()
    {
        var cut = Render<IonCheckbox>(parameters => parameters
            .Add(p => p.Attributes, new Dictionary<string, object>
            {
                { "id", "checkbox" }
            }));

        await Verify(cut.Markup);
    }

    // ---------------------------------------------------------------------------
    // @bind-Checked: parallel CheckedChanged + IonChange callbacks
    // ---------------------------------------------------------------------------

    [Fact]
    public async Task IonChange_FiresBoth_CheckedChangedAndIonChange()
    {
        bool? capturedChecked = null;
        IonCheckboxChangeEventArgs? capturedArgs = null;

        var cut = Render<IonCheckbox>(parameters => parameters
            .Add(p => p.CheckedChanged, v => capturedChecked = v)
            .Add(p => p.IonChange, args => capturedArgs = args));

        var payload = new System.Text.Json.Nodes.JsonObject
        {
            ["detail"] = new System.Text.Json.Nodes.JsonObject { ["checked"] = true, ["value"] = "yes" }
        };
        await InvokeIonEventAsync("ionChange", payload);

        capturedChecked.Should().Be(true);
        capturedArgs.Should().NotBeNull();
        capturedArgs!.Checked.Should().Be(true);
        capturedArgs.Value.Should().Be("yes");
        cut.Instance.Checked.Should().Be(true);
    }
}
