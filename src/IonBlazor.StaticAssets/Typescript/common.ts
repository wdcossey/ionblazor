export interface DotNetObjectReference {
    invokeMethodAsync(method: string, ...args: unknown[]): Promise<unknown>;
}

interface ListenerConfig {
    event: string;
    ref: DotNetObjectReference;
}

export function attachListener(type: string, element: HTMLElement, ref: DotNetObjectReference): void {
    element.addEventListener(type, (ev: Event) => {
        const customEv = ev as CustomEvent;
        ref.invokeMethodAsync(dotNetCallbackMethod, { tagName: (ev.target as HTMLElement).tagName, detail: customEv.detail });
    });
}

export function attachListeners(configs: ListenerConfig[], element: HTMLElement): void {
    configs.forEach(function (config) {
        attachListener(config.event, element, config.ref);
    });
}

export const dotNetCallbackMethod = 'OnCallbackEvent';
