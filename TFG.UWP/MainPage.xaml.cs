using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using TFG.Core;
using TFG.Core.Model;
using Windows.Foundation;
using Windows.Foundation.Collections;
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
        public MainPage()
        {
            this.InitializeComponent();

            //TODO Colocar esto en otro sitio. De todas formas es provisional
            //var client = KaomiClient.Connect();
            //client.AttachProcess();
        }

        // Hacer click sobre un marcador en el mapa
        private void MapControl1_MapElementClick(MapControl sender, MapElementClickEventArgs args)
        {
            var clickedIcon = args.MapElements.FirstOrDefault(x => x is MapIcon) as MapIcon;
            var country = clickedIcon.Title;
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
            Frame.Navigate(typeof(VistaListado), Visualization.Default);
        }
    }
}
