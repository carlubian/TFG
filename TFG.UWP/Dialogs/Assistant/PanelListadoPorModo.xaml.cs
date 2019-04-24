﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using TFG.Core.Model;
using TFG.Core.Model.Criteria;
using Windows.Devices.Input;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// La plantilla de elemento del cuadro de diálogo de contenido está documentada en https://go.microsoft.com/fwlink/?LinkId=234238

namespace TFG.UWP.Dialogs.Assistant
{
    public sealed partial class PanelListadoPorModo : ContentDialog
    {
        public PanelListadoPorModo()
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
        private void Button_RightTapped(object sender, RightTappedRoutedEventArgs e)
        {
            this.Hide();
        }

        // Botón de 'Posición 1' (Indefinido)
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Hide();
            (Window.Current.Content as Frame)?.Navigate(typeof(VistaListado), new Visualization
            {
                Operaciones = new PredicateCriteria
                {
                    Evaluate = str => str.Equals("Indefinido"),
                    Verbose = $"en modo Indefinido",
                    StringValue = "Indefinido"
                },
                TipoSensor = new AllEncompasingCriteria(),
                Localizacion = new AllEncompasingCriteria(),
                Pais = new AllEncompasingCriteria(),
                Ordenacion = Ordenacion.Pais
            });
        }

        // Botón de 'Posición 2' (Pruebas)
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            this.Hide();
            (Window.Current.Content as Frame)?.Navigate(typeof(VistaListado), new Visualization
            {
                Operaciones = new PredicateCriteria
                {
                    Evaluate = str => str.Equals("Pruebas"),
                    Verbose = $"en modo Pruebas",
                    StringValue = "Pruebas"
                },
                TipoSensor = new AllEncompasingCriteria(),
                Localizacion = new AllEncompasingCriteria(),
                Pais = new AllEncompasingCriteria(),
                Ordenacion = Ordenacion.Pais
            });
        }

        // Botón de 'Posición 3' (Producción)
        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            this.Hide();
            (Window.Current.Content as Frame)?.Navigate(typeof(VistaListado), new Visualization
            {
                Operaciones = new PredicateCriteria
                {
                    Evaluate = str => str.Equals("Produccion"),
                    Verbose = $"en modo Producción",
                    StringValue = "Produccion"
                },
                TipoSensor = new AllEncompasingCriteria(),
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
                    Button_Click_1(this, null);
                    break;
                case Windows.System.VirtualKey.NumberPad8:
                    Button_Click_2(this, null);
                    break;
                case Windows.System.VirtualKey.NumberPad9:
                    Button_Click_3(this, null);
                    break;
                case Windows.System.VirtualKey.NumberPad5:
                    Button_Click(this, null);
                    break;
                case Windows.System.VirtualKey.NumberPad0:
                    Button_RightTapped(this, null);
                    break;
            }
        }
    }
}