﻿namespace IonBlazor.Components;

internal sealed class IonEvent
{
    [JsonPropertyName("event")]
    public string Event { get; private set; }

    [JsonPropertyName("ref")]
    public object? Reference { get; private set; }

    private IonEvent(string @event, object? reference)
    {
        Event = @event;
        Reference = reference;
    }

    internal static IonEvent Set<TArgs>(string @event, DotNetObjectReference<TArgs> reference)
        where TArgs : class => new(@event, reference);
}