using System.Collections.Immutable;

namespace IonBlazor.Components;

public sealed class AlertButtonBuilder
{
    private readonly List<IAlertButton> _buttons = [];
    internal AlertButtonBuilder() { }

    public IImmutableList<IAlertButton> Build()
    {
        return _buttons.ToImmutableList();
    }

    public AlertButtonBuilder Add<TButton>(Action<TButton> configure) where TButton : class, IAlertButton, new()
    {
        TButton button = new();
        configure(button);
        _buttons.Add(button);
        return this;
    }

    public AlertButtonBuilder Add<TButton>(TButton button) where TButton : class, IAlertButton
    {
        _buttons.Add(button);
        return this;
    }


}