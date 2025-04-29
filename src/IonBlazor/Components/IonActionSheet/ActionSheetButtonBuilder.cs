using System.Collections.Immutable;

namespace IonBlazor.Components;

public sealed class ActionSheetButtonBuilder
{
    private readonly List<IActionSheetButton> _buttons = [];
    internal ActionSheetButtonBuilder() { }

    public IImmutableList<IActionSheetButton> Build()
    {
        return _buttons.ToImmutableList();
    }

    public ActionSheetButtonBuilder Add<TButton>(Action<TButton> configure)
        where TButton : class, IActionSheetButton, new()
    {
        TButton button = new();
        configure(button);
        _buttons.Add(button);
        return this;
    }

    public ActionSheetButtonBuilder Add<TButton>(TButton button)
        where TButton : class, IActionSheetButton
    {
        _buttons.Add(button);
        return this;
    }


}