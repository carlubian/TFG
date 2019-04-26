using TFG.Core.Model;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

// La plantilla de elemento del cuadro de diálogo de contenido está documentada en https://go.microsoft.com/fwlink/?LinkId=234238

namespace TFG.UWP.Dialogs.Assistant
{
    public sealed partial class PanelSensorNuevoStep3 : ContentDialog
    {
        public PanelSensorNuevoStep3()
        {
            this.InitializeComponent();
            this.ComboPais.ItemsSource = ValoresCriterio.Pais;

            var sensor = SessionStorage.SensorBeingBuilt;
            if (sensor.Pais is null)
                this.ComboPais.SelectedIndex = 0;
            else
                this.ComboPais.SelectedItem = sensor.Pais;
        }

        // Botón de 'Volver atrás' (Click izquierdo)
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            _ = new PanelSensorNuevoStep2().ShowAsync();
        }

        // Botón de 'Volver atrás' (Click derecho)
        private void Button_RightTapped(object sender, RightTappedRoutedEventArgs e) => this.Hide();

        // Botón de 'Posición 2' (Continuar)
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            SessionStorage.SensorBeingBuilt = SessionStorage.SensorBeingBuilt
                .WithPais(this.ComboPais.SelectedItem.ToString());
            this.Hide();
            _ = new PanelSensorNuevoStep4().ShowAsync();
        }

        // Utilizar el teclado numérico para navegar
        // 7 8 9  - Fila superior
        // 4 5 6  - Fila central (5 para atrás)
        // 1 2 3  - Fila inferior
        //   0    - Para salir
        private void Grid_KeyUp(object sender, KeyRoutedEventArgs e)
        {
            switch (e.Key)
            {
                case Windows.System.VirtualKey.NumberPad8:
                    this.Button_Click_2(this, null);
                    break;
                case Windows.System.VirtualKey.NumberPad5:
                    this.Button_Click(this, null);
                    break;
                case Windows.System.VirtualKey.NumberPad0:
                    this.Button_RightTapped(this, null);
                    break;
            }
        }
    }
}
