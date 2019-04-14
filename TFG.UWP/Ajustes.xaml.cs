using ConfigAdapter.Xml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using TFG.UWP.Dialogs.Assistant;
using Windows.ApplicationModel.Core;
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
    public sealed partial class Ajustes : Page
    {
        public Ajustes()
        {
            this.InitializeComponent();
            this.PopulateSettings();
        }

        // Volver sin guardar
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Frame.GoBack();
        }

        // Guardar ajustes
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (ValidateSettings())
            {
                SaveSettings();
                Frame.GoBack();
            }
        }

        private void PopulateSettings()
        {
            var directory = ApplicationData.Current.LocalFolder.Path;

            var config = XmlConfig.From(Path.Combine(directory, "Settings.xml"));
            var attempts = config.Read("Global:Attempts");
            var wait = config.Read("Global:Delay");

            if (attempts is default(string))
                attempts = "1";
            if (wait is default(string))
                wait = "20";

            FieldAttempts.Text = attempts;
            FieldWait.Text = wait;
        }

        private bool ValidateSettings()
        {
            var attempts = FieldAttempts.Text;
            var wait = FieldWait.Text;

            if (int.TryParse(attempts, out var n1) &&
                int.TryParse(wait, out var n2) &&
                n1 > 0 && n1 < 11 &&
                n2 > 5 && n2 < 180)
                return true;

            return false;
        }

        private void SaveSettings()
        {
            var directory = Windows.Storage.ApplicationData.Current.LocalFolder.Path;

            var config = XmlConfig.From(Path.Combine(directory, "Settings.xml"));
            config.Write("Global:Attempts", FieldAttempts.Text);
            config.Write("Global:Delay", FieldWait.Text);
        }

        // Exportar ajustes
        private async void Button_Click_2(object sender, RoutedEventArgs e)
        {
            var savePicker = new FileSavePicker
            {
                SuggestedStartLocation = PickerLocationId.DocumentsLibrary,
                SuggestedFileName = "Settings",
                CommitButtonText = "Exportar"
            };
            savePicker.FileTypeChoices.Add("Archivo XML", new List<string>() { ".xml" });

            var file = await savePicker.PickSaveFileAsync();

            if (file != null)
            {
                 await FileIO.WriteLinesAsync(file, 
                    await FileIO.ReadLinesAsync(await ApplicationData.Current.LocalFolder.GetFileAsync("Settings.xml")) as IEnumerable<string>);
            }
        }

        // Importar ajustes
        private async void Button_Click_3(object sender, RoutedEventArgs e)
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

                PopulateSettings();
            }
        }

        // Abrir el sistema de asistencia
        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            _ = new InicioAyuda().ShowAsync();
        }

        // Restaurar datos de fábrica
        private async void Button_Click_5(object sender, RoutedEventArgs e)
        {
            // TODO Habría que mostrar un diálogo de confirmación.

            var directory = Windows.Storage.ApplicationData.Current.LocalFolder.Path;

            var config = XmlConfig.From(Path.Combine(directory, "Settings.xml"));

            // Eliminar todos los sensores conectados
            var sensores = config.Read("ActiveSensors")
                                 .Split('|', StringSplitOptions.RemoveEmptyEntries);
            foreach (var sensor in sensores)
                config.DeleteSection(sensor);
            config.Write("ActiveSensors", "");

            // Preparar la NUE para el próximo arranque
            config.DeleteKey("ExistingUser");

            // Reiniciar la aplicación
            await CoreApplication.RequestRestartAsync("");
        }

        // F1 también abre el sistema de asistencia
        private void Grid_KeyUp(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key is Windows.System.VirtualKey.F1
                || e.Key is Windows.System.VirtualKey.NumberPad0)
                Button_Click_4(this, null);
        }
    }
}
