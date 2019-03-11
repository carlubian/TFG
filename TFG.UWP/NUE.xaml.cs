using ConfigAdapter.Xml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.Storage.Pickers;
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
    public sealed partial class NUE : Page
    {
        public NUE()
        {
            this.InitializeComponent();

            var directory = ApplicationData.Current.LocalFolder.Path;
            var config = XmlConfig.From(Path.Combine(directory, "Settings.xml"));
            config.Write("ExistingUser", "True", "Skip New User Experience when launching app. Setting value is ignored.");
        }

        // Continuar la introducción y añadir un nuevo sensor
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // true indica que la introducción está activa
            Frame.Navigate(typeof(NuevoSensor), true);
        }

        // Importar un archivo de configuración existente
        private async void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var openPicker = new FileOpenPicker
            {
                SuggestedStartLocation = PickerLocationId.DocumentsLibrary,
                CommitButtonText = "Importar"
            };
            openPicker.FileTypeFilter.Add(".xml");

            var file = await openPicker.PickSingleFileAsync();

            if (file != null)
            {
                await FileIO.WriteLinesAsync(await ApplicationData.Current.LocalFolder.GetFileAsync("Settings.xml"),
                    await FileIO.ReadLinesAsync(file) as IEnumerable<string>);

                var dialog = new ContentDialog
                {
                    Title = "Configuración importada",
                    Content = "Los ajustes anteriores han sido restaurados correectamente. Todo debería estar justo como lo dejaste.",
                    CloseButtonText = "Aceptar"
                };
                await dialog.ShowAsync();

                Frame.Navigate(typeof(MainPage));
            }
        }

        // Saltar la introducción y empezar de cero
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainPage));
        }
    }
}
