namespace IonBlazor.Components;

public sealed class ActionSheetButtonBuilder
{
    private readonly IList<IActionSheetButton> _buttons = new List<IActionSheetButton>();
    internal ActionSheetButtonBuilder() { }

    public IReadOnlyList<IActionSheetButton> Build()
    {
        return _buttons.ToArray();
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