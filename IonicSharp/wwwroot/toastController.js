export async function presentToast(message, position, duration = 1500, icon = null, positionAnchor = null) {
    const alert = document.createElement('ion-toast');
    alert.duration = duration;
    alert.message = message;
    alert.buttons = ['OK'];
    alert.position = position;
    alert.icon = icon
    alert.positionAnchor = positionAnchor

    document.body.appendChild(alert);
    await alert.present();
}