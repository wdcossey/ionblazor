
export function isDateEnabled(element: HTMLIonDatetimeElement, script: string): void {
    // eslint-disable-next-line no-eval
    element.isDateEnabled = eval(script) as (dateIsoString: string) => boolean;
}

export function setValue(element: HTMLIonDatetimeElement, value: string | string[]): void {
    element.value = value;
}

export function cancel(element: HTMLIonDatetimeElement, closeOverlay?: boolean): void {
    element.cancel(closeOverlay);
}

export function confirm(element: HTMLIonDatetimeElement, closeOverlay?: boolean): void {
    element.confirm(closeOverlay);
}

export function reset(element: HTMLIonDatetimeElement, startDate?: string): void {
    element.reset(startDate);
}
