
export function dismiss(element: HTMLIonLoadingElement, data?: unknown, role?: string): Promise<boolean> {
    return element.dismiss(data, role);
}

export async function present(element: HTMLIonLoadingElement): Promise<void> {
    await element.present();
}

export function updateMessage(element: HTMLIonLoadingElement, message: string): void {
    element.message = message;
}
