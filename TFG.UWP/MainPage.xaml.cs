using ConfigAdapter.Xml;
using DotNet.Misc.Extensions.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
using TFG.Core;
using TFG.Core.Model;
using TFG.Core.Model.Criteria;
using TFG.UWP.Dialogs.Assistant;
using Windows.ApplicationModel.Core;
using Windows.Devices.Geolocation;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Maps;
using Windows.UI.Xaml.Input;

// La plantilla de elemento Página en blanco está documentada en https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0xc0a

namespace TFG.UWP
{
    /// <summary>
    /// Página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();

            UpdateMapIcons();
#pragma warning disable IDE0067
            new Timer(_ =>
            {
                _ = CoreApplication.MainView.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal,
                () => this.UpdateMapIcons());
            }, null, 1500, 30000);
#pragma warning restore IDE0067

            this.MapControl1.Center = new Geopoint(new BasicGeoposition
            {
                Latitude = 40.42,
                Longitude = -3.70
            });
            this.MapControl1.MapServiceToken = "eWoeELiV1WcFmNuR4kou~qCQZAXCYRqacXKP2uaAbWg~AugO7XLtIKL-_0p4H7oxambFdWNbiL-Vnfotj3IEIqIJUFgjuDbugknIlRs-842f";
        }

        private void UpdateMapIcons()
        {
            // Buscar todos los países en los que hay sensores
            var paises = SessionStorage.GetSensores()
                .Select(sensor => sensor.Pais)
                .Distinct()
                .ToArray();

            var elements = new List<MapElement>();
            Coordinates.Country.Where(kvp => paises.Contains(kvp.Key))
                .ForEach(kvp =>
                {
                    elements.Add(new MapIcon
                    {
                        Location = new Geopoint(new BasicGeoposition
                        {
                            Latitude = kvp.Value.latitud,
                            Longitude = kvp.Value.longitud
                        }),
                        ZIndex = 0,
                        Title = kvp.Key,
                        Image = RandomAccessStreamReference.CreateFromUri(Estado.SensoresEnPais(SessionStorage.GetSensores(), kvp.Key))
                    });
                });
            this.MapControl1.Layers.Clear();
            this.MapControl1.Layers.Add(new MapElementsLayer
            {
                ZIndex = 1,
                MapElements = elements
            });
        }

        // Hacer click sobre un marcador en el mapa
        private void MapControl1_MapElementClick(MapControl sender, MapElementClickEventArgs args)
        {
            var clickedIcon = args.MapElements.FirstOrDefault(x => x is MapIcon) as MapIcon;
            var country = clickedIcon.Title;

            // Ver solo los sensores de ese país
            this.Frame.Navigate(typeof(VistaListado), new Visualization
            {
                TipoSensor = new AllEncompasingCriteria(),
                Localizacion = new AllEncompasingCriteria(),
                Operaciones = new AllEncompasingCriteria(),
                Pais = new PredicateCriteria
                {
                    Evaluate = p => p.Equals(country),
                    StringValue = country,
                    Verbose = $"de {country}"
                },
                Ordenacion = Ordenacion.TipoSensor
            });
        }

        // Hacer click sobre el botón 'Nuevo sensor'
        private void Button_Click(object sender, RoutedEventArgs e) => this.Frame.Navigate(typeof(NuevoSensor));

        // Hacer click sobre el botón 'Ajustes'
        private void Button_Click_1(object sender, RoutedEventArgs e) => this.Frame.Navigate(typeof(Ajustes));

        // Mostrar todos los sensores
        private void Button_Click_2(object sender, RoutedEventArgs e) => this.Frame.Navigate(typeof(VistaListado), Visualization.Default);

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
