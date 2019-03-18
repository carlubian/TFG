using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TFG.Core.Model;
using TFG.UWP.Dialogs;
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
    public sealed partial class VistaListado : Page
    {
        private Visualization Filters;
        private IList<Sensor> sensores;

        public VistaListado()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            var tupla = (System.Runtime.CompilerServices.ITuple)e.Parameter;
            Filters = tupla[1] as Visualization;
            sensores = tupla[0] as IList<Sensor>;
            LabelCriteria.Text = Filters.ToString();
            ListaSensores.ItemsSource = Filters.Apply(sensores);
        }
        
        // Volver atrás
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Frame.GoBack();
        }

        // Añadir nuevo sensor
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(NuevoSensor));
        }

        // Mostrar diálogo de filtros
        private async void Button_Click_2(object sender, RoutedEventArgs e)
        {
            var dialog = new FiltrarListado(Filters);
            await dialog.ShowAsync();

            Filters = dialog.Filters;
            LabelCriteria.Text = Filters.ToString();

            ListaSensores.ItemsSource = Filters.Apply(sensores);
        }

        // Ver detalles del sensor seleccionado
        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            if (ListaSensores.SelectedItem is null)
            {
                Notification.Show("Debes seleccionar un sensor antes de poder ver sus detalles.", 2000);
                return;
            }

            var sensor = ListaSensores.SelectedItem as Sensor;
            Frame.Navigate(typeof(DetallesSensor), sensor);
        }
    }
}
