
export function open(element: HTMLIonSelectElement, target?: HTMLElement): void {
    if (target) {
        const event = new MouseEvent('click', {
            bubbles: true,
            cancelable: true,
            view: window,
            relatedTarget: target,
        });

        element.open(event);

        return;
    }

    element.open();
}

export function setValue(element: HTMLIonSelectElement, value: any[]) {
    element.value = value;
}