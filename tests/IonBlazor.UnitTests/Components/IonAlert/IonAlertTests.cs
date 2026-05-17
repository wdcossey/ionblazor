using System.Collections.Immutable;
using FluentAssertions;
using Microsoft.AspNetCore.Components;

namespace IonBlazor.UnitTests.Components;

public class IonAlertTests : IonTestContext
{
    public IonAlertTests()
    {
        SetupComponentModule(nameof(IonAlert), module =>
        {
            module.SetupVoid("addButtons", _ => true).SetVoidResult();
            module.SetupVoid("addInputs", _ => true).SetVoidResult();
            module.Setup<bool>("dismiss", _ => true).SetResult(true);
            module.SetupVoid("present", _ => true).SetVoidResult();
        });
    }

    // ---------------------------------------------------------------------------
    // Render tests
    // ---------------------------------------------------------------------------

    [Fact]
    public async Task IonAlertRendersCorrectly()
    {
        // Act
        var cut = Render<IonAlert>();

        // Assert
        await Verify(cut.Markup);
    }

    [Theory]
    [InlineData(IonMode.iOS)]
    [InlineData(IonMode.MaterialDesign)]
    public async Task WithMode_RendersCorrectly(string mode)
    {
        VerifySettings settings = new();
        settings.UseTextForParameters($"mode={mode}");

        // Act
        var cut = Render<IonAlert>(parameters => parameters
            .Add(p => p.Mode, mode));

        // Assert
        await Verify(cut.Markup, settings);
    }

