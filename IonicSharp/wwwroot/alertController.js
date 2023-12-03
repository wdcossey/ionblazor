export async function presentAlert(header, subHeader, message) {
    const alert = document.createElement('ion-alert');
    alert.header = header;
    alert.subHeader = subHeader;
    alert.message = message;
    alert.buttons = ['Action'];

    document.body.appendChild(alert);
    await alert.present();
}