using System.Collections.Immutable;

namespace IonBlazor.Components;

public sealed class AlertInputBuilder
{
    private readonly IList<IAlertInput> _inputs = [];

    internal AlertInputBuilder() { }

    internal IImmutableList<IAlertInput> Build()
    {
        return _inputs.ToImmutableList();
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