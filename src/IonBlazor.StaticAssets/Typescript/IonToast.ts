import { dotNetCallbackMethod } from './common.js';
import type { DotNetObjectReference } from './common.js';

export function dismiss(element: HTMLIonToastElement): Promise<boolean> {
    return element.dismiss();
}

export function withButtons(element: HTMLIonToastElement, buttons: ToastButton[] | null, callback: DotNetObjectReference): void {
    if (buttons == null) return;

    buttons.forEach(function (button, index) {
        button.handler = () => {
            callback.invokeMethodAsync(dotNetCallbackMethod, { index });
        };
    });

    element.buttons = buttons;
}
