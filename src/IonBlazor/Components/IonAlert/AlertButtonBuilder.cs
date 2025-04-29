namespace IonBlazor.Components;

public sealed class AlertButtonBuilder
{
    private readonly IList<IAlertButton> _buttons = new List<IAlertButton>();
    internal AlertButtonBuilder() { }

    public IReadOnlyList<IAlertButton> Build()
    {
        return _buttons.ToArray();
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