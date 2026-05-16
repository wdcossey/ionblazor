using FluentAssertions;
using IonBlazor.Mcp.Resources;

namespace IonBlazor.UnitTests.Mcp;

public class ComponentRegistryTests
{
    [Fact]
    public void ListAll_DiscoversAllPublicSealedComponents()
    {
        var components = ComponentRegistry.ListAll();

        components.Should().NotBeEmpty();
        components.Should().OnlyHaveUniqueItems(c => c.Name);
        components.Should().BeInAscendingOrder(c => c.Name);
    }

    [Fact]
    public void ListAll_IncludesIonButton_AsContentComponentWithoutJsInterop()
    {
        var ionButton = ComponentRegistry.ListAll().Single(c => c.Name == "IonButton");

        ionButton.BaseClass.Should().Be("IonContentComponent");
        ionButton.HasJsInterop.Should().BeFalse();
        ionButton.HasChildContent.Should().BeTrue();
        ionButton.JsImportName.Should().BeNull();
        ionButton.Interfaces.Should().BeEquivalentTo("IIonColorComponent", "IIonModeComponent");
    }

    [Fact]
    public void ListAll_IncludesIonInput_AsJsContentComponentWithBindSupport()
    {
        var ionInput = ComponentRegistry.ListAll().Single(c => c.Name == "IonInput");

        ionInput.BaseClass.Should().Be("IonJsContentComponent");
        ionInput.HasJsInterop.Should().BeTrue();
        ionInput.JsImportName.Should().Be("IonInput");
        ionInput.BindProperties.Should().Contain("Value");
    }

    [Fact]
    public void ListAll_IncludesGenericIonSelect_WithAngleBracketName()
    {
        var ionSelect = ComponentRegistry.ListAll().SingleOrDefault(c => c.Name == "IonSelect<TValue>");

        ionSelect.Should().NotBeNull("generic components must render with their type parameter names");
        ionSelect!.BaseClass.Should().Be("IonJsContentComponent");
    }

    [Fact]
    public void GetMetadata_ReturnsNull_WhenComponentUnknown()
    {
        var metadata = ComponentRegistry.GetMetadata("IonDoesNotExist");

        metadata.Should().BeNull();
    }

    [Fact]
    public void GetMetadata_IonInput_DetectsValueBindWithBothCommitAndLiveCallbacks()
    {
        var meta = ComponentRegistry.GetMetadata("IonInput");

        meta.Should().NotBeNull();
        var bind = meta!.Binds.Single(b => b.PropertyName == "Value");
        bind.ChangedCallbackName.Should().Be("ValueChanged");
        bind.InputCallbackName.Should().Be("ValueInput");
    }

    [Fact]
    public void GetMetadata_IonInput_PreservesNullabilityOnEventPayloads()
    {
        var meta = ComponentRegistry.GetMetadata("IonInput");

        meta.Should().NotBeNull();
        var valueChanged = meta!.Events.Single(e => e.Name == "ValueChanged");
        valueChanged.PayloadType.Should().Be("string?");
    }

    [Fact]
    public void GetMetadata_IonInput_ListsJsMethodsButExcludesDisposeAsync()
    {
        var meta = ComponentRegistry.GetMetadata("IonInput");

        meta.Should().NotBeNull();
        meta!.JsMethods.Select(m => m.Name).Should().Contain([
            "SetFocusAsync",
            "SetValueAsync",
            "MarkValidAsync",
            "MarkInvalidAsync",
        ]);
        meta.JsMethods.Select(m => m.Name).Should().NotContain("DisposeAsync");
    }

    [Fact]
    public void GetMetadata_IonButton_HasNoJsMethods_BecauseItIsNotJsBacked()
    {
        var meta = ComponentRegistry.GetMetadata("IonButton");

        meta.Should().NotBeNull();
        meta!.JsMethods.Should().BeEmpty();
    }

    [Fact]
    public void GetMetadata_IonButton_HasCascadingParameter_Parent()
    {
        var meta = ComponentRegistry.GetMetadata("IonButton");

        meta.Should().NotBeNull();
        var parent = meta!.CascadingParameters.Single(c => c.Name == "Parent");
        parent.CascadingName.Should().Be("Parent");
    }

    [Fact]
    public void GetMetadata_IonSelectGeneric_PropagatesTypeParameterIntoEventPayloads()
    {
        var meta = ComponentRegistry.GetMetadata("IonSelect<TValue>");

        meta.Should().NotBeNull();
        var ionChange = meta!.Events.Single(e => e.Name == "IonChange");
        ionChange.PayloadType.Should().Be("IonSelectChangeEventArgs<TValue>");
    }

    [Fact]
    public void GetMetadata_IonInput_Value_SurfacesPropertyXmlSummary()
    {
        var meta = ComponentRegistry.GetMetadata("IonInput");

        meta.Should().NotBeNull();
        var value = meta!.Parameters.Single(p => p.Name == "Value");
        value.Description.Should().Be("The value of the input.");
    }

    [Fact]
    public void GetMetadata_IonInput_SetFocusAsync_SurfacesMethodXmlSummary()
    {
        var meta = ComponentRegistry.GetMetadata("IonInput");

        meta.Should().NotBeNull();
        var setFocus = meta!.JsMethods.Single(m => m.Name == "SetFocusAsync");
        setFocus.Description.Should().NotBeNullOrWhiteSpace();
        setFocus.Description.Should().Contain("focus");
    }

    [Fact]
    public void GetMetadata_IonInput_ValueBind_InheritsValuePropertySummary()
    {
        var meta = ComponentRegistry.GetMetadata("IonInput");

        meta.Should().NotBeNull();
        var bind = meta!.Binds.Single(b => b.PropertyName == "Value");
        bind.Description.Should().Be("The value of the input.");
    }

    [Fact]
    public void GetMetadata_IonButton_Color_InheritsInterfaceXmlSummary()
    {
        var meta = ComponentRegistry.GetMetadata("IonButton");

        meta.Should().NotBeNull();
        var color = meta!.Parameters.Single(p => p.Name == "Color");
        color.Description.Should().NotBeNullOrWhiteSpace();
        color.Description.Should().Contain("color");
    }
}
