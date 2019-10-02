using System;
using System.Threading;
using TFG.Core.Model;
using TFG.UWP.Dialogs;
using TFG.UWP.Dialogs.Assistant;
using Windows.ApplicationModel.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
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
            this.Filters = e.Parameter as Visualization;
            this.LabelCriteria.Text = this.Filters.ToString();
            this.ListaSensores.ItemsSource = this.Filters.Apply(SessionStorage.GetSensores());

#pragma warning disable IDE0067 // Desechar (Dispose) objetos antes de perder el ámbito
            new Timer(_ => this.UpdateList(), null, 0, 30000);
#pragma warning restore IDE0067 // Desechar (Dispose) objetos antes de perder el ámbito
        }

        private void UpdateList() => _ = CoreApplication.MainView.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal,
                () => this.ListaSensores.ItemsSource = this.Filters.Apply(SessionStorage.GetSensores()));

        // Volver atrás
        private void Button_Click(object sender, RoutedEventArgs e) => Navigation.GoBack(this);

        // Mostrar diálogo de filtros
        private async void Button_Click_2(object sender, RoutedEventArgs e)
        {
            var dialog = new FiltrarListado(this.Filters);
            await dialog.ShowAsync();

            this.Filters = dialog.Filters;
            this.LabelCriteria.Text = this.Filters.ToString();

            this.ListaSensores.ItemsSource = this.Filters.Apply(SessionStorage.GetSensores());
        }

        // Ahora se ven detalles pulsando directamente sobre la lista
        private void ListaSensores_ItemClick(object sender, ItemClickEventArgs e)
        {
            var sensor = e.ClickedItem as Sensor;
            this.Frame.Navigate(typeof(DetallesSensor), sensor);
        }

        // Abrir el sistema de asistencia
        private void Button_Click_4(object sender, RoutedEventArgs e) => _ = new InicioAyuda().ShowAsync();

        // F1 también abre el sistema de asistencia
        private void Grid_KeyUp(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key is Windows.System.VirtualKey.F1
                || e.Key is Windows.System.VirtualKey.NumberPad0)
                this.Button_Click_4(this, null);
        }
    }
}
