using System.Text.Json.Nodes;
using FluentAssertions;
using Microsoft.AspNetCore.Components;

namespace IonBlazor.UnitTests.Components;

public class IonPickerColumnTests : IonTestContext
{
    public IonPickerColumnTests()
    {
        SetupComponentModule<IonPickerColumn>(module =>
        {
            module.Setup<string>("setFocus", _ => true).SetResult("");
        });
    }

    // ---------------------------------------------------------------------------
    // Render tests
    // ---------------------------------------------------------------------------

    [Fact]
    public async Task IonPickerColumnRendersCorrectly()
    {
        var cut = Render<IonPickerColumn>();
        await Verify(cut.Markup);
    }

    [Theory]
    [InlineData(IonMode.iOS)]
    [InlineData(IonMode.MaterialDesign)]
    public async Task WithMode_RendersCorrectly(string mode)
    {
        VerifySettings settings = new();
        settings.UseTextForParameters($"mode={mode}");

        var cut = Render<IonPickerColumn>(parameters => parameters
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

        var cut = Render<IonPickerColumn>(parameters => parameters
            .Add(p => p.Color, color));

        await Verify(cut.Markup, settings);
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public async Task WithDisabled_RendersCorrectly(bool value)
    {
        VerifySettings settings = new();
        settings.UseTextForParameters($"value={value}");

        var cut = Render<IonPickerColumn>(parameters => parameters
            .Add(p => p.Disabled, value));

        await Verify(cut.Markup, settings);
    }

    [Fact]
    public async Task WithValue_RendersCorrectly()
    {
        var cut = Render<IonPickerColumn>(parameters => parameters
            .Add(p => p.Value, "option-1"));

        await Verify(cut.Markup);
    }

    [Fact]
    public async Task WithChildContent_RendersCorrectly()
    {
        var cut = Render<IonPickerColumn>(parameters => parameters
            .AddChildContent("<ion-picker-column-option value=\"a\">Option A</ion-picker-column-option>"));

        await Verify(cut.Markup);
    }

    [Fact]
    public async Task WithAttributes_RendersCorrectly()
    {
        var cut = Render<IonPickerColumn>(parameters => parameters
            .Add(p => p.Attributes, new Dictionary<string, object>
            {
                { "id", "picker-column" }
            }));

        await Verify(cut.Markup);
    }

    // ---------------------------------------------------------------------------
    // JS interop tests
    // ---------------------------------------------------------------------------

    [Fact]
    public async Task SetFocusAsync_InvokesJsMethod_WhenCalled()
    {
        var cut = Render<IonPickerColumn>();

        await cut.Instance.SetFocusAsync();

        JSRuntimeInvocation invocation = JSInterop.Invocations["setFocus"].Single();
        invocation.Arguments[0]
            .Should().BeAssignableTo<ElementReference>()
            .Which.Should().Be(cut.Instance.IonElement);
    }

    // ---------------------------------------------------------------------------
    // JsImportName
    // ---------------------------------------------------------------------------

    [Fact]
    public void Assert_JsImportName()
    {
        var cut = Render<IonPickerColumn>();
        Assert.Equal(nameof(IonPickerColumn), cut.Instance.JsImportName);
    }

    // ---------------------------------------------------------------------------
    // @bind-Value: parallel ValueChanged + IonChange callbacks
    // ---------------------------------------------------------------------------

    [Fact]
    public async Task IonChange_FiresBoth_ValueChangedAndIonChange()
    {
        string? capturedValue = null;
        IonPickerColumnIonChangeEventArgs? capturedArgs = null;

        var cut = Render<IonPickerColumn>(parameters => parameters
            .Add(p => p.ValueChanged, v => capturedValue = v)
            .Add(p => p.IonChange, args => capturedArgs = args));

        var payload = new JsonObject
        {
            ["detail"] = new JsonObject { ["value"] = "option-2" }
        };
        await InvokeIonEventAsync("ionChange", payload);

        capturedValue.Should().Be("option-2");
        capturedArgs.Should().NotBeNull();
        capturedArgs!.Value.Should().Be("option-2");
        capturedArgs.Sender.Should().BeSameAs(cut.Instance);
        cut.Instance.Value.Should().Be("option-2");
    }
}
