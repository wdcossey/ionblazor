using System.Collections.Immutable;

namespace IonBlazor.Components;

public sealed class PickerLegacyButtonBuilder
{
    private readonly IList<IPickerButton> _buttons = [];

    internal PickerLegacyButtonBuilder() { }

    internal IImmutableList<IPickerButton> Build()
    {
        return _buttons.ToImmutableList();
    }

    public PickerLegacyButtonBuilder Add<TButton>(Action<TButton> configure) where TButton : class, IPickerButton, new()
    {
        TButton button = new();
        configure(button);
        _buttons.Add(button);
        return this;
    }

    public PickerLegacyButtonBuilder Add<TButton>(TButton button) where TButton : class, IPickerButton
    {
        _buttons.Add(button);
        return this;
    }
}