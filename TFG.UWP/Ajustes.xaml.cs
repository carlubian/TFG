using ConfigAdapter.Xml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
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
            var directory = Windows.Storage.ApplicationData.Current.LocalFolder.Path;

            var config = XmlConfig.From(Path.Combine(directory, "Settings.xml"));
            var attempts = config.AsTransferable().ReadAll().FirstOrDefault(s => s.Key.Equals("Global:Attempts")).Value;
            var wait = config.AsTransferable().ReadAll().FirstOrDefault(s => s.Key.Equals("Global:Delay")).Value;

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
    }
}
