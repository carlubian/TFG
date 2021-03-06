﻿using TFG.Core.Model;
using TFG.Core.Model.Criteria;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

// La plantilla de elemento del cuadro de diálogo de contenido está documentada en https://go.microsoft.com/fwlink/?LinkId=234238

namespace TFG.UWP.Dialogs.Assistant
{
    public sealed partial class PanelListadoPorTipo : ContentDialog
    {
        public PanelListadoPorTipo()
        {
            this.InitializeComponent();
        }

        // Botón de 'Volver atrás' (Click izquierdo)
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            _ = new PanelListados().ShowAsync();
        }

        // Botón de 'Volver atrás' (Click derecho)
        private void Button_RightTapped(object sender, RightTappedRoutedEventArgs e) => this.Hide();

        // Botón de 'Posición 1' (Indefinido)
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Hide();
            (Window.Current.Content as Frame)?.Navigate(typeof(VistaListado), new Visualization
            {
                TipoSensor = new PredicateCriteria
                {
                    Evaluate = str => str.Equals("Indefinido"),
                    Verbose = $"de tipo Indefinido",
                    StringValue = "Indefinido"
                },
                Operaciones = new AllEncompasingCriteria(),
                Localizacion = new AllEncompasingCriteria(),
                Pais = new AllEncompasingCriteria(),
                Ordenacion = Ordenacion.Pais
            });
        }

        // Botón de 'Posición 2' (Gocator)
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            this.Hide();
            (Window.Current.Content as Frame)?.Navigate(typeof(VistaListado), new Visualization
            {
                TipoSensor = new PredicateCriteria
                {
                    Evaluate = str => str.Equals("Gocator"),
                    Verbose = $"de tipo Gocator",
                    StringValue = "Gocator"
                },
                Operaciones = new AllEncompasingCriteria(),
                Localizacion = new AllEncompasingCriteria(),
                Pais = new AllEncompasingCriteria(),
                Ordenacion = Ordenacion.Pais
            });
        }

        // Botón de 'Posición 3' (TestGocator)
        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            this.Hide();
            (Window.Current.Content as Frame)?.Navigate(typeof(VistaListado), new Visualization
            {
                TipoSensor = new PredicateCriteria
                {
                    Evaluate = str => str.Equals("TestGocator"),
                    Verbose = $"de tipo TestGocator",
                    StringValue = "TestGocator"
                },
                Operaciones = new AllEncompasingCriteria(),
                Localizacion = new AllEncompasingCriteria(),
                Pais = new AllEncompasingCriteria(),
                Ordenacion = Ordenacion.Pais
            });
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
