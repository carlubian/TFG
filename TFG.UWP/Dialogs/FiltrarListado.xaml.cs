using DotNet.Misc.Extensions.Linq;
using System.Linq;
using TFG.Core.Model;
using TFG.Core.Model.Criteria;
using Windows.UI.Xaml.Controls;

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

            this.FieldTipo.ItemsSource = "".Enumerate().Concat(ValoresCriterio.TipoSensor);
            this.FieldTipo.SelectedItem = Filters.TipoSensor.ToString();
            this.FieldPais.ItemsSource = "".Enumerate().Concat(ValoresCriterio.Pais);
            this.FieldPais.SelectedItem = Filters.Pais.ToString();
            this.FieldLugar.ItemsSource = "".Enumerate().Concat(ValoresCriterio.Localizacion);
            this.FieldLugar.SelectedItem = Filters.Localizacion.ToString();
            this.FieldOps.ItemsSource = "".Enumerate().Concat(ValoresCriterio.Operaciones);
            this.FieldOps.SelectedItem = Filters.Operaciones.ToString();

            if (Filters.Ordenacion is Ordenacion.Pais)
                this.RadioPais.IsChecked = true;
            if (Filters.Ordenacion is Ordenacion.TipoSensor)
                this.RadioTipo.IsChecked = true;
            if (Filters.Ordenacion is Ordenacion.Localizacion)
                this.RadioLugar.IsChecked = true;
            if (Filters.Ordenacion is Ordenacion.Operaciones)
                this.RadioOps.IsChecked = true;
        }

        // Aplicar filtros y ordenación
        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            if (this.FieldTipo.SelectedItem.Equals(""))
                this.Filters.TipoSensor = new AllEncompasingCriteria();
            else
                this.Filters.TipoSensor = new PredicateCriteria
                {
                    Evaluate = str => str.Equals(this.FieldTipo.SelectedItem),
                    Verbose = $"de tipo {this.FieldTipo.SelectedItem}",
                    StringValue = this.FieldTipo.SelectedItem.ToString()
                };
            if (this.FieldPais.SelectedItem.Equals(""))
                this.Filters.Pais = new AllEncompasingCriteria();
            else
                this.Filters.Pais = new PredicateCriteria
                {
                    Evaluate = str => str.Equals(this.FieldPais.SelectedItem),
                    Verbose = $"de {this.FieldPais.SelectedItem}",
                    StringValue = this.FieldPais.SelectedItem.ToString()
                };
            if (this.FieldLugar.SelectedItem.Equals(""))
                this.Filters.Localizacion = new AllEncompasingCriteria();
            else
                this.Filters.Localizacion = new PredicateCriteria
                {
                    Evaluate = str => str.Equals(this.FieldLugar.SelectedItem),
                    Verbose = $"en {this.FieldLugar.SelectedItem}",
                    StringValue = this.FieldLugar.SelectedItem.ToString()
                };
            if (this.FieldOps.SelectedItem.Equals(""))
                this.Filters.Operaciones = new AllEncompasingCriteria();
            else
                this.Filters.Operaciones = new PredicateCriteria
                {
                    Evaluate = str => str.Equals(this.FieldOps.SelectedItem),
                    Verbose = $"en modo {this.FieldOps.SelectedItem}",
                    StringValue = this.FieldOps.SelectedItem.ToString()
                };

            if (this.RadioPais.IsChecked is true)
                this.Filters.Ordenacion = Ordenacion.Pais;
            if (this.RadioTipo.IsChecked is true)
                this.Filters.Ordenacion = Ordenacion.TipoSensor;
            if (this.RadioLugar.IsChecked is true)
                this.Filters.Ordenacion = Ordenacion.Localizacion;
            if (this.RadioOps.IsChecked is true)
                this.Filters.Ordenacion = Ordenacion.Operaciones;

            this.Hide();
        }

        // Eliminar filtros y reiniciar ordenación
        private void ContentDialog_SecondaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args) 
            => this.Filters = Visualization.Default;
    }
}
