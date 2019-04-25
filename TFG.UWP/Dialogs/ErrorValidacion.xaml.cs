using Windows.UI.Xaml.Controls;

// La plantilla de elemento del cuadro de diálogo de contenido está documentada en https://go.microsoft.com/fwlink/?LinkId=234238

namespace TFG.UWP.Dialogs
{
    public sealed partial class ErrorValidacion : ContentDialog
    {
        public ErrorValidacion(string fieldName)
        {
            this.InitializeComponent();
            this.FieldName.Text = fieldName;
        }

        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {

        }
    }
}
