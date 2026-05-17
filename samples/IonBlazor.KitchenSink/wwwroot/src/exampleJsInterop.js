var JSInteropWithTypeScript;
(function (JSInteropWithTypeScript) {
    var ExampleJsFunctions = /** @class */ (function () {
        function ExampleJsFunctions() {
        }
        ExampleJsFunctions.prototype.showPrompt = function (message) {
            return prompt(message, 'Type anything here');
        };
        ExampleJsFunctions.prototype.showIonAlert = function (message) {
            var alert = document.createElement('ion-alert');
            alert.header = 'Alert';
            alert.message = 'Title already in the list.';
            alert.buttons = ['OK'];
            document.body.appendChild(alert);
            return alert.present();
        };
        return ExampleJsFunctions;
    }());
    function Load() {
        window['exampleJsFunctions'] = new ExampleJsFunctions();
    }
    JSInteropWithTypeScript.Load = Load;
})(JSInteropWithTypeScript || (JSInteropWithTypeScript = {}));
JSInteropWithTypeScript.Load();
//# sourceMappingURL=exampleJsInterop.js.map