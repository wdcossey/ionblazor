using FluentAssertions;
using IonBlazor.Mcp.Tools;

namespace IonBlazor.UnitTests.Mcp;

/// <summary>
/// Covers the consistent + forgiving parameter contract on the three single-entity getter tools:
/// each accepts its typed <c>&lt;entity&gt;Name</c> parameter AND a generic <c>name</c> alias, and
/// returns a clear (non-exception) error — naming the accepted parameter(s) — when no name is
/// supplied or the named entity does not exist. Backward compatibility: the typed names
/// (componentName / serviceName) and value-set's historical <c>name</c> all still resolve.
/// </summary>
public class GetterToolParameterTests
{
    // ---- get_component_metadata --------------------------------------------------------------

    [Fact]
    public void GetComponentMetadata_WithComponentName_ReturnsMetadata()
    {
        var result = ComponentTools.GetComponentMetadata(componentName: "IonInput");

        result.Should().Contain("# IonInput");
        result.Should().NotContain("Error [");
    }

    [Fact]
    public void GetComponentMetadata_WithNameAlias_ReturnsMetadata()
    {
        var result = ComponentTools.GetComponentMetadata(name: "IonInput");

        result.Should().Contain("# IonInput");
        result.Should().NotContain("Error [");
    }

    [Fact]
    public void GetComponentMetadata_WithNoName_ReturnsDescriptiveError()
    {
        var result = ComponentTools.GetComponentMetadata();

        result.Should().Contain("get_component_metadata");
        result.Should().Contain("componentName");
        result.Should().Contain("'name'");
        result.Should().Contain("list_components");
    }

    [Fact]
    public void GetComponentMetadata_WithUnknownComponent_ReturnsDescriptiveError()
    {
        var result = ComponentTools.GetComponentMetadata(componentName: "IonDoesNotExist");

        result.Should().Contain("IonDoesNotExist");
        result.Should().Contain("not found");
        result.Should().Contain("componentName");
        result.Should().Contain("list_components");
    }

    // ---- get_service_metadata ----------------------------------------------------------------

    [Fact]
    public void GetServiceMetadata_WithServiceName_ReturnsMetadata()
    {
        var result = ServiceTools.GetServiceMetadata(serviceName: "IonAlertService");

        result.Should().Contain("# IonAlertService");
        result.Should().NotContain("Error [");
    }

    [Fact]
    public void GetServiceMetadata_WithNameAlias_ReturnsMetadata()
    {
        var result = ServiceTools.GetServiceMetadata(name: "IonAlertService");

        result.Should().Contain("# IonAlertService");
        result.Should().NotContain("Error [");
    }

    [Fact]
    public void GetServiceMetadata_WithNoName_ReturnsDescriptiveError()
    {
        var result = ServiceTools.GetServiceMetadata();

        result.Should().Contain("get_service_metadata");
        result.Should().Contain("serviceName");
        result.Should().Contain("'name'");
        result.Should().Contain("list_services");
    }

    [Fact]
    public void GetServiceMetadata_WithUnknownService_ReturnsDescriptiveError()
    {
        var result = ServiceTools.GetServiceMetadata(serviceName: "IonNopeService");

        result.Should().Contain("IonNopeService");
        result.Should().Contain("not found");
        result.Should().Contain("serviceName");
        result.Should().Contain("list_services");
    }

    // ---- get_value_set -----------------------------------------------------------------------

    [Fact]
    public void GetValueSet_WithValueSetName_ReturnsMetadata()
    {
        var result = ValueSetTools.GetValueSet(valueSetName: "IonMode");

        result.Should().Contain("# IonMode");
        result.Should().NotContain("Error [");
    }

    [Fact]
    public void GetValueSet_WithNameAlias_StillResolves_ForBackwardCompatibility()
    {
        var result = ValueSetTools.GetValueSet(name: "IonMode");

        result.Should().Contain("# IonMode");
        result.Should().NotContain("Error [");
    }

    [Fact]
    public void GetValueSet_WithNoName_ReturnsDescriptiveError()
    {
        var result = ValueSetTools.GetValueSet();

        result.Should().Contain("get_value_set");
        result.Should().Contain("valueSetName");
        result.Should().Contain("'name'");
        result.Should().Contain("list_value_sets");
    }

    [Fact]
    public void GetValueSet_WithUnknownValueSet_ReturnsDescriptiveError()
    {
        var result = ValueSetTools.GetValueSet(valueSetName: "IonNopeSet");

        result.Should().Contain("IonNopeSet");
        result.Should().Contain("not found");
        result.Should().Contain("valueSetName");
        result.Should().Contain("list_value_sets");
    }
}