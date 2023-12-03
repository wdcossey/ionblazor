export async function presentToast(message, position, duration = 1500, icon = null) {
    const alert = document.createElement('ion-toast');
    alert.duration = duration;
    alert.message = message;
    alert.buttons = ['OK'];
    alert.position = position;
    alert.icon = icon

    document.body.appendChild(alert);
    await alert.present();
}