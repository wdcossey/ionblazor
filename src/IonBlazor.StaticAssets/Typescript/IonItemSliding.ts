
export function close(element: HTMLIonItemSlidingElement): Promise<void> {
    return element.close();
}

export function closeOpened(element: HTMLIonItemSlidingElement): Promise<boolean> {
    return element.closeOpened();
}

export function getOpenAmount(element: HTMLIonItemSlidingElement): Promise<number> {
    return element.getOpenAmount();
}

export function getSlidingRatio(element: HTMLIonItemSlidingElement): Promise<number> {
    return element.getSlidingRatio();
}

export function open(element: HTMLIonItemSlidingElement, side?: 'start' | 'end'): Promise<void> {
    return element.open(side);
}
