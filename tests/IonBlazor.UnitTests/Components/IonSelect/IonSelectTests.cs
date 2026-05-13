using System.Text.Json.Nodes;
using FluentAssertions;
using Microsoft.AspNetCore.Components;

namespace IonBlazor.UnitTests.Components;

public class IonSelectTests : IonTestContext
{
    public IonSelectTests()
    {
        SetupComponentModule(nameof(IonSelect<string>), module =>
        {
            module.SetupVoid("open", _ => true).SetVoidResult();
            module.SetupVoid("setValue", _ => true).SetVoidResult();
        });
    }

    // ---------------------------------------------------------------------------
    // Render tests
    // ---------------------------------------------------------------------------

    [Fact]
    public async Task IonSelectRendersCorrectly()
    {
        var cut = Render<IonSelect<string>>();
        await Verify(cut.Markup);
    }

    [Theory]
    [InlineData(IonMode.iOS)]
    [InlineData(IonMode.MaterialDesign)]
    public async Task WithMode_RendersCorrectly(string mode)
    {
        VerifySettings settings = new();
        settings.UseTextForParameters($"mode={mode}");

        var cut = Render<IonSelect<string>>(parameters => parameters
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

        var cut = Render<IonSelect<string>>(parameters => parameters
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

        var cut = Render<IonSelect<string>>(parameters => parameters
            .Add(p => p.Disabled, value));

        await Verify(cut.Markup, settings);
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public async Task WithMultiple_RendersCorrectly(bool value)
    {
        VerifySettings settings = new();
        settings.UseTextForParameters($"value={value}");

        var cut = Render<IonSelect<string>>(parameters => parameters
            .Add(p => p.Multiple, value));

        await Verify(cut.Markup, settings);
    }

    [Fact]
    public async Task WithLabel_RendersCorrectly()
    {
        var cut = Render<IonSelect<string>>(parameters => parameters
            .Add(p => p.Label, "My Label"));

        await Verify(cut.Markup);
    }

    [Fact]
    public async Task WithPlaceholder_RendersCorrectly()
    {
        var cut = Render<IonSelect<string>>(parameters => parameters
            .Add(p => p.Placeholder, "Select an option"));

        await Verify(cut.Markup);
    }

    [Fact]
    public async Task WithChildContent_RendersCorrectly()
    {
        var cut = Render<IonSelect<string>>(parameters => parameters
            .AddChildContent("<ion-select-option value=\"a\">Option A</ion-select-option>"));

        await Verify(cut.Markup);
    }

    [Fact]
    public async Task WithAttributes_RendersCorrectly()
    {
        var cut = Render<IonSelect<string>>(parameters => parameters
            .Add(p => p.Attributes, new Dictionary<string, object>
            {
                { "id", "select" }
            }));

        await Verify(cut.Markup);
    }

    // ---------------------------------------------------------------------------
    // JS interop tests
    // ---------------------------------------------------------------------------

    [Fact]
    public async Task OpenAsync_InvokesJsMethod_WhenCalled()
    {
        var cut = Render<IonSelect<string>>();

        await cut.Instance.OpenAsync();

        JSRuntimeInvocation invocation = JSInterop.Invocations["open"].Single();
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
        var cut = Render<IonSelect<string>>();
        Assert.Equal(nameof(IonSelect<string>), cut.Instance.JsImportName);
    }

    // ---------------------------------------------------------------------------
    // @bind-Value: parallel ValueChanged + IonChange callbacks
    // ---------------------------------------------------------------------------

    [Fact]
    public async Task IonChange_FiresBoth_ValueChangedAndIonChange_ForSingleValue()
    {
        string[]? capturedValue = null;
        IonSelectChangeEventArgs<string>? capturedArgs = null;

        var cut = Render<IonSelect<string>>(parameters => parameters
            .Add(p => p.ValueChanged, v => capturedValue = v)
            .Add(p => p.IonChange, args => capturedArgs = args));

        var payload = new JsonObject
        {
            ["detail"] = new JsonObject { ["value"] = "apple" }
        };
        await InvokeIonEventAsync("ionChange", payload);

        capturedValue.Should().BeEquivalentTo(["apple"]);
        capturedArgs.Should().NotBeNull();
        capturedArgs!.Value.Should().BeEquivalentTo(["apple"]);
        cut.Instance.Value.Should().BeEquivalentTo(["apple"]);
    }

    [Fact]
    public async Task IonChange_FiresBoth_ValueChangedAndIonChange_ForMultipleValues()
    {
        string[]? capturedValue = null;
        IonSelectChangeEventArgs<string>? capturedArgs = null;

        var cut = Render<IonSelect<string>>(parameters => parameters
            .Add(p => p.Multiple, true)
            .Add(p => p.ValueChanged, v => capturedValue = v)
            .Add(p => p.IonChange, args => capturedArgs = args));

        var payload = new JsonObject
        {
            ["detail"] = new JsonObject
            {
                ["value"] = new JsonArray { "apple", "orange" }
            }
        };
        await InvokeIonEventAsync("ionChange", payload);

        capturedValue.Should().BeEquivalentTo(["apple", "orange"]);
        capturedArgs.Should().NotBeNull();
        capturedArgs!.Value.Should().BeEquivalentTo(["apple", "orange"]);
        cut.Instance.Value.Should().BeEquivalentTo(["apple", "orange"]);
    }

    [Fact]
    public async Task IonChange_NullValue_YieldsEmptyArray()
    {
        string[]? capturedValue = null;

        Render<IonSelect<string>>(parameters => parameters
            .Add(p => p.ValueChanged, v => capturedValue = v));

        var payload = new JsonObject
        {
            ["detail"] = new JsonObject { ["value"] = null }
        };
        await InvokeIonEventAsync("ionChange", payload);

        capturedValue.Should().NotBeNull().And.BeEmpty();
    }

    // ---------------------------------------------------------------------------
    // setValue JS interop: invoked on first render when seeded with multiple values
    // ---------------------------------------------------------------------------

    [Fact]
    public void OnAfterRender_InvokesSetValue_WhenMultipleSeedValuesProvided()
    {
        Render<IonSelect<string>>(parameters => parameters
            .Add(p => p.Multiple, true)
            .Add(p => p.Value, ["apple", "orange"]));

        JSRuntimeInvocation invocation = JSInterop.Invocations["setValue"].Single();
        invocation.Arguments[0]
            .Should().BeAssignableTo<ElementReference>();
        invocation.Arguments[1]
            .Should().BeAssignableTo<IEnumerable<string>>()
            .Which.Should().BeEquivalentTo(["apple", "orange"]);
    }

    [Fact]
    public void OnAfterRender_DoesNotInvokeSetValue_WhenSingleSeedValueProvided()
    {
        Render<IonSelect<string>>(parameters => parameters
            .Add(p => p.Value, ["apple"]));

        JSInterop.Invocations["setValue"].Should().BeEmpty();
    }
}
