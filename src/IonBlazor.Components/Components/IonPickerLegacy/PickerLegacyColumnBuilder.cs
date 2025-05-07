using System.Collections.Immutable;

namespace IonBlazor.Components;

public sealed class PickerLegacyColumnBuilder
{
    private readonly IList<IPickerColumn> _columns = [];

    internal PickerLegacyColumnBuilder() { }

    internal IImmutableList<IPickerColumn> Build()
    {
        return _columns.ToImmutableList();
    }

    public PickerLegacyColumnBuilder Add<TColumn>(Action<TColumn> configure) where TColumn : class, IPickerColumn, new()
    {
        TColumn column = new();
        configure(column);
        _columns.Add(column);
        return this;
    }

    public PickerLegacyColumnBuilder Add<TColumn>(TColumn column) where TColumn : class, IPickerColumn
    {
        _columns.Add(column);
        return this;
    }
}