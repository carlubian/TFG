using ConfigAdapter.Xml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using TFG.Core;
using TFG.Core.Model;
using TFG.UWP.Dialogs;
using TFG.UWP.Dialogs.Assistant;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
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
            sensor = e.Parameter as Sensor;

            FieldName.Text = sensor.Nombre;
            FieldIP.Text = sensor.IP;
            FieldPort.Text = sensor.Puerto;

            FieldType.ItemsSource = ValoresCriterio.TipoSensor;
            FieldType.SelectedItem = sensor.Tipo;
            FieldCountry.ItemsSource = ValoresCriterio.Pais;
            FieldCountry.SelectedItem = sensor.Pais;
            FieldLocation.ItemsSource = ValoresCriterio.Localizacion;
            FieldLocation.SelectedItem = sensor.Lugar;
            FieldOps.ItemsSource = ValoresCriterio.Operaciones;
            FieldOps.SelectedItem = sensor.Operaciones;
        }

        // Cancelar modificaciones
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Frame.GoBack();
        }

        // Guardar modificaciones
        private async void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (!Validate.IPAddress(FieldIP.Text))
            {
                await new ErrorValidacion("Dirección IP").ShowAsync();
                return;
            }
            if (!Validate.Port(FieldPort.Text))
            {
                await new ErrorValidacion("Puerto").ShowAsync();
                return;
            }

            var directory = ApplicationData.Current.LocalFolder.Path;
            var config = XmlConfig.From(Path.Combine(directory, "Settings.xml"));
            var thisID = sensor.InternalID;

            config.Write($"{thisID}:Name", FieldName.Text);
            config.Write($"{thisID}:IP", FieldIP.Text);
            config.Write($"{thisID}:Port", FieldPort.Text);
            config.Write($"{thisID}:Type", FieldType.SelectedItem as string);
            config.Write($"{thisID}:Country", FieldCountry.SelectedItem as string);
            config.Write($"{thisID}:Location", FieldLocation.SelectedItem as string);
            config.Write($"{thisID}:Operations", FieldOps.SelectedItem as string);

            sensor.Nombre = FieldName.Text;
            sensor.IP = FieldIP.Text;
            sensor.Puerto = FieldPort.Text;
            sensor.Tipo = FieldType.SelectedItem as string;
            sensor.Pais = FieldCountry.SelectedItem as string;
            sensor.Lugar = FieldLocation.SelectedItem as string;
            sensor.Operaciones = FieldOps.SelectedItem as string;

            Frame.GoBack();
        }

        // Abrir el sistema de asistencia
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            _ = new InicioAyuda().ShowAsync();
        }

        // F1 también abre el sistema de asistencia
        private void Grid_KeyUp(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key is Windows.System.VirtualKey.F1
                || e.Key is Windows.System.VirtualKey.NumberPad0)
                Button_Click_2(this, null);
        }
    }
}
