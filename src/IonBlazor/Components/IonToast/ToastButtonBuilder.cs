namespace IonBlazor.Components;

public sealed class ToastButtonBuilder
{
    private readonly IList<IIonToastButton> _buttons = new List<IIonToastButton>();
    internal ToastButtonBuilder() { }

    public IReadOnlyList<IIonToastButton> Build()
    {
        return _buttons.ToArray();
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