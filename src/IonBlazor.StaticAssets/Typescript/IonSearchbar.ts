import type { DotNetObjectReference } from './common.js';

export async function getInputElement(element: HTMLIonSearchbarElement, callback: DotNetObjectReference): Promise<unknown> {
    const result = await element.getInputElement();
    return callback.invokeMethodAsync('OnCallbackEvent', result);
}

export function setFocus(element: HTMLIonSearchbarElement): Promise<void> {
    return element.setFocus();
}

export function setValue(element: HTMLIonSearchbarElement, value: string): string {
    return (element.value = value);
}