    [Theory]
    [InlineData(true, "true")]
    [InlineData(false, "false")]
    public async Task WithAnimated_RendersCorrectly(bool isAnimated, string expected)
    {
        VerifySettings settings = new();
        settings.UseTextForParameters($"isAnimated={isAnimated}_expected={expected}");

        // Act
        var cut = Render<IonAlert>(parameters => parameters
            .Add(p => p.Animated, isAnimated));

        // Assert
        await Verify(cut.Markup, settings);
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public async Task WithBackdropDismiss_RendersCorrectly(bool value)
    {
        VerifySettings settings = new();
        settings.UseTextForParameters($"value={value}");

        // Act
        var cut = Render<IonAlert>(parameters => parameters
            .Add(p => p.BackdropDismiss, value));

        // Assert
        await Verify(cut.Markup, settings);
    }

    [Fact]
    public async Task WithCssClass_RendersCorrectly()
    {
        // Act
        var cut = Render<IonAlert>(parameters => parameters
            .Add(p => p.CssClass, "my-alert"));

        // Assert
        await Verify(cut.Markup);
    }

    [Fact]
    public async Task WithHeader_RendersCorrectly()
    {
        // Act
        var cut = Render<IonAlert>(parameters => parameters
            .Add(p => p.Header, "Alert Header"));

        // Assert
        await Verify(cut.Markup);
    }

    [Fact]
    public async Task WithSubHeader_RendersCorrectly()
    {
        // Act
        var cut = Render<IonAlert>(parameters => parameters
            .Add(p => p.SubHeader, "Alert Sub Header"));

        // Assert
        await Verify(cut.Markup);
    }

    [Fact]
    public async Task WithMessage_RendersCorrectly()
    {
        // Act
        var cut = Render<IonAlert>(parameters => parameters
            .Add(p => p.Message, "Alert message"));

        // Assert
        await Verify(cut.Markup);
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public async Task WithIsOpen_RendersCorrectly(bool value)
    {
        VerifySettings settings = new();
        settings.UseTextForParameters($"value={value}");

        // Act
        var cut = Render<IonAlert>(parameters => parameters
            .Add(p => p.IsOpen, value));

        // Assert
        await Verify(cut.Markup, settings);
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public async Task WithKeyboardClose_RendersCorrectly(bool value)
    {
        VerifySettings settings = new();
        settings.UseTextForParameters($"value={value}");

        // Act
        var cut = Render<IonAlert>(parameters => parameters
            .Add(p => p.KeyboardClose, value));

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
        var cut = Render<IonAlert>(parameters => parameters
            .Add(p => p.Translucent, value));

        // Assert
        await Verify(cut.Markup, settings);
    }

    [Fact]
    public async Task WithTrigger_RendersCorrectly()
    {
        // Act
        var cut = Render<IonAlert>(parameters => parameters
            .Add(p => p.Trigger, "open-alert"));

        // Assert
        await Verify(cut.Markup);
    }

    [Fact]
    public async Task WithAttributes_RendersCorrectly()
    {
        // Act
        var cut = Render<IonAlert>(parameters => parameters
            .Add(p => p.Attributes, new Dictionary<string, object>
            {
                { "id", "accordion" }
            }));

        // Assert
        await Verify(cut.Markup);
    }

    // ---------------------------------------------------------------------------
    // JS interop tests
    // ---------------------------------------------------------------------------

    [Fact]
    public async Task DismissAsync_InvokesJsMethod_WhenCalled()
    {
        // Arrange
        var cut = Render<IonAlert>();

        // Act
        await cut.Instance.DismissAsync();

        // Assert
        JSRuntimeInvocation invocation = JSInterop.Invocations["dismiss"].Single();
        invocation.Arguments[0]
            .Should().BeAssignableTo<ElementReference>()
            .Which.Should().Be(cut.Instance.IonElement);
    }

    [Fact]
    public async Task PresentAsync_InvokesJsMethod_WhenCalled()
    {
        // Arrange

        var cut = Render<IonAlert>();

        // Act
        await cut.Instance.PresentAsync();

        // Assert
        JSRuntimeInvocation invocation = JSInterop.Invocations["present"].Single();
        invocation.Arguments[0]
            .Should().BeAssignableTo<ElementReference>()
            .Which.Should().Be(cut.Instance.IonElement);
    }

    [Fact]
    public void WithButtonsBuilder_InvokesAddButtons_WhenRendered()
    {
        // Act
        Render<IonAlert>(parameters => parameters
            .Add(p => p.ButtonsBuilder, builder => builder
                .Add(new AlertButton { Text = "OK", Role = "confirm" })
                .Add(new AlertButton { Text = "Cancel", Role = "cancel" })));

        // Act

        // Assert
        JSRuntimeInvocation invocation = JSInterop.Invocations["addButtons"].Single();
        invocation.Arguments[1].Should().BeAssignableTo<IReadOnlyList<IAlertButton>>()
            .Which.Count.Should().Be(2);
    }

    [Fact]
    public void WithButtonsBuilder_UsingConfigureOverload_InvokesAddButtons_WhenRendered()
    {
        // Act
        Render<IonAlert>(parameters => parameters
            .Add(p => p.ButtonsBuilder, builder => builder
                .Add<AlertButton>(b => { b.Text = "OK"; b.Role = "confirm"; })
                .Add<AlertButton>(b => { b.Text = "Cancel"; b.Role = "cancel"; })));

        // Assert
        JSRuntimeInvocation invocation = JSInterop.Invocations["addButtons"].Single();
        invocation.Arguments[1].Should().BeAssignableTo<IReadOnlyList<IAlertButton>>()
            .Which.Count.Should().Be(2);
    }

    [Fact]
    public void WithInputsBuilder_InvokesAddInputs_WhenRendered()
    {
        // Act
        Render<IonAlert>(parameters => parameters
            .Add(p => p.InputsBuilder, builder => builder
                .Add(new AlertInput { Name = "name", Placeholder = "Your name" })));

        // Assert
        JSRuntimeInvocation invocation = JSInterop.Invocations["addInputs"].Single();
        invocation.Arguments[1].Should().BeAssignableTo<IImmutableList<IAlertInput>>()
            .Which.Count.Should().Be(1);
    }

    // ---------------------------------------------------------------------------
    // JsImportName
    // ---------------------------------------------------------------------------

    [Fact]
    public void Assert_JsImportName()
    {
        // Arrange
        var cut = Render<IonAlert>();

        // Assert
        Assert.Equal(nameof(IonAlert), cut.Instance.JsImportName);
    }
}