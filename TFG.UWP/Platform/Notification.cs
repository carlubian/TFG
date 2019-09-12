using Microsoft.QueryStringDotNET;
using Microsoft.Toolkit.Uwp.Notifications;
using System;
using TFG.Core.Model;
using Windows.UI.Notifications;

namespace TFG.UWP.Platform
{
    /// <summary>
    /// Clase que abstrae la emisión de notificaciones
    /// UWP para los cambios de estado de sensores.
    /// </summary>
    internal static class Notification
    {
        internal static void Show(Sensor sensor, SensorStatus status)
        {
            var title = "Un sensor tiene problemas";
            var content = $"El sensor {sensor.Nombre} ha pasado a estado {status.ToString()}";

            var visual = new ToastVisual()
            {
                BindingGeneric = new ToastBindingGeneric()
                {
                    Children =
                    {
                        new AdaptiveText()
                        {
                            Text = title
                        },
                        new AdaptiveText()
                        {
                            Text = content
                        }
                    }
                }
            };

            var actions = new ToastActionsCustom()
            {
                Buttons =
                {
                    new ToastButton("Ver detalles", new QueryString()
                    {
                        { "action", "viewSensor" },
                        { "sensorId", sensor.InternalID }

                    }.ToString())
                }
            };

            var toastContent = new ToastContent()
            {
                Visual = visual,
                Actions = actions,

                Launch = new QueryString()
                {
                    { "action", "viewSensor" },
                    { "sensorId", sensor.InternalID }

                }.ToString()
            };

            var toast = new ToastNotification(toastContent.GetXml())
            {
                ExpirationTime = DateTime.Now.AddHours(1)
            };
            ToastNotificationManager.CreateToastNotifier().Show(toast);
        }
    }
}
