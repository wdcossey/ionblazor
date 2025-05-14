export function open(element, target) {

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
