
export function getSelected(element: HTMLIonTabsElement): Promise<string | undefined> {
    return element.getSelected();
}

export function getTab(element: HTMLIonTabsElement, tab: string | HTMLIonTabElement): Promise<HTMLIonTabElement | undefined> {
    return element.getTab(tab);
}

export function select(element: HTMLIonTabsElement, tab: string | HTMLIonTabElement): Promise<boolean> {
    return element.select(tab);
}
