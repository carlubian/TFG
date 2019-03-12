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
    public sealed partial class NuevoSensor : Page
    {
        private bool ShowIntro = false;

        public NuevoSensor()
        {
            this.InitializeComponent();
            GridStep1.Visibility = Visibility.Visible;
            GridStep2.Visibility = Visibility.Collapsed;
            GridStep3.Visibility = Visibility.Collapsed;

            FieldCountry.ItemsSource = ValoresCriterio.Pais;
            FieldCountry.SelectedIndex = 0;
            FieldType.ItemsSource = ValoresCriterio.TipoSensor;
            FieldType.SelectedIndex = 0;
            FieldOps.ItemsSource = ValoresCriterio.Operaciones;
            FieldOps.SelectedIndex = 0;
            FieldLocation.ItemsSource = ValoresCriterio.Localizacion;
            FieldLocation.SelectedIndex = 0;
        }

        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (true.Equals(e.Parameter))
            {
                ShowIntro = true;
                await new NuevoSensorNUE1().ShowAsync();
            }
        }
        
        // Hacer click sobre el botón 'Cancelar'
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Frame.GoBack();
        }

        // Avanzar al paso 2
        private async void Button_Click_1(object sender, RoutedEventArgs e)
        {
            // TODO Validación
            GridStep1.Visibility = Visibility.Collapsed;
            GridStep2.Visibility = Visibility.Visible;

            if (ShowIntro)
                await new NuevoSensorNUE2().ShowAsync();
        }

        // Avanzar al paso 3
        private async void Button_Click_2(object sender, RoutedEventArgs e)
        {
            // TODO Validación
            GridStep2.Visibility = Visibility.Collapsed;
            GridStep3.Visibility = Visibility.Visible;

            if (ShowIntro)
                await new NuevoSensorNUE3().ShowAsync();
        }

        // Completar el proceso
        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            // TODO Validación y añadir sensor
            Frame.Navigate(typeof(MainPage));
        }
    }
}
