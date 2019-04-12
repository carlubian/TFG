using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
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
    public sealed partial class InicioAyuda : ContentDialog
    {
        public InicioAyuda()
        {
            this.InitializeComponent();
        }

        // Botón de 'Volver atrás' (Click izquierdo)
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // En este caso, no hay atrás.
            this.Hide();
        }

        // Botón de 'Volver atrás' (Click derecho)
        private void Button_RightTapped(object sender, RightTappedRoutedEventArgs e)
        {
            this.Hide();
        }

        // Botón de 'Posición 1'
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

        }

        // Botón de 'Posición 2'
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {

        }

        // Botón de 'Posición 3'
        private void Button_Click_3(object sender, RoutedEventArgs e)
        {

        }

        // Botón de 'Posición 4'
        private void Button_Click_4(object sender, RoutedEventArgs e)
        {

        }

        // Botón de 'Posición 5'
        private void Button_Click_5(object sender, RoutedEventArgs e)
        {

        }

        // Botón de 'Posición 6'
        private void Button_Click_6(object sender, RoutedEventArgs e)
        {

        }

        // Botón de 'Posición 7'
        private void Button_Click_7(object sender, RoutedEventArgs e)
        {

        }

        // Botón de 'Posición 8'
        private void Button_Click_8(object sender, RoutedEventArgs e)
        {

        }
    }
}
