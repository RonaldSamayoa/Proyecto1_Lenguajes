using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using Microsoft.Win32;
using System.IO;
using AnalizadorLexico.Analizador;
using AnalizadorLexico.Vistas;

namespace AnalizadorLexico.Vistas
{
    /// Lógica de interacción para Window1 (Ventana Inicial)

    public partial class Window1 : Window
    {
        private bool cambiosSinGuardar = false; // Para saber si hay cambios en el editor
        private string rutaArchivo = string.Empty; // Para almacenar la ruta del archivo abierto
        public Window1()
        {
            InitializeComponent();
            txtCodigoFuente.TextChanged += TxtCodigoFuente_TextChanged;
            txtCodigoFuente.SelectionChanged += TxtCodigoFuente_SelectionChanged;
        }

        private bool HayCambiosSinGuardar()
        {
            return cambiosSinGuardar;
        }
        private void TxtCodigoFuente_TextChanged(object sender, TextChangedEventArgs e)
        {
            cambiosSinGuardar = true;
        }

        private void Nuevo_Click(object sender, RoutedEventArgs e)
        {
            if (HayCambiosSinGuardar())
            {
                MessageBoxResult resultado = MessageBox.Show(
                    "¿Desea guardar los cambios antes de crear un nuevo archivo?",
                    "Confirmación", MessageBoxButton.YesNoCancel, MessageBoxImage.Warning);

                if (resultado == MessageBoxResult.Yes)
                {
                    Guardar_Click(sender, e); // Llama a "Guardar"
                }
                else if (resultado == MessageBoxResult.Cancel)
                {
                    return; // No hace nada si el usuario cancela
                }
            }
            txtCodigoFuente.Document.Blocks.Clear(); // Borra el texto del RichTextBox
            rutaArchivo = string.Empty; // Resetea la ruta del archivo
        }

        private void Abrir_Click(object sender, RoutedEventArgs e)
        {
            if (cambiosSinGuardar)
            {
                MessageBoxResult resultado = MessageBox.Show("Hay cambios sin guardar. ¿Desea continuar?", "Confirmar", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (resultado == MessageBoxResult.No)
                    return;
            }

            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Archivos de texto (*.txt)|*.txt|Todos los archivos (*.*)|*.*"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                rutaArchivo = openFileDialog.FileName;
                string contenido = File.ReadAllText(rutaArchivo);

                // Limpiar y cargar el texto en el RichTextBox
                txtCodigoFuente.Document.Blocks.Clear();
                txtCodigoFuente.Document.Blocks.Add(new Paragraph(new Run(contenido)));

                cambiosSinGuardar = false;
            }
        }

        private void Guardar_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(rutaArchivo))
            {
                GuardarComo_Click(sender, e); // Si no hay archivo, usa "Guardar Como"
            }
            else
            {
                GuardarArchivo(rutaArchivo);  // Guarda en el archivo existente
            }
        }

        private void GuardarArchivo(string ruta)
        {
            TextRange textRange = new TextRange(txtCodigoFuente.Document.ContentStart, txtCodigoFuente.Document.ContentEnd);
            File.WriteAllText(ruta, textRange.Text);
            cambiosSinGuardar = false;
        }

        private void GuardarComo_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "Archivos de texto (*.txt)|*.txt|Todos los archivos (*.*)|*.*"
            };

            if (saveFileDialog.ShowDialog() == true)
            {
                rutaArchivo = saveFileDialog.FileName;
                GuardarArchivo(rutaArchivo);
            }
        }

        private void Salir_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Copiar_Click(object sender, RoutedEventArgs e)
        {
            txtCodigoFuente.Copy();
        }

        private void Pegar_Click(object sender, RoutedEventArgs e)
        {
            txtCodigoFuente.Paste();
        }

        private void Deshacer_Click(object sender, RoutedEventArgs e)
        {
            if (txtCodigoFuente.CanUndo)
            {
                txtCodigoFuente.Undo();
            }
        }

        private void Rehacer_Click(object sender, RoutedEventArgs e)
        {
            if (txtCodigoFuente.CanRedo)
            {
                txtCodigoFuente.Redo();
            }
        }

        private void AcercaDe_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Aplicación desarrollada por: \n" +
                            "Ronald Renán Samayoa Martínez \n" +
                            "Carnet: 202031046");
        }
        private void txtCodigoFuente_TextChanged(object sender, TextChangedEventArgs e)
        {
            cambiosSinGuardar = true;
        }
        private void TxtCodigoFuente_SelectionChanged(object sender, RoutedEventArgs e)
        {
            TextPointer caretPosition = txtCodigoFuente.CaretPosition;
            TextPointer start = txtCodigoFuente.Document.ContentStart;

            int line = 1;
            int column = 1;

            TextRange fullText = new TextRange(start, caretPosition);
            string textUpToCaret = fullText.Text;

            int lastNewLineIndex = textUpToCaret.LastIndexOf('\n');

            if (lastNewLineIndex == -1)
            {
                // No hay saltos de línea antes del cursor → Estamos en la primera línea
                column = textUpToCaret.Length + 1;
            }
            else
            {
                // Contamos cuántos saltos de línea hay para determinar la línea actual
                line = textUpToCaret.Count(c => c == '\n') + 1;
                column = textUpToCaret.Length - lastNewLineIndex;
            }

            lblPosicionCursor.Text = $"Línea: {line}, Columna: {column}";
        }
        //botones
        private void Limpiar_Click(object sender, RoutedEventArgs e)
        {
            txtCodigoFuente.Document.Blocks.Clear(); // Borra el contenido del RichTextBox
            cambiosSinGuardar = false; // Reinicia el estado de cambios
        }

        private async void btnAnalizar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Obtener el texto del RichTextBox
                TextRange textRange = new TextRange(txtCodigoFuente.Document.ContentStart, txtCodigoFuente.Document.ContentEnd);
                string codigoFuente = textRange.Text;

                // Ejecutar el análisis en un hilo separado
                var tokens = await Task.Run(() =>
                {
                    var analizador = new Analizador.AnalizadorLexico(codigoFuente);
                    return analizador.Analizar();
                });

                // Verificar si hay errores
                var errores = tokens.Where(t => t.Tipo == Modelos.TipoToken.Error).ToList();

                // Usar Dispatcher.Invoke para actualizar la UI de manera segura
                Dispatcher.Invoke(() =>
                {
                    if (errores.Any())
                    {
                        // Mostrar ventana de errores
                        var ventanaErrores = new Window2(errores);
                        ventanaErrores.Show();
                    }
                    else
                    {
                        // Mostrar tokens
                        var ventanaTokens = new Window3(tokens);
                        ventanaTokens.Show();
                    }
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ocurrió un error durante el análisis: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void btnBuscarPatron_Click(object sender, RoutedEventArgs e)
        {
            var ventanaBuscar = new Window4(txtCodigoFuente);
            ventanaBuscar.ShowDialog();
        }

    }
}
