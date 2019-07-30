using ConfigAdapter.Xml;
using System;
using System.Collections.Generic;
using System.IO;
using TFG.UWP.Dialogs;
using TFG.UWP.Dialogs.Assistant;
using Windows.ApplicationModel.Core;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

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
        private void Button_Click(object sender, RoutedEventArgs e) => Navigation.GoBack(this);

        // Guardar ajustes
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (this.ValidateSettings())
            {
                this.SaveSettings();
                Navigation.GoBack(this);
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
                wait = "60";

            this.FieldAttempts.Text = attempts;
            this.FieldWait.Text = wait;
        }

        private bool ValidateSettings()
        {
            // Validar datos
            var intentos = this.FieldAttempts.Text;
            var delay = this.FieldWait.Text;
            var intentoss = -1;
            if (!int.TryParse(intentos, out intentoss))
            {
                new ErrorValidacion("Intentos de conexión").ShowAsync();
                return false;
            }
            var delayy = -1;
            if (!int.TryParse(delay, out delayy))
            {
                new ErrorValidacion("Intervalo de actualización").ShowAsync();
                return false;
            }
            if (intentoss <= 0 || intentoss > 10)
            {
                new ErrorRango("Intentos de conexión", 1, 10).ShowAsync();
                return false;
            }
            if (delayy < 10 || delayy > 600)
            {
                new ErrorRango("Intervalo de actualización", 10, 600).ShowAsync();
                return false;
            }

            return true;
        }

        private void SaveSettings()
        {
            var directory = ApplicationData.Current.LocalFolder.Path;

            var config = XmlConfig.From(Path.Combine(directory, "Settings.xml"));
            config.Write("Global:Attempts", this.FieldAttempts.Text);
            config.Write("Global:Delay", this.FieldWait.Text);
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

                this.PopulateSettings();
            }
        }

        // Abrir el sistema de asistencia
        private void Button_Click_4(object sender, RoutedEventArgs e) => _ = new InicioAyuda().ShowAsync();

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
                this.Button_Click_4(this, null);
        }
    }
}
