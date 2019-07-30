using Windows.UI.Xaml.Controls;

// La plantilla de elemento del cuadro de diálogo de contenido está documentada en https://go.microsoft.com/fwlink/?LinkId=234238

namespace TFG.UWP.Dialogs
{
    public sealed partial class ErrorRango : ContentDialog
    {
        public ErrorRango(string fieldName, int min, int max)
        {
            this.InitializeComponent();
            this.FieldName.Text = fieldName;
            this.FieldRange.Text = $"[{min}..{max}]";
        }

        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {

        }
    }
}
