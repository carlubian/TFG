using ConfigAdapter.Xml;
using DotNet.Misc.Extensions.Linq;
using System;
using System.IO;
using System.Linq;
using System.Threading;
using TFG.Core.Model;
using TFG.UWP.Dialogs;
using TFG.UWP.Dialogs.Assistant;
using Windows.ApplicationModel.Core;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
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

            this.FieldCountry.Text = sensor.Pais;
            this.FieldIP.Text = sensor.IP;
            this.FieldLocation.Text = sensor.Lugar;
            this.FieldName.Text = sensor.Nombre;
            this.FieldOps.Text = sensor.Operaciones;
            this.FieldPort.Text = sensor.Puerto;
            this.FieldType.Text = sensor.Tipo;

            this.GridNumeric.ItemsSource = sensor.NumericProperties;
            this.ListTextual.ItemsSource = sensor.TextualProperties;
            this.StatusIcon.Source = new BitmapImage(new Uri($"ms-appx:///Assets/Icons/{sensor.StatusIcon}"));

            this.ColorBar.Background = sensor.ColorEstado;

            this.sensor = sensor;

            new Timer(_ => this.UpdateProperties(), null, 0, 30000);
        }

        private void UpdateProperties()
        {
            _ = CoreApplication.MainView.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal,
                () => this.GridNumeric.ItemsSource = this.sensor.NumericProperties);
            _ = CoreApplication.MainView.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal,
                () => this.ListTextual.ItemsSource = this.sensor.TextualProperties);
            _ = CoreApplication.MainView.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal,
                () => this.StatusIcon.Source = new BitmapImage(new Uri($"ms-appx:///Assets/Icons/{this.sensor.StatusIcon}")));
            _ = CoreApplication.MainView.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal,
               () => this.ColorBar.Background = sensor.ColorEstado);
        }

        // Volver atrás
        private void Button_Click(object sender, RoutedEventArgs e) => Navigation.GoBack(this);

        // Editar datos del sensor
        private void Button_Click_1(object sender, RoutedEventArgs e) => this.Frame.Navigate(typeof(EditarSensor), this.sensor);

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
                .Except(this.sensor.InternalID.Enumerate());
            var newValue = "";
            if (result.Count() is 0)
                newValue = "";
            else
                newValue = result.Stringify(str => str, "|");

            config.Write("ActiveSensors", newValue);
            config.DeleteSection(this.sensor.InternalID);

            this.sensor.Deleted = true;

            this.Frame.GoBack();
        }

        private void StatusIcon_Tapped(object sender, TappedRoutedEventArgs e)
        {
            var icon = (this.StatusIcon.Source as BitmapImage).UriSource.ToString();

            if (icon.Contains("Error"))
                this.Notification.Show("Este sensor está conectado, pero su estado es incorrecto.", 2000);
            else if (icon.Contains("Offline"))
                this.Notification.Show("No se puede conectar con el servidor remoto de este sensor.", 2000);
            else if (icon.Contains("OK"))
                this.Notification.Show("Este sensor está conectado y funciona correctamente.", 2000);
        }

        // Abrir el sistema de asistencia
        private void Button_Click_3(object sender, RoutedEventArgs e) => _ = new InicioAyuda().ShowAsync();

        // F1 también abre el sistema de asistencia
        private void Grid_KeyUp(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key is Windows.System.VirtualKey.F1
                || e.Key is Windows.System.VirtualKey.NumberPad0)
                this.Button_Click_3(this, null);
        }
    }
}
