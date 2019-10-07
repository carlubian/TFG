using ConfigAdapter.Xml;
using System;
using System.IO;
using TFG.Core;
using TFG.Core.Model;
using TFG.UWP.Dialogs;
using TFG.UWP.Dialogs.Assistant;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Navigation;

// La plantilla de elemento Página en blanco está documentada en https://go.microsoft.com/fwlink/?LinkId=234238

namespace TFG.UWP
{
    /// <summary>
    /// Una página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
    /// </summary>
    public sealed partial class EditarSensor : Page
    {
        private Sensor sensor;

        public EditarSensor()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            this.sensor = e.Parameter as Sensor;

            this.FieldName.Text = this.sensor.Nombre;
            this.FieldIP.Text = this.sensor.IP;
            this.FieldPort.Text = this.sensor.Puerto;
            this.FieldCity.Text = this.sensor.Ciudad;
            this.FieldType.ItemsSource = ValoresCriterio.TipoSensor;
            this.FieldType.SelectedItem = this.sensor.Tipo;
            this.FieldCountry.ItemsSource = ValoresCriterio.Pais;
            this.FieldCountry.SelectedItem = this.sensor.Pais;
            this.FieldLocation.ItemsSource = ValoresCriterio.Localizacion;
            this.FieldLocation.SelectedItem = this.sensor.Lugar;
            this.FieldOps.ItemsSource = ValoresCriterio.Operaciones;
            this.FieldOps.SelectedItem = this.sensor.Operaciones;
            this.FieldComments.Text = this.sensor.Comentarios;
        }

        // Cancelar modificaciones
        private void Button_Click(object sender, RoutedEventArgs e) => Navigation.GoBack(this);

        // Guardar modificaciones
        private async void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (!Validate.IPAddress(this.FieldIP.Text))
            {
                await new ErrorValidacion("Dirección IP").ShowAsync();
                return;
            }
            if (!Validate.Port(this.FieldPort.Text))
            {
                await new ErrorValidacion("Puerto").ShowAsync();
                return;
            }

            var directory = ApplicationData.Current.LocalFolder.Path;
            var config = XmlConfig.From(Path.Combine(directory, "Settings.xml"));
            var thisID = this.sensor.InternalID;

            config.Write($"{thisID}:Name", this.FieldName.Text);
            config.Write($"{thisID}:IP", this.FieldIP.Text);
            config.Write($"{thisID}:Port", this.FieldPort.Text);
            config.Write($"{thisID}:City", this.FieldCity.Text);
            config.Write($"{thisID}:Type", this.FieldType.SelectedItem as string);
            config.Write($"{thisID}:Country", this.FieldCountry.SelectedItem as string);
            config.Write($"{thisID}:Location", this.FieldLocation.SelectedItem as string);
            config.Write($"{thisID}:Operations", this.FieldOps.SelectedItem as string);
            config.Write($"{thisID}:Comments", this.FieldComments.Text);

            this.sensor.Nombre = this.FieldName.Text;
            this.sensor.IP = this.FieldIP.Text;
            this.sensor.Puerto = this.FieldPort.Text;
            this.sensor.Ciudad = this.FieldCity.Text;
            this.sensor.Tipo = this.FieldType.SelectedItem as string;
            this.sensor.Pais = this.FieldCountry.SelectedItem as string;
            this.sensor.Lugar = this.FieldLocation.SelectedItem as string;
            this.sensor.Operaciones = this.FieldOps.SelectedItem as string;
            this.sensor.Comentarios = this.FieldComments.Text;

            this.Frame.GoBack();
        }

        // Abrir el sistema de asistencia
        private void Button_Click_2(object sender, RoutedEventArgs e) => _ = new InicioAyuda().ShowAsync();

        // F1 también abre el sistema de asistencia
        private void Grid_KeyUp(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key is Windows.System.VirtualKey.F1
                || e.Key is Windows.System.VirtualKey.NumberPad0)
                this.Button_Click_2(this, null);
        }
    }
}
