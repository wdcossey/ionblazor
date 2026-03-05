
export function getScrollElement(_element: HTMLIonContentElement): null {
    return null; // element.getScrollElement();
}

export function scrollByPoint(element: HTMLIonContentElement, x: number, y: number, duration: number): Promise<void> {
    return element.scrollByPoint(x, y, duration);
}

export function scrollToBottom(element: HTMLIonContentElement, duration?: number): Promise<void> {
    return element.scrollToBottom(duration);
}

export function scrollToPoint(element: HTMLIonContentElement, x: number | undefined, y: number, duration?: number): Promise<void> {
    return element.scrollToPoint(x, y, duration);
}

export function scrollToTop(element: HTMLIonContentElement, duration?: number): Promise<void> {
    return element.scrollToTop(duration);
}
