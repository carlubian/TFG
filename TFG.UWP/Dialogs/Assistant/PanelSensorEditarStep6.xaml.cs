using ConfigAdapter.Xml;
using System;
using System.IO;
using TFG.Core.Model;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

// La plantilla de elemento del cuadro de diálogo de contenido está documentada en https://go.microsoft.com/fwlink/?LinkId=234238

namespace TFG.UWP.Dialogs.Assistant
{
    public sealed partial class PanelSensorEditarStep6 : ContentDialog
    {
        private Sensor sensor;

        public PanelSensorEditarStep6(Sensor sensor)
        {
            this.InitializeComponent();
            this.sensor = sensor;
            this.FieldNombre.Text = sensor.Nombre;
        }

        // Botón de 'Volver atrás' (Click izquierdo)
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            _ = new PanelSensorEditarStep5(sensor).ShowAsync();
        }

        // Botón de 'Volver atrás' (Click derecho)
        private void Button_RightTapped(object sender, RightTappedRoutedEventArgs e) => this.Hide();

        // Botón de 'Posición 2' (Finalizar)
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            sensor.Nombre = this.FieldNombre.Text;

            this.Hide();

            var directory = ApplicationData.Current.LocalFolder.Path;
            var config = XmlConfig.From(Path.Combine(directory, "Settings.xml"));
            var thisID = sensor.InternalID;

            config.Write($"{thisID}:Name", sensor.Nombre);
            config.Write($"{thisID}:IP", sensor.IP);
            config.Write($"{thisID}:Port", sensor.Puerto);
            config.Write($"{thisID}:Type", sensor.Tipo);
            config.Write($"{thisID}:Country", sensor.Pais);
            config.Write($"{thisID}:Location", sensor.Lugar);
            config.Write($"{thisID}:Operations", sensor.Operaciones);

            (Window.Current.Content as Frame)?.Navigate(typeof(DetallesSensor), sensor);
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
