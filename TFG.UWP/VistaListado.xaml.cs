using ConfigAdapter.Xml;
using DotNet.Misc.Extensions.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using TFG.Core.Model;
using TFG.UWP.Dialogs;
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
    public sealed partial class VistaListado : Page
    {
        private Visualization Filters;
        private IList<Sensor> sensores = new List<Sensor>();

        public VistaListado()
        {
            this.InitializeComponent();

            var directory = ApplicationData.Current.LocalFolder.Path;
            var config = XmlConfig.From(Path.Combine(directory, "Settings.xml"));

            var ids = config.Read("ActiveSensors");
            ids.Split('|', StringSplitOptions.RemoveEmptyEntries).ForEach(id =>
            {
                sensores.Add(new Sensor
                {
                    Nombre = config.Read($"{id}:Name"),
                    IP = config.Read($"{id}:IP"),
                    Puerto = config.Read($"{id}:Port"),
                    Pais = config.Read($"{id}:Country"),
                    Tipo = config.Read($"{id}:Type"),
                    Lugar = config.Read($"{id}:Location"),
                    Operaciones = config.Read($"{id}:Operations"),
                    InternalID = id
                });
            });
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            Filters = e.Parameter as Visualization;
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
                // TODO Mostrar notificación o mensaje de aviso
                return;
            }

            var sensor = ListaSensores.SelectedItem as Sensor;
            Frame.Navigate(typeof(DetallesSensor), sensor);
        }
    }
}
