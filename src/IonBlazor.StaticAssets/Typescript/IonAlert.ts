import { dotNetCallbackMethod } from './common.js';
import type { DotNetObjectReference } from './common.js';

export function addButtons(element: HTMLIonAlertElement, buttons: AlertButton[], callback: DotNetObjectReference): void {
    buttons.forEach(function (button, index) {
        button.handler = () => {
            callback.invokeMethodAsync(dotNetCallbackMethod, { index });
        };
    });
    element.buttons = buttons;
}

export function addInputs(element: HTMLIonAlertElement, inputs: AlertInput[]): void {
    element.inputs = inputs;
}

export function dismiss(element: HTMLIonAlertElement, data?: unknown, role?: string): Promise<boolean> {
    return element.dismiss(data, role);
}

export function present(element: HTMLIonAlertElement): Promise<void> {
    return element.present();
}
