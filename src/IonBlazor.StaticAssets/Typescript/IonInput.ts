import { dotNetCallbackMethod } from './common.js';
import type { DotNetObjectReference } from './common.js';

export function counterFormatter(element: HTMLIonInputElement, expression: string | null): void {
    if (!expression || expression.length === 0) {
        element.counterFormatter = undefined;
        return;
    }

    // eslint-disable-next-line no-eval
    element.counterFormatter = eval(expression) as (inputLength: number, maxLength: number) => string;
}

export function setValue(element: HTMLIonInputElement, value: string | number | null): string | number | null | undefined {
    element.value = value;
    return element.value;
}

export async function getInputElement(element: HTMLIonInputElement, callback: DotNetObjectReference): Promise<unknown> {
    const result = await element.getInputElement();
    return callback.invokeMethodAsync(dotNetCallbackMethod, { result });
}

export function setFocus(element: HTMLIonInputElement): Promise<void> {
    return element.setFocus();
}

export function markTouched(element: HTMLIonInputElement | null): void {
    if (element == null) return;
    element.classList.add('ion-touched');
}

export function markUnTouched(element: HTMLIonInputElement | null): void {
    if (element == null) return;
    element.classList.remove('ion-touched');
}

export function markInvalid(element: HTMLIonInputElement | null): void {
    if (element == null) return;
    removeMarking(element);
    element.classList.add('ion-invalid');
}

export function markValid(element: HTMLIonInputElement | null): void {
    if (element == null) return;
    removeMarking(element);
    element.classList.add('ion-valid');
}

export function removeMarking(element: HTMLIonInputElement | null): void {
    if (element == null) return;
    element.classList.remove('ion-invalid');
    element.classList.remove('ion-valid');
}
