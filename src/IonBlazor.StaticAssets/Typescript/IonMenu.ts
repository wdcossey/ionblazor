
export function close(element: HTMLIonMenuElement, animated?: boolean): Promise<boolean> {
    return element?.close(animated) ?? Promise.resolve(false);
}

export function isActive(element: HTMLIonMenuElement): Promise<boolean> {
    return element.isActive();
}

export function isOpen(element: HTMLIonMenuElement): Promise<boolean> {
    return element.isOpen();
}

export function open(element: HTMLIonMenuElement, animated?: boolean): Promise<boolean> {
    return element.open(animated);
}

export function setOpen(element: HTMLIonMenuElement, shouldOpen: boolean, animated?: boolean): Promise<boolean> {
    return element.setOpen(shouldOpen, animated);
}

export function toggle(element: HTMLIonMenuElement, animated?: boolean): Promise<boolean> {
    return element.toggle(animated);
}
