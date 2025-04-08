using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace AnalizadorLexico.Vistas
{
    // Lógica de interacción para Window4 (buscar patrones)   
    public partial class Window4 : Window
    {
        private RichTextBox editor;

        public Window4(RichTextBox richTextBox)
        {
            InitializeComponent();
            editor = richTextBox;
        }

        private void Buscar_Click(object sender, RoutedEventArgs e)
        {
            string patron = txtPatron.Text;

            // Validación manual 
            bool patronValido = false;
            foreach (char c in patron)
            {
                if (c != ' ' && c != '\t' && c != '\n' && c != '\r')
                {
                    patronValido = true;
                    break;
                }
            }

            if (!patronValido)
            {
                MessageBox.Show("Por favor, ingrese un patrón a buscar.", "Aviso", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            // Limpiar resaltado anterior
            TextRange rangoCompleto = new TextRange(editor.Document.ContentStart, editor.Document.ContentEnd);
            rangoCompleto.ApplyPropertyValue(TextElement.BackgroundProperty, Brushes.Transparent);

            int coincidencias = 0;
            TextPointer posicion = editor.Document.ContentStart;

            while (posicion != null && posicion.CompareTo(editor.Document.ContentEnd) < 0)
            {
                if (posicion.GetPointerContext(LogicalDirection.Forward) == TextPointerContext.Text)
                {
                    string texto = posicion.GetTextInRun(LogicalDirection.Forward);
                    int index = 0;

                    while (index <= texto.Length - patron.Length)
                    {
                        int encontrado = texto.IndexOf(patron, index, StringComparison.CurrentCulture); // Distinción exacta
                        if (encontrado == -1) break;

                        TextPointer inicio = posicion.GetPositionAtOffset(encontrado, LogicalDirection.Forward);
                        TextPointer fin = inicio?.GetPositionAtOffset(patron.Length, LogicalDirection.Forward);

                        if (inicio != null && fin != null)
                        {
                            TextRange resaltado = new TextRange(inicio, fin);
                            resaltado.ApplyPropertyValue(TextElement.BackgroundProperty, Brushes.Yellow);
                            coincidencias++;
                        }

                        index = encontrado + patron.Length;
                    }
                }

                posicion = posicion.GetNextContextPosition(LogicalDirection.Forward);
            }

            txtResultado.Text = $"No. Repeticiones: {coincidencias} {(coincidencias == 1 ? "vez" : "veces")}";
        }


        private void Cerrar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
