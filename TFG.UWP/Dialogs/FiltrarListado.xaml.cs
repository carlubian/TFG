using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using TFG.Core.Model;
using TFG.Core.Model.Criteria;
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
        public Visualization Filters { get; private set; }

        public FiltrarListado(Visualization Filters)
        {
            this.InitializeComponent();
            this.Filters = Filters;
        }

        // Aplicar filtros y ordenación
        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            if (FieldTipo.Text != "")
                Filters.TipoSensor = new PredicateCriteria
                {
                    Evaluate = str => str.ToUpperInvariant().Equals(FieldTipo.Text),
                    Verbose = $"de tipo {FieldTipo.Text}"
                };
            else
                Filters.TipoSensor = new AllEncompasingCriteria();
            if (FieldPais.Text != "")
                Filters.Pais = new PredicateCriteria
                {
                    Evaluate = str => str.ToUpperInvariant().Equals(FieldPais.Text),
                    Verbose = $"de {FieldPais.Text}"
                };
            else
                Filters.Pais = new AllEncompasingCriteria();
            if (FieldLugar.Text != "")
                Filters.Localizacion = new PredicateCriteria
                {
                    Evaluate = str => str.ToUpperInvariant().Equals(FieldLugar.Text),
                    Verbose = $"en {FieldLugar.Text}"
                };
            else
                Filters.Localizacion = new AllEncompasingCriteria();
            if (FieldOps.Text != "")
                Filters.Operaciones = new PredicateCriteria
                {
                    Evaluate = str => str.ToUpperInvariant().Equals(FieldOps.Text),
                    Verbose = $"en modo {FieldOps.Text}"
                };
            else
                Filters.Operaciones = new AllEncompasingCriteria();

            if (RadioPais.IsChecked is true)
                Filters.Ordenacion = Ordenacion.Pais;
            if (RadioTipo.IsChecked is true)
                Filters.Ordenacion = Ordenacion.TipoSensor;
            if (RadioLugar.IsChecked is true)
                Filters.Ordenacion = Ordenacion.Localizacion;
            if (RadioOps.IsChecked is true)
                Filters.Ordenacion = Ordenacion.Operaciones;

            this.Hide();
        }

        // Eliminar filtros y reiniciar ordenación
        private void ContentDialog_SecondaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            this.Filters = Visualization.Default;
        }
    }
}
