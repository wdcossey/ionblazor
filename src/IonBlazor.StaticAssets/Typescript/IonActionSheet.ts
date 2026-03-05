import { dotNetCallbackMethod } from './common.js';
import type { DotNetObjectReference } from './common.js';

export function addButtons(element: HTMLIonActionSheetElement, buttons: ActionSheetButton[], callback: DotNetObjectReference): void {
    buttons.forEach(function (button, index) {
        button.handler = () => {
            callback.invokeMethodAsync(dotNetCallbackMethod, { index });
        };
    });
    element.buttons = buttons;
}

export function dismiss(element: HTMLIonActionSheetElement, data?: unknown, role?: string): Promise<boolean> {
    return element.dismiss(data, role);
}

export function present(element: HTMLIonActionSheetElement): Promise<void> {
    return element.present();
}
