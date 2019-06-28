using ConfigAdapter.Xml;
using DotNet.Misc.Extensions.Linq;
using System;
using System.IO;
using System.Linq;
using TFG.Core.Model;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

// La plantilla de elemento del cuadro de diálogo de contenido está documentada en https://go.microsoft.com/fwlink/?LinkId=234238

namespace TFG.UWP.Dialogs.Assistant
{
    public sealed partial class PanelSensorEliminar : ContentDialog
    {
        public PanelSensorEliminar()
        {
            this.InitializeComponent();
            this.ComboSensores.ItemsSource = SessionStorage.GetSensores();
            this.ComboSensores.SelectedIndex = 0;
        }

        // Botón de 'Volver atrás' (Click izquierdo)
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            _ = new PanelSensores().ShowAsync();
        }

        // Botón de 'Volver atrás' (Click derecho)
        private void Button_RightTapped(object sender, RightTappedRoutedEventArgs e) => this.Hide();

        // Botón de 'Posición 2' (Eliminar)
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            var sensor = this.ComboSensores.SelectedItem as Sensor;
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
            SessionStorage.RemoveSensor(sensor);

            this.Hide();
            _ = new PanelSensores().ShowAsync();
        }

        // Utilizar el teclado numérico para navegar
        // 7 8 9  - Fila superior
        // 4 5 6  - Fila central (5 para atrás)
        // 1 2 3  - Fila inferior
        //   0    - Para salir
        private void Grid_KeyUp(object sender, KeyRoutedEventArgs e)
        {
            switch (e.Key)
            {
                case Windows.System.VirtualKey.NumberPad8:
                    this.Button_Click_2(this, null);
                    break;
                case Windows.System.VirtualKey.NumberPad5:
                    this.Button_Click(this, null);
                    break;
                case Windows.System.VirtualKey.NumberPad0:
                    this.Button_RightTapped(this, null);
                    break;
            }
        }
    }
}
