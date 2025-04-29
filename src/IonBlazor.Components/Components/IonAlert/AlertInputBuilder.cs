namespace IonBlazor.Components;

public sealed class AlertInputBuilder
{
    private readonly IList<IAlertInput> _inputs = new List<IAlertInput>();
    internal AlertInputBuilder() { }

    public IReadOnlyList<IAlertInput> Build()
    {
        return _inputs.ToArray();
    }

    public AlertInputBuilder Add<TInput>(Action<TInput> configure) where TInput : class, IAlertInput, new()
    {
        TInput input = new();
        configure(input);
        _inputs.Add(input);
        return this;
    }

    public AlertInputBuilder Add<TInput>(TInput input) where TInput : class, IAlertInput
    {
        _inputs.Add(input);
        return this;
    }


}