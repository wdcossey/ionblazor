
export function setValue(element: HTMLIonAccordionGroupElement, value: string | string[]): void {
    element.value = value;
}

export function getValue(element: HTMLIonAccordionGroupElement): string[] {
    const value = element.value;
    return Array.isArray(value) ? value : [value as string];
}
