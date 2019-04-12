using ConfigAdapter.Xml;
using DotNet.Misc.Extensions.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
using TFG.Core.Model;
using TFG.Core.Model.SensorProperties;
using TFG.UWP.Dialogs;
using TFG.UWP.Dialogs.Assistant;
using Windows.ApplicationModel.Core;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// La plantilla de elemento Página en blanco está documentada en https://go.microsoft.com/fwlink/?LinkId=234238

namespace TFG.UWP
{
    /// <summary>
    /// Una página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
    /// </summary>
    public sealed partial class DetallesSensor : Page
    {
        private Sensor sensor;

        public DetallesSensor()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            var sensor = e.Parameter as Sensor;

            FieldCountry.Text = sensor.Pais;
            FieldIP.Text = sensor.IP;
            FieldLocation.Text = sensor.Lugar;
            FieldName.Text = sensor.Nombre;
            FieldOps.Text = sensor.Operaciones;
            FieldPort.Text = sensor.Puerto;
            FieldType.Text = sensor.Tipo;

            GridNumeric.ItemsSource = sensor.NumericProperties;
            ListTextual.ItemsSource = sensor.TextualProperties;
            StatusIcon.Source = new BitmapImage(new Uri($"ms-appx:///Assets/Icons/{sensor.StatusIcon}"));

            ColorBar.Background = sensor.ColorEstado;

            this.sensor = sensor;

            new Timer(_ => UpdateProperties(), null, 0, 30000);
        }

        private void UpdateProperties()
        {
            _ = CoreApplication.MainView.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal,
                () => GridNumeric.ItemsSource = sensor.NumericProperties);
            _ = CoreApplication.MainView.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal,
                () => ListTextual.ItemsSource = sensor.TextualProperties);
            _ = CoreApplication.MainView.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal,
                () => StatusIcon.Source = new BitmapImage(new Uri($"ms-appx:///Assets/Icons/{sensor.StatusIcon}")));
        }

        // Volver atrás
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Frame.GoBack();
        }

        // Editar datos del sensor
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(EditarSensor), sensor);
        }

        // Eliminar sensor
        private async void Button_Click_2(object sender, RoutedEventArgs e)
        {
            // Confirmar eliminación
            var dialog = new ConfirmarEliminacion();
            await dialog.ShowAsync();
            if (dialog.Result is false)
                return;

            var directory = ApplicationData.Current.LocalFolder.Path;
            var config = XmlConfig.From(Path.Combine(directory, "Settings.xml"));
            var sensores = config.Read("ActiveSensors");

            var result = sensores.Split('|', StringSplitOptions.RemoveEmptyEntries)
                .Except(sensor.InternalID.Enumerate());
            var newValue = "";
            if (result.Count() is 0)
                newValue = "";
            else
                newValue = result.Stringify(str => str, "|");

            config.Write("ActiveSensors", newValue);
            config.DeleteSection(sensor.InternalID);

            sensor.Deleted = true;

            Frame.GoBack();
        }

        private void StatusIcon_Tapped(object sender, TappedRoutedEventArgs e)
        {
            var icon = (StatusIcon.Source as BitmapImage).UriSource.ToString();

            if (icon.Contains("Error"))
                Notification.Show("Este sensor está conectado, pero su estado es incorrecto.", 2000);
            else if (icon.Contains("Offline"))
                Notification.Show("No se puede conectar con el servidor remoto de este sensor.", 2000);
            else if (icon.Contains("OK"))
                Notification.Show("Este sensor está conectado y funciona correctamente.", 2000);
        }

        // Abrir el sistema de asistencia
        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            _ = new InicioAyuda().ShowAsync();
        }

        // F1 también abre el sistema de asistencia
        private void Grid_KeyUp(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key is Windows.System.VirtualKey.F1)
                Button_Click_3(this, null);
        }
    }
}
