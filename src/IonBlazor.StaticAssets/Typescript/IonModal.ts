import { dotNetCallbackMethod } from './common.js';
import type { DotNetObjectReference } from './common.js';

export function isOpen(element: HTMLIonModalElement, value: boolean): boolean {
    return (element.isOpen = value);
}

export function dismiss(element: HTMLIonModalElement, data?: unknown, role?: string): Promise<boolean> {
    return element.dismiss(data, role);
}

export function canDismiss(element: HTMLIonModalElement, value: boolean): void {
    element.canDismiss = value;
}

export function canDismissCallback(element: HTMLIonModalElement, callback: DotNetObjectReference): void {
    element.canDismiss = (args: unknown) => callback.invokeMethodAsync(dotNetCallbackMethod, args) as Promise<boolean>;
}

export function getCurrentBreakpoint(element: HTMLIonModalElement): Promise<number | undefined> {
    return element.getCurrentBreakpoint();
}

export function breakpoints(element: HTMLIonModalElement, value: number[]): void {
    element.breakpoints = value;
}

export function initialBreakpoint(element: HTMLIonModalElement, value: number): void {
    element.initialBreakpoint = value;
}

export function present(element: HTMLIonModalElement): Promise<void> {
    return element.present();
}

export async function setCurrentBreakpoint(element: HTMLIonModalElement, value: number): Promise<void> {
    await element.setCurrentBreakpoint(value);
}

export function enterAnimation(element: HTMLIonModalElement, value: string): void {
    // eslint-disable-next-line no-eval
    element.enterAnimation = eval(value) as AnimationBuilder;
}
