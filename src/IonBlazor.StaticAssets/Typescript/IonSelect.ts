<<<<<<<< HEAD:src/IonBlazor.StaticAssets/wwwroot/IonSelect.js
export function open(element, target) {
========

export function open(element: HTMLIonSelectElement, target?: HTMLElement): void {
>>>>>>>> claude/update-unit-tests-G9BRc:src/IonBlazor.StaticAssets/Typescript/IonSelect.ts
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