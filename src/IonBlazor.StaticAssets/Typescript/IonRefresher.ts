
export function cancel(element: HTMLIonRefresherElement): void {
    element.cancel();
}

export function complete(element: HTMLIonRefresherElement): Promise<void> {
    return element.complete();
}

export function getProgress(element: HTMLIonRefresherElement): Promise<number> {
    return element.getProgress();
}
