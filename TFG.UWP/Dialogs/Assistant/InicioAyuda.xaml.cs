using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

// La plantilla de elemento del cuadro de diálogo de contenido está documentada en https://go.microsoft.com/fwlink/?LinkId=234238

namespace TFG.UWP.Dialogs.Assistant
{
    public sealed partial class InicioAyuda : ContentDialog
    {
        public InicioAyuda()
        {
            this.InitializeComponent();
            SessionStorage.SensorBeingBuilt = null;
        }

        // Botón de 'Volver atrás' (Click izquierdo)
        private void Button_Click(object sender, RoutedEventArgs e) =>
            // En este caso, no hay atrás.
            this.Hide();

        // Botón de 'Volver atrás' (Click derecho)
        private void Button_RightTapped(object sender, RightTappedRoutedEventArgs e) => this.Hide();

        // Botón de 'Posición 1' (Sensores)
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Hide();
            _ = new PanelSensores().ShowAsync();
        }

        // Botón de 'Posición 3' (Ajustes)
        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            this.Hide();
            (Window.Current.Content as Frame)?.Navigate(typeof(Ajustes), null);
        }

        // Botón de 'Posición 6' (Listados)
        private void Button_Click_6(object sender, RoutedEventArgs e)
        {
            this.Hide();
            _ = new PanelListados().ShowAsync();
        }

        // Botón de 'Posición 8'
        private void Button_Click_8(object sender, RoutedEventArgs e)
        {

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
                case Windows.System.VirtualKey.NumberPad9:
                    this.Button_Click_3(this, null);
                    break;
                case Windows.System.VirtualKey.NumberPad5:
                    this.Button_Click(this, null);
                    break;
                case Windows.System.VirtualKey.NumberPad1:
                    this.Button_Click_6(this, null);
                    break;
                case Windows.System.VirtualKey.NumberPad3:
                    this.Button_Click_8(this, null);
                    break;
                case Windows.System.VirtualKey.NumberPad0:
                    this.Button_RightTapped(this, null);
                    break;
            }
        }
    }
}
