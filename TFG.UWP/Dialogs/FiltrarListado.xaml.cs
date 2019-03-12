using DotNet.Misc.Extensions.Linq;
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

            FieldTipo.ItemsSource = "".Enumerate().Concat(ValoresCriterio.TipoSensor);
            FieldTipo.SelectedItem = Filters.TipoSensor.ToString();
            FieldPais.ItemsSource = "".Enumerate().Concat(ValoresCriterio.Pais);
            FieldPais.SelectedItem = Filters.Pais.ToString();
            FieldLugar.ItemsSource = "".Enumerate().Concat(ValoresCriterio.Localizacion);
            FieldLugar.SelectedItem = Filters.Localizacion.ToString();
            FieldOps.ItemsSource = "".Enumerate().Concat(ValoresCriterio.Operaciones);
            FieldOps.SelectedItem = Filters.Operaciones.ToString();

            if (Filters.Ordenacion is Ordenacion.Pais)
                RadioPais.IsChecked = true;
            if (Filters.Ordenacion is Ordenacion.TipoSensor)
                RadioTipo.IsChecked = true;
            if (Filters.Ordenacion is Ordenacion.Localizacion)
                RadioLugar.IsChecked = true;
            if (Filters.Ordenacion is Ordenacion.Operaciones)
                RadioOps.IsChecked = true;
        }

        // Aplicar filtros y ordenación
        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            if (FieldTipo.SelectedItem.Equals(""))
                Filters.TipoSensor = new AllEncompasingCriteria();
            else
                Filters.TipoSensor = new PredicateCriteria
                {
                    Evaluate = str => str.Equals(FieldTipo.SelectedItem),
                    Verbose = $"de tipo {FieldTipo.SelectedItem}",
                    StringValue = FieldTipo.SelectedItem.ToString()
                };
            if (FieldPais.SelectedItem.Equals(""))
                Filters.Pais = new AllEncompasingCriteria();
            else
                Filters.Pais = new PredicateCriteria
                {
                    Evaluate = str => str.Equals(FieldPais.SelectedItem),
                    Verbose = $"de {FieldPais.SelectedItem}",
                    StringValue = FieldPais.SelectedItem.ToString()
                };
            if (FieldLugar.SelectedItem.Equals(""))
                Filters.Localizacion = new AllEncompasingCriteria();
            else
                Filters.Localizacion = new PredicateCriteria
                {
                    Evaluate = str => str.Equals(FieldLugar.SelectedItem),
                    Verbose = $"en {FieldLugar.SelectedItem}",
                    StringValue = FieldLugar.SelectedItem.ToString()
                };
            if (FieldOps.SelectedItem.Equals(""))
                Filters.Operaciones = new AllEncompasingCriteria();
            else
                Filters.Operaciones = new PredicateCriteria
                {
                    Evaluate = str => str.Equals(FieldOps.SelectedItem),
                    Verbose = $"en modo {FieldOps.SelectedItem}",
                    StringValue = FieldOps.SelectedItem.ToString()
                };

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
