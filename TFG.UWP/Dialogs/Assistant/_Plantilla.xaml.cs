﻿using System;
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
    public sealed partial class _Plantilla : ContentDialog
    {
        public _Plantilla()
        {
            this.InitializeComponent();
        }

        // Botón de 'Volver atrás' (Click izquierdo)
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
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
                case Windows.System.VirtualKey.NumberPad4:
                    Button_Click_4(this, null);
                    break;
                case Windows.System.VirtualKey.NumberPad5:
                    Button_Click(this, null);
                    break;
                case Windows.System.VirtualKey.NumberPad6:
                    Button_Click_5(this, null);
                    break;
                case Windows.System.VirtualKey.NumberPad1:
                    Button_Click_6(this, null);
                    break;
                case Windows.System.VirtualKey.NumberPad2:
                    Button_Click_7(this, null);
                    break;
                case Windows.System.VirtualKey.NumberPad3:
                    Button_Click_8(this, null);
                    break;
                case Windows.System.VirtualKey.NumberPad0:
                    Button_RightTapped(this, null);
                    break;
            }
        }
    }
}