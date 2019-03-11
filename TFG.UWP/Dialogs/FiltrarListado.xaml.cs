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

// La plantilla de elemento del cuadro de diálogo de contenido está documentada en https://go.microsoft.com/fwlink/?LinkId=234238

namespace TFG.UWP.Dialogs
{
    public sealed partial class FiltrarListado : ContentDialog
    {
        // TODO Ofrecer criterio elegido al llamador
        public object Criteria { get; }

        public FiltrarListado()
        {
            this.InitializeComponent();
            // TODO Recibir criterio actual como parámetro
            Criteria = new object();
        }

        // Aplicar filtros y ordenación
        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            // TODO
            this.Hide();
        }

        // Eliminar filtros y reiniciar ordenación
        private void ContentDialog_SecondaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            // TODO
            // Nota: Este botón no cierra el diálogo para
            // permitir al usuario indicar un nuevo criterio
            // de filtrado desde cero.
        }
    }
}
