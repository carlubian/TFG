using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using TFG.Core.Model;
using TFG.UWP.Dialogs;
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
    public sealed partial class VistaListado : Page
    {
        private Visualization Filters;

        public VistaListado()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            Filters = e.Parameter as Visualization;
            LabelCriteria.Text = Filters.ToString();
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
        }
    }
}
