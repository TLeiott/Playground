using Hmd.Core.UI.Controls.CustomControls;
using System.Windows.Input;

namespace Serientermine.UI
{
    /// <summary>
    /// Interaction logic for EditSerieDialog.xaml
    /// </summary>
    partial class EditSerieDialog : HmdWindow
    {
        public EditSerieDialog()
        {
            InitializeComponent();
        }
        private void NumericTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            // Überprüfen, ob die eingegebene Zeichenfolge eine Zahl ist
            if (!IsNumeric(e.Text))
            {
                e.Handled = true; // Verhindert die Eingabe des Zeichens
            }
        }

        private bool IsNumeric(string input)
        {
            return int.TryParse(input, out _);
        }
    }
}
