
export function dismiss(element: HTMLIonPopoverElement, data?: unknown, role?: string, dismissParentPopover?: boolean): Promise<boolean> {
    return element.dismiss(data, role, dismissParentPopover);
}

export function present(element: HTMLIonPopoverElement, event?: Event): Promise<void> {
    return element.present(event as PointerEvent | MouseEvent | TouchEvent | CustomEvent | undefined);
}

export function setIsOpen(element: HTMLIonPopoverElement, value: boolean): void {
    element.isOpen = value;
}
