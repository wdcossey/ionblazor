using System.Collections.Immutable;

namespace IonBlazor.Components;

public sealed class ToastButtonBuilder
{
    private readonly List<IIonToastButton> _buttons = [];

    internal ToastButtonBuilder() { }

    public IImmutableList<IIonToastButton> Build()
    {
        return _buttons.ToImmutableList();
    }

    public ToastButtonBuilder Add<TButton>(Action<TButton> configure) where TButton : class, IIonToastButton, new()
    {
        TButton button = new();
        configure(button);
        _buttons.Add(button);
        return this;
    }

    public ToastButtonBuilder Add<TButton>(TButton button) where TButton : class, IIonToastButton
    {
        _buttons.Add(button);
        return this;
    }


}