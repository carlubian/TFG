using ConfigAdapter.Xml;
using DotNet.Misc.Extensions.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using TFG.Core;
using TFG.Core.Model;
using TFG.Core.Model.Criteria;
using Windows.ApplicationModel.Core;
using Windows.Devices.Geolocation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.System.Threading;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Maps;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// La plantilla de elemento Página en blanco está documentada en https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0xc0a

namespace TFG.UWP
{
    /// <summary>
    /// Página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private IList<Sensor> sensores = new List<Sensor>();

        public MainPage()
        {
            this.InitializeComponent();

            _ = ThreadPool.RunAsync((_) => TryConnect());

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

            // Buscar todos los países en los que hay sensores
            var paises = sensores
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
                        Title = kvp.Key
                    });
                });
            MapControl1.Layers.Add(new MapElementsLayer
            {
                ZIndex = 1,
                MapElements = elements
            });
        }

        private void TryConnect()
        {
            //TODO Colocar esto en otro sitio. De todas formas es provisional
            var client = KaomiClient.Connect("127.0.0.1", 5000);
            if (client.Connected())
                _ = CoreApplication.MainView.CoreWindow.Dispatcher
                    .RunAsync(CoreDispatcherPriority.Normal, () => Notification.Show("Conexión con Kaomi exitosa.", 2000));
            else
                _ = CoreApplication.MainView.CoreWindow.Dispatcher
                    .RunAsync(CoreDispatcherPriority.Normal, () => Notification.Show("Fallo de conexión con Kaomi.", 2000));
        }

        // Hacer click sobre un marcador en el mapa
        private void MapControl1_MapElementClick(MapControl sender, MapElementClickEventArgs args)
        {
            var clickedIcon = args.MapElements.FirstOrDefault(x => x is MapIcon) as MapIcon;
            var country = clickedIcon.Title;

            // Ver solo los sensores de ese país
            Frame.Navigate(typeof(VistaListado), (sensores, criterio: new Visualization
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
            }));
        }

        // Hacer click sobre el botón 'Nuevo sensor'
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(NuevoSensor));
        }

        // Hacer click sobre el botón 'Ajustes'
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Ajustes));
        }

        // Mostrar todos los sensores
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(VistaListado), (sensores, criterio:Visualization.Default));
        }
    }
}
