import { dotNetCallbackMethod } from './common.js';
import type { DotNetObjectReference } from './common.js';

interface LoadingOptions {
    id?: string;
    spinner?: SpinnerTypes;
    message?: string;
    cssClass?: string | string[];
    showBackdrop?: boolean;
    duration?: number;
    translucent?: boolean;
    animated?: boolean;
    backdropDismiss?: boolean;
    mode?: 'ios' | 'md';
    keyboardClose?: boolean;
    htmlAttributes?: Record<string, unknown>;
}

export async function create(
    options: LoadingOptions,
    didDismissHandler: DotNetObjectReference | null,
    didPresentHandler: DotNetObjectReference | null
): Promise<string> {
    const loading = document.createElement('ion-loading') as HTMLIonLoadingElement;

    if (options.id) {
        loading.id = options.id;
    }

    loading.spinner = options.spinner ?? undefined;
    loading.message = options.message;
    loading.cssClass = options.cssClass ?? undefined;
    if (options.showBackdrop !== undefined) loading.showBackdrop = options.showBackdrop;
    if (options.duration !== undefined) loading.duration = options.duration;
    if (options.translucent !== undefined) loading.translucent = options.translucent;
    if (options.animated !== undefined) loading.animated = options.animated;
    if (options.backdropDismiss !== undefined) loading.backdropDismiss = options.backdropDismiss;
    loading.mode = options.mode ?? undefined;
    if (options.keyboardClose !== undefined) loading.keyboardClose = options.keyboardClose;
    loading.htmlAttributes = options.htmlAttributes ?? undefined;

    document.body.appendChild(loading);

    if (didDismissHandler) {
        loading.addEventListener('didDismiss', (ev: Event) => {
            didDismissHandler.invokeMethodAsync(dotNetCallbackMethod, {
                tagName: (ev.target as HTMLElement).tagName,
                detail: (ev as CustomEvent).detail,
                htmlAttributes: (ev.target as HTMLIonLoadingElement).htmlAttributes,
                id: (ev.target as HTMLIonLoadingElement).id,
            });
        });
    }

    if (didPresentHandler) {
        loading.addEventListener('didPresent', (ev: Event) => {
            didPresentHandler.invokeMethodAsync(dotNetCallbackMethod, {
                tagName: (ev.target as HTMLElement).tagName,
                detail: (ev as CustomEvent).detail,
                htmlAttributes: (ev.target as HTMLIonLoadingElement).htmlAttributes,
                id: (ev.target as HTMLIonLoadingElement).id,
            });
        });
    }

    return options.id ?? loading.id;
}

export async function present(id: string): Promise<void> {
    const loading = getElement(id);
    if (loading) {
        await loading.present();
    }
}

export async function presentWithMessage(id: string, message: string): Promise<void> {
    const loading = getElement(id);
    if (loading) {
        loading.message = message;
        await loading.present();
    }
}

export function updateMessage(id: string, message: string): void {
    const loading = getElement(id);
    if (loading) {
        loading.message = message;
    }
}

export function updateDuration(id: string, duration: number): void {
    const loading = getElement(id);
    if (loading) {
        loading.duration = duration;
    }
}

export function dismiss(id: string, data?: unknown, role?: string): Promise<boolean> | undefined {
    const loading = getElement(id);
    return loading?.dismiss(data, role);
}

export function remove(id: string): void {
    const loading = getElement(id);
    loading?.dismiss();
    loading?.remove();
}

function getElement(id: string): HTMLIonLoadingElement | null {
    return document.querySelector<HTMLIonLoadingElement>(`ion-loading#${id}`);
}
