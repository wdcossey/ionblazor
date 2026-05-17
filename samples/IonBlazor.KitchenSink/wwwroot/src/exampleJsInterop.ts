
namespace JSInteropWithTypeScript {

    class ExampleJsFunctions {
        public showPrompt(message: string): string {
            return prompt(message, 'Type anything here');
        }

        public showIonAlert(message: string) {
            const alert: any = document.createElement('ion-alert');
            alert.header = 'Alert';
            alert.message = 'Title already in the list.';
            alert.buttons = ['OK'];

            document.body.appendChild(alert);
            return alert.present();
        }
    }

    export function Load(): void {
        window['exampleJsFunctions'] = new ExampleJsFunctions();
    }
}

JSInteropWithTypeScript.Load();