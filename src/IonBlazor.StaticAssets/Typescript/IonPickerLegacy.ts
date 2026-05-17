import { dotNetCallbackMethod } from './common.js';
import type { DotNetObjectReference } from './common.js';

export function withColumns(element: HTMLIonPickerLegacyElement, columns: PickerColumn[]): void {
    element.columns = columns;
}

export function withButtons(element: HTMLIonPickerLegacyElement, buttons: PickerButton[], callback: DotNetObjectReference): void {
    buttons.forEach(function (button, index) {
        button.handler = (value: unknown) => {
            callback.invokeMethodAsync(dotNetCallbackMethod, { value, index });
        };
    });
    element.buttons = buttons;
}

export function dismiss(element: HTMLIonPickerLegacyElement, data?: unknown, role?: string): Promise<boolean> {
    return element.dismiss(data, role);
}

export function getColumn(element: HTMLIonPickerLegacyElement, name: string): Promise<PickerColumn | undefined> {
    return element.getColumn(name);
}

export function present(element: HTMLIonPickerLegacyElement): Promise<void> {
    return element.present();
}
