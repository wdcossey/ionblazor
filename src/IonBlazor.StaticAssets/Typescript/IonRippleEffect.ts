
export function addRipple(element: HTMLIonRippleEffectElement, x: number, y: number): Promise<() => void> {
    return element.addRipple(x, y);
}
