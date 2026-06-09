// Blazor JavaScript initializer for IonBlazor.
//
// `ion-modal`'s `breakpoints` (number[]) and `initialBreakpoint` (number) are JS object/number
// properties, not HTML attributes — `breakpoints` has no observed attribute, so it can only be
// set via the element property. Ionic computes the internal *sorted* breakpoints array exactly
// once, in the component's `componentDidLoad`; it is not reactive. Blazor's JS interop from
// `OnAfterRenderAsync` runs after that one-shot computation (and, in Server/Hybrid, asynchronously
// over an IPC channel), so assigning the property from .NET is always too late and the sheet drag
// gesture then reduces over an empty array ("Reduce of empty array with no initial value").
//
// `IonModal` instead renders the values as plain data attributes (which Blazor *can* emit at
// element-creation time). This initializer — loaded before the app boots — copies them onto the
// real JS properties the instant the element appears, which is a microtask and therefore runs
// ahead of Stencil's `requestAnimationFrame`-scheduled `componentDidLoad`. That ordering is
// guaranteed by spec (microtasks drain before the next animation frame) across every engine
// (WebView2, WKWebView, Android System WebView, and desktop browsers), so it holds for Blazor
// WebAssembly, Server, and MAUI Hybrid alike.

type SheetModalElement = Element & {
    breakpoints?: number[];
    initialBreakpoint?: number;
};

const BREAKPOINTS_ATTR = 'data-ibz-breakpoints';
const INITIAL_BREAKPOINT_ATTR = 'data-ibz-initial-breakpoint';

function applyBreakpoints(element: SheetModalElement): void {
    const raw = element.getAttribute(BREAKPOINTS_ATTR);
    if (raw === null) {
        return;
    }

    try {
        element.breakpoints = JSON.parse(raw) as number[];

        const initial = element.getAttribute(INITIAL_BREAKPOINT_ATTR);
        if (initial !== null && initial !== '') {
            element.initialBreakpoint = parseFloat(initial);
        }
    } catch (error) {
        console.error('IonBlazor: failed to apply ion-modal sheet breakpoints', raw, error);
    }

    element.removeAttribute(BREAKPOINTS_ATTR);
    element.removeAttribute(INITIAL_BREAKPOINT_ATTR);
}

let installed = false;

function install(): void {
    if (installed || typeof document === 'undefined') {
        return;
    }
    installed = true;

    // Modals already present in the document (prerendered / static SSR markup).
    document.querySelectorAll<SheetModalElement>(`ion-modal[${BREAKPOINTS_ATTR}]`).forEach(applyBreakpoints);

    // Modals added later by interactive rendering (WebAssembly / Server / Hybrid).
    new MutationObserver(mutations => {
        for (const mutation of mutations) {
            mutation.addedNodes.forEach(node => {
                if (node.nodeType !== Node.ELEMENT_NODE) {
                    return;
                }

                const element = node as Element;
                if (element.tagName === 'ION-MODAL') {
                    applyBreakpoints(element as SheetModalElement);
                } else {
                    element
                        .querySelectorAll<SheetModalElement>(`ion-modal[${BREAKPOINTS_ATTR}]`)
                        .forEach(applyBreakpoints);
                }
            });
        }
    }).observe(document.documentElement, { childList: true, subtree: true });
}

// Blazor WebAssembly (standalone) and Blazor Hybrid (MAUI WebView) call beforeStart/afterStarted.
export function beforeStart(): void {
    install();
}

export function afterStarted(): void {
    install();
}

// Blazor Web Apps (Server / WebAssembly / Auto) call the *Web* variants instead.
export function beforeWebStart(): void {
    install();
}

export function afterWebStarted(): void {
    install();
}
