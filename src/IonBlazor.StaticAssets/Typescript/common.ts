export interface DotNetObjectReference {
    invokeMethodAsync(method: string, ...args: unknown[]): Promise<unknown>;
}

interface ListenerConfig {
    event: string;
    ref: DotNetObjectReference;
}

interface TrackedListener {
    type: string;
    handler: EventListener;
}

// Tracks the concrete listener functions bound to each element so they can be
// removed when the owning Blazor component is disposed. Without this the DOM
// keeps invoking DotNetObjectReferences that .NET has already disposed (e.g. a
// teardown "ionBlur" fired while navigating away), producing
// "There is no tracked object with id ..." errors.
const trackedListeners = new WeakMap<HTMLElement, TrackedListener[]>();

export function attachListener(type: string, element: HTMLElement, ref: DotNetObjectReference): void {
    const handler: EventListener = (ev: Event) => {
        ref.invokeMethodAsync(dotNetCallbackMethod, { tagName: (ev.target as HTMLElement).tagName, detail: (ev as CustomEvent).detail })
            .catch(err => console.error(`IonBlazor: ${dotNetCallbackMethod} for "${type}" failed`, err));
    };

    element.addEventListener(type, handler);

    let listeners = trackedListeners.get(element);
    if (listeners === undefined) {
        listeners = [];
        trackedListeners.set(element, listeners);
    }
    listeners.push({ type, handler });
}

export function attachListeners(configs: ListenerConfig[], element: HTMLElement): void {
    configs.forEach(function (config) {
        attachListener(config.event, element, config.ref);
    });
}

export function detachListeners(element: HTMLElement): void {
    const listeners = trackedListeners.get(element);
    if (listeners === undefined)
        return;

    for (const {type, handler} of listeners) {
        element.removeEventListener(type, handler);
    }

    trackedListeners.delete(element);
}

export const dotNetCallbackMethod = 'OnCallbackEvent';