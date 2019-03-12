using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using TFG.Core.Model;
using Windows.Foundation;
using Windows.Foundation.Collections;
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
    public sealed partial class DetallesSensor : Page
    {
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
        }
        // Volver atrás
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Frame.GoBack();
        }

        // Editar datos del sensor
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

        }

        // Eliminar sensor
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {

        }
    }
}
