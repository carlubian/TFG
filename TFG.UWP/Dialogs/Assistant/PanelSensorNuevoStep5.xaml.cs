using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

// La plantilla de elemento del cuadro de diálogo de contenido está documentada en https://go.microsoft.com/fwlink/?LinkId=234238

namespace TFG.UWP.Dialogs.Assistant
{
    public sealed partial class PanelSensorNuevoStep5 : ContentDialog
    {
        public PanelSensorNuevoStep5()
        {
            this.InitializeComponent();
        }

        // Botón de 'Volver atrás' (Click izquierdo)
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            _ = new PanelSensorNuevoStep4().ShowAsync();
        }

        // Botón de 'Volver atrás' (Click derecho)
        private void Button_RightTapped(object sender, RightTappedRoutedEventArgs e) => this.Hide();

        // Botón de 'Posición 1' (Pruebas)
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            SessionStorage.SensorBeingBuilt = SessionStorage.SensorBeingBuilt
                .WithOps("Pruebas");
            this.Hide();
            _ = new PanelSensorNuevoStep6().ShowAsync();
        }

        // Botón de 'Posición 2' (Producción)
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            SessionStorage.SensorBeingBuilt = SessionStorage.SensorBeingBuilt
                .WithOps("Produccion");
            this.Hide();
            _ = new PanelSensorNuevoStep6().ShowAsync();
        }

        // Botón de 'Posición 3' (Indefinido)
        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            SessionStorage.SensorBeingBuilt = SessionStorage.SensorBeingBuilt
                .WithOps("Indefinido");
            this.Hide();
            _ = new PanelSensorNuevoStep6().ShowAsync();
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
                case Windows.System.VirtualKey.NumberPad7:
                    this.Button_Click_1(this, null);
                    break;
                case Windows.System.VirtualKey.NumberPad8:
                    this.Button_Click_2(this, null);
                    break;
                case Windows.System.VirtualKey.NumberPad9:
                    this.Button_Click_3(this, null);
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
