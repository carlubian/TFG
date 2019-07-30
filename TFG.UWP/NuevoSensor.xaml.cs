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
    public sealed partial class NuevoSensor : Page
    {
        private int Step;

        public NuevoSensor()
        {
            this.InitializeComponent();
            this.GridStep1.Visibility = Visibility.Visible;
            this.GridStep2.Visibility = Visibility.Collapsed;
            this.GridStep3.Visibility = Visibility.Collapsed;

            this.FieldCountry.ItemsSource = ValoresCriterio.Pais;
            this.FieldCountry.SelectedIndex = 0;
            this.FieldType.ItemsSource = ValoresCriterio.TipoSensor;
            this.FieldType.SelectedIndex = 0;
            this.FieldOps.ItemsSource = ValoresCriterio.Operaciones;
            this.FieldOps.SelectedIndex = 0;
            this.FieldLocation.ItemsSource = ValoresCriterio.Localizacion;
            this.FieldLocation.SelectedIndex = 0;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e) => this.Step = 1;

        // Hacer click sobre el botón 'Cancelar'
        private void Button_Click(object sender, RoutedEventArgs e) => Navigation.GoBack(this);

        // Nuevo botón de 'Siguiente' común a todos los pasos
        private async void Button_Click_4(object sender, RoutedEventArgs e)
        {
            if (this.Step is 1)
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

                this.Step = 2;

                this.GridStep1.Visibility = Visibility.Collapsed;
                this.GridStep2.Visibility = Visibility.Visible;

                return;
            }
            if (this.Step is 2)
            {
                if (!Validate.StringContainsText(this.FieldName.Text))
                {
                    await new ErrorValidacion("Nombre del sensor").ShowAsync();
                    return;
                }

                this.Step = 3;

                this.GridStep2.Visibility = Visibility.Collapsed;
                this.GridStep3.Visibility = Visibility.Visible;

                switch (this.FieldType.SelectedItem as string)
                {
                    case "Indefinido":
                        GridAjustesIndef.Visibility = Visibility.Visible;
                        GridAjustesGocator.Visibility = Visibility.Collapsed;
                        GridAjustesTestGocator.Visibility = Visibility.Collapsed;
                        break;
                    case "Gocator":
                        GridAjustesIndef.Visibility = Visibility.Collapsed;
                        GridAjustesGocator.Visibility = Visibility.Visible;
                        GridAjustesTestGocator.Visibility = Visibility.Collapsed;
                        break;
                    case "TestGocator":
                        GridAjustesIndef.Visibility = Visibility.Collapsed;
                        GridAjustesGocator.Visibility = Visibility.Collapsed;
                        GridAjustesTestGocator.Visibility = Visibility.Visible;
                        break;
                }

                return;
            }
            if (this.Step is 3)
            {
                this.Step = 1;
                // TODO Validación
                var directory = ApplicationData.Current.LocalFolder.Path;
                var config = XmlConfig.From(Path.Combine(directory, "Settings.xml"));
                var thisID = DateTime.Now.Ticks;

                var intentos = config.Read("Global:Attempts");
                var delay = config.Read("Global:Delay");
                var intentoss = -1;
                if (!int.TryParse(intentos, out intentoss))
                    intentoss = 1;
                var delayy = -1;
                if (!int.TryParse(delay, out delayy))
                    delayy = 60;

                var sensores = config.Read("ActiveSensors");
                config.Write("ActiveSensors", $"{sensores}|SN{thisID}");

                config.Write($"SN{thisID}:Name", this.FieldName.Text);
                config.Write($"SN{thisID}:IP", this.FieldIP.Text);
                config.Write($"SN{thisID}:Port", this.FieldPort.Text);
                config.Write($"SN{thisID}:Type", this.FieldType.SelectedItem as string);
                config.Write($"SN{thisID}:Country", this.FieldCountry.SelectedItem as string);
                config.Write($"SN{thisID}:Location", this.FieldLocation.SelectedItem as string);
                config.Write($"SN{thisID}:Operations", this.FieldOps.SelectedItem as string);

                var sensor = new Sensor(intentoss, delayy)
                {
                    InternalID = $"SN{thisID}",
                    IP = this.FieldIP.Text,
                    Lugar = this.FieldLocation.SelectedItem as string,
                    Nombre = this.FieldName.Text,
                    Operaciones = this.FieldOps.SelectedItem as string,
                    Pais = this.FieldCountry.SelectedItem as string,
                    Puerto = this.FieldPort.Text,
                    Tipo = this.FieldType.SelectedItem as string
                };
                SessionStorage.AddSensor(sensor);

                this.Frame.Navigate(typeof(MainPage));
            }
        }

        // Abrir el sistema de asistencia
        private void Button_Click_1(object sender, RoutedEventArgs e) => _ = new InicioAyuda().ShowAsync();

        // F1 también abre el sistema de asistencia
        private void Grid_KeyUp(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key is Windows.System.VirtualKey.F1
                || e.Key is Windows.System.VirtualKey.NumberPad0)
                this.Button_Click_1(this, null);
        }
    }
}
