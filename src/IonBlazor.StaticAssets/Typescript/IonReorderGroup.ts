
export function complete(element: HTMLIonReorderGroupElement, listOrReorder?: boolean | unknown[]): Promise<unknown> {
    return element.complete(listOrReorder as boolean | undefined);
}
