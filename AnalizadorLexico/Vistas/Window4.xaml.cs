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
        private RichTextBox editor; // Referencia al RichTextBox del que se realizarán las búsquedas

        public Window4(RichTextBox richTextBox)
        {
            InitializeComponent();
            editor = richTextBox;
        }
        // Evento de clic para iniciar la búsqueda del patrón
        private void Buscar_Click(object sender, RoutedEventArgs e)
        {
            string patron = txtPatron.Text; // Obtiene el patrón de búsqueda desde el TextBox

            // Validación manual para asegurarse de que el patrón no esté vacío
            bool patronValido = false;
            foreach (char c in patron) // Asegura que el patrón contenga al menos un carácter no blanco
            {
                if (c != ' ' && c != '\t' && c != '\n' && c != '\r')
                {
                    patronValido = true;
                    break;
                }
            }
            // Si el patrón es vacío o solo contiene espacios, muestra un mensaje de advertencia
            if (!patronValido)
            {
                MessageBox.Show("Por favor, ingrese un patrón a buscar.", "Aviso", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            // Limpiar resaltado anterior
            TextRange rangoCompleto = new TextRange(editor.Document.ContentStart, editor.Document.ContentEnd);
            rangoCompleto.ApplyPropertyValue(TextElement.BackgroundProperty, Brushes.Transparent);

            int coincidencias = 0;// Contador de coincidencias encontradas
            TextPointer posicion = editor.Document.ContentStart; // Comienza desde el inicio del documento
            // Itera sobre cada posición del documento hasta el final
            while (posicion != null && posicion.CompareTo(editor.Document.ContentEnd) < 0)
            {
                // Verifica si la posición actual es parte del texto
                if (posicion.GetPointerContext(LogicalDirection.Forward) == TextPointerContext.Text)
                {
                    string texto = posicion.GetTextInRun(LogicalDirection.Forward); // Obtiene el texto en la posición actual
                    int index = 0;  // Índice para buscar el patrón
                    // Busca el patrón dentro del texto
                    while (index <= texto.Length - patron.Length)
                    {
                        // Encuentra la primera coincidencia del patrón en el texto
                        int encontrado = texto.IndexOf(patron, index, StringComparison.CurrentCulture); // Distinción exacta
                        if (encontrado == -1) break; // Si no se encuentra, rompe el ciclo
                        // Calcula las posiciones de inicio y fin de la coincidencia
                        TextPointer inicio = posicion.GetPositionAtOffset(encontrado, LogicalDirection.Forward);
                        TextPointer fin = inicio?.GetPositionAtOffset(patron.Length, LogicalDirection.Forward);

                        // Si se encuentran las posiciones de inicio y fin, resalta el patrón encontrado
                        if (inicio != null && fin != null)
                        {
                            TextRange resaltado = new TextRange(inicio, fin); // Crea un rango de texto
                            // Aplica el color de fondo amarillo al resaltado
                            resaltado.ApplyPropertyValue(TextElement.BackgroundProperty, Brushes.Yellow);
                            coincidencias++; // Incrementa el contador de coincidencias
                        }
                        // Actualiza el índice para buscar la siguiente coincidencia
                        index = encontrado + patron.Length;
                    }
                }
                // Se mueve a la siguiente posición en el documento
                posicion = posicion.GetNextContextPosition(LogicalDirection.Forward);
            }
            // Muestra el número de coincidencias encontradas
            txtResultado.Text = $"No. Repeticiones: {coincidencias} {(coincidencias == 1 ? "vez" : "veces")}";
        }
        private void Cerrar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
