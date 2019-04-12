using ConfigAdapter.Xml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using TFG.Core;
using TFG.Core.Model;
using TFG.UWP.Dialogs;
using TFG.UWP.Dialogs.Assistant;
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
    public sealed partial class NuevoSensor : Page
    {
        private bool ShowIntro = false;
        private int Step;

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

            Step = 1;
        }

        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (true.Equals(e.Parameter))
            {
                ShowIntro = true;
                await new NuevoSensorNUE1().ShowAsync();
            }

            Step = 1;
        }
        
        // Hacer click sobre el botón 'Cancelar'
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Frame.GoBack();
        }

        // Nuevo botón de 'Siguiente' común a todos los pasos
        private async void Button_Click_4(object sender, RoutedEventArgs e)
        {
            if (Step is 1)
            {
                if (!Validate.IPAddress(FieldIP.Text))
                {
                    await new ErrorValidacion("Dirección IP").ShowAsync();
                    return;
                }
                if (!Validate.Port(FieldPort.Text))
                {
                    await new ErrorValidacion("Puerto").ShowAsync();
                    return;
                }

                Step = 2;

                GridStep1.Visibility = Visibility.Collapsed;
                GridStep2.Visibility = Visibility.Visible;

                if (ShowIntro)
                    await new NuevoSensorNUE2().ShowAsync();

                return;
            }
            if (Step is 2)
            {
                if (!Validate.StringContainsText(FieldName.Text))
                {
                    await new ErrorValidacion("Nombre del sensor").ShowAsync();
                    return;
                }

                Step = 3;
                
                GridStep2.Visibility = Visibility.Collapsed;
                GridStep3.Visibility = Visibility.Visible;

                if (ShowIntro)
                    await new NuevoSensorNUE3().ShowAsync();

                return;
            }
            if (Step is 3)
            {
                Step = 1;
                // TODO Validación
                var directory = ApplicationData.Current.LocalFolder.Path;
                var config = XmlConfig.From(Path.Combine(directory, "Settings.xml"));
                var thisID = DateTime.Now.Ticks;

                var sensores = config.Read("ActiveSensors");
                config.Write("ActiveSensors", $"{sensores}|SN{thisID}");

                config.Write($"SN{thisID}:Name", FieldName.Text);
                config.Write($"SN{thisID}:IP", FieldIP.Text);
                config.Write($"SN{thisID}:Port", FieldPort.Text);
                config.Write($"SN{thisID}:Type", FieldType.SelectedItem as string);
                config.Write($"SN{thisID}:Country", FieldCountry.SelectedItem as string);
                config.Write($"SN{thisID}:Location", FieldLocation.SelectedItem as string);
                config.Write($"SN{thisID}:Operations", FieldOps.SelectedItem as string);

                Frame.Navigate(typeof(MainPage));
            }
        }

        // Abrir el sistema de asistencia
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            _ = new InicioAyuda().ShowAsync();
        }
    }
}
