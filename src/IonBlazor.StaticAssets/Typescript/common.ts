export interface DotNetObjectReference {
    invokeMethodAsync(method: string, ...args: unknown[]): Promise<unknown>;
}

interface ListenerConfig {
    event: string;
    ref: DotNetObjectReference;
}

export function attachListener(type: string, element: HTMLElement, ref: DotNetObjectReference): void {
    element.addEventListener(type, (ev: Event) => {
        ref.invokeMethodAsync(dotNetCallbackMethod, { tagName: (ev.target as HTMLElement).tagName, detail: (ev as CustomEvent).detail })
            .catch(err => console.error(`IonBlazor: ${dotNetCallbackMethod} for "${type}" failed`, err));
    });
}

export function attachListeners(configs: ListenerConfig[], element: HTMLElement): void {
    configs.forEach(function (config) {
        attachListener(config.event, element, config.ref);
    });
}

export const dotNetCallbackMethod = 'OnCallbackEvent';