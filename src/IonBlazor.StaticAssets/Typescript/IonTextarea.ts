import type { DotNetObjectReference } from './common.js';

export function counterFormatter(element: HTMLIonTextareaElement, callback: DotNetObjectReference): void {
    element.counterFormatter = (async (inputLength: number, maxLength: number) => {
        return await callback.invokeMethodAsync('OnCallbackEvent', { inputLength, maxLength }) as string;
    }) as unknown as typeof element.counterFormatter;
}

export function counterFormat(element: HTMLIonTextareaElement, format: string | null): void {
    if (format == null) return;

    // eslint-disable-next-line no-eval
    element.counterFormatter = (inputLength: number, maxLength: number) => {
        return eval(format) as string;
    };
}

export function setValue(element: HTMLIonTextareaElement, value: string | null): string | null | undefined {
    element.value = value;
    return element.value;
}

export function setFocus(element: HTMLIonTextareaElement): Promise<void> {
    return element.setFocus();
}

export function markTouched(element: HTMLIonTextareaElement | null): void {
    if (element == null) return;
    element.classList.add('ion-touched');
}

export function markUnTouched(element: HTMLIonTextareaElement | null): void {
    if (element == null) return;
    element.classList.remove('ion-touched');
}

export function markInvalid(element: HTMLIonTextareaElement | null): void {
    if (element == null) return;
    removeMarking(element);
    element.classList.add('ion-invalid');
}

export function markValid(element: HTMLIonTextareaElement | null): void {
    if (element == null) return;
    removeMarking(element);
    element.classList.add('ion-valid');
}

export function removeMarking(element: HTMLIonTextareaElement | null): void {
    if (element == null) return;
    element.classList.remove('ion-invalid');
    element.classList.remove('ion-valid');
}
