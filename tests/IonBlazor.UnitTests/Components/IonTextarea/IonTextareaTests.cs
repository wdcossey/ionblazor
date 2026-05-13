using FluentAssertions;
using Microsoft.AspNetCore.Components;

namespace IonBlazor.UnitTests.Components;

public class IonTextareaTests : IonTestContext
{
    public IonTextareaTests()
    {
        SetupComponentModule<IonTextarea>(module =>
        {
            module.SetupVoid("counterFormat", _ => true).SetVoidResult();
            module.SetupVoid("setFocus", _ => true).SetVoidResult();
            module.SetupVoid("setValue", _ => true).SetVoidResult();
            module.SetupVoid("markTouched", _ => true).SetVoidResult();
            module.SetupVoid("markUnTouched", _ => true).SetVoidResult();
            module.SetupVoid("markInvalid", _ => true).SetVoidResult();
            module.SetupVoid("markValid", _ => true).SetVoidResult();
            module.SetupVoid("removeMarking", _ => true).SetVoidResult();
        });
    }

    // ---------------------------------------------------------------------------
    // Render tests
    // ---------------------------------------------------------------------------

    [Fact]
    public async Task IonTextareaRendersCorrectly()
    {
        var cut = Render<IonTextarea>();
        await Verify(cut.Markup);
    }

    [Theory]
    [InlineData(IonMode.iOS)]
    [InlineData(IonMode.MaterialDesign)]
    public async Task WithMode_RendersCorrectly(string mode)
    {
        VerifySettings settings = new();
        settings.UseTextForParameters($"mode={mode}");

        var cut = Render<IonTextarea>(parameters => parameters
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

        var cut = Render<IonTextarea>(parameters => parameters
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

        var cut = Render<IonTextarea>(parameters => parameters
            .Add(p => p.Disabled, value));

        await Verify(cut.Markup, settings);
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public async Task WithReadonly_RendersCorrectly(bool value)
    {
        VerifySettings settings = new();
        settings.UseTextForParameters($"value={value}");

        var cut = Render<IonTextarea>(parameters => parameters
            .Add(p => p.Readonly, value));

        await Verify(cut.Markup, settings);
    }

    [Fact]
    public async Task WithPlaceholder_RendersCorrectly()
    {
        var cut = Render<IonTextarea>(parameters => parameters
            .Add(p => p.Placeholder, "Enter text here"));

        await Verify(cut.Markup);
    }

    [Fact]
    public async Task WithLabel_RendersCorrectly()
    {
        var cut = Render<IonTextarea>(parameters => parameters
            .Add(p => p.Label, "My Label"));

        await Verify(cut.Markup);
    }

    [Fact]
    public async Task WithChildContent_RendersCorrectly()
    {
        var cut = Render<IonTextarea>(parameters => parameters
            .AddChildContent("<div slot=\"label\">Label</div>"));

        await Verify(cut.Markup);
    }

    [Fact]
    public async Task WithAttributes_RendersCorrectly()
    {
        var cut = Render<IonTextarea>(parameters => parameters
            .Add(p => p.Attributes, new Dictionary<string, object>
            {
                { "id", "textarea" }
            }));

        await Verify(cut.Markup);
    }

    // ---------------------------------------------------------------------------
    // JS interop tests
    // ---------------------------------------------------------------------------

    [Fact]
    public async Task SetFocusAsync_InvokesJsMethod_WhenCalled()
    {
        var cut = Render<IonTextarea>();

        await cut.Instance.SetFocusAsync();

        JSRuntimeInvocation invocation = JSInterop.Invocations["setFocus"].Single();
        invocation.Arguments[0]
            .Should().BeAssignableTo<ElementReference>()
            .Which.Should().Be(cut.Instance.IonElement);
    }

    [Fact]
    public async Task SetValueAsync_InvokesJsMethod_WhenCalled()
    {
        var cut = Render<IonTextarea>();

        await cut.Instance.SetValueAsync("new value");

        JSRuntimeInvocation invocation = JSInterop.Invocations["setValue"].Single();
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
        var cut = Render<IonTextarea>();
        Assert.Equal(nameof(IonTextarea), cut.Instance.JsImportName);
    }

    // ---------------------------------------------------------------------------
    // @bind-Value: parallel ValueChanged + IonChange callbacks
    // ---------------------------------------------------------------------------

    [Fact]
    public async Task IonChange_FiresBoth_ValueChangedAndIonChange()
    {
        string? capturedValue = null;
        IonTextareaChangeEventArgs? capturedArgs = null;

        var cut = Render<IonTextarea>(parameters => parameters
            .Add(p => p.ValueChanged, v => capturedValue = v)
            .Add(p => p.IonChange, args => capturedArgs = args));

        var payload = new System.Text.Json.Nodes.JsonObject
        {
            ["detail"] = new System.Text.Json.Nodes.JsonObject
            {
                ["value"] = "multi\nline",
                ["event"] = new System.Text.Json.Nodes.JsonObject { ["isTrusted"] = true }
            }
        };
        await InvokeIonEventAsync("ionChange", payload);

        capturedValue.Should().Be("multi\nline");
        capturedArgs.Should().NotBeNull();
        capturedArgs!.Value.Should().Be("multi\nline");
        cut.Instance.Value.Should().Be("multi\nline");
    }

    [Fact]
    public async Task IonInput_FiresBoth_ValueInputAndIonInput()
    {
        string? capturedValue = null;
        IonTextareaInputEventArgs? capturedArgs = null;

        var cut = Render<IonTextarea>(parameters => parameters
            .Add(p => p.ValueInput, v => capturedValue = v)
            .Add(p => p.IonInput, args => capturedArgs = args));

        var payload = new System.Text.Json.Nodes.JsonObject
        {
            ["detail"] = new System.Text.Json.Nodes.JsonObject
            {
                ["value"] = "typing",
                ["event"] = new System.Text.Json.Nodes.JsonObject { ["isTrusted"] = true }
            }
        };
        await InvokeIonEventAsync("ionInput", payload);

        capturedValue.Should().Be("typing");
        capturedArgs.Should().NotBeNull();
        capturedArgs!.Value.Should().Be("typing");
        cut.Instance.Value.Should().Be("typing");
    }
}
