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
            txtCodigoFuente.TextChanged += TxtCodigoFuente_TextChanged; // Evento para detectar cambios en el texto
            txtCodigoFuente.SelectionChanged += TxtCodigoFuente_SelectionChanged; // Evento para mostrar posición del cursor
            this.Closing += Window1_Closing; // Evento que se dispara al cerrar la ventana
        }

        private bool HayCambiosSinGuardar()
        {
            return cambiosSinGuardar;
        }
        private void TxtCodigoFuente_TextChanged(object sender, TextChangedEventArgs e)
        {
            cambiosSinGuardar = true; // Marca que el contenido ha sido modificado
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
            txtCodigoFuente.Document.Blocks.Clear(); // Limpia el texto del editor
            rutaArchivo = string.Empty; // Reinicia la ruta del archivo al crear uno nuevo
        }

        private void Abrir_Click(object sender, RoutedEventArgs e)
        {
            if (cambiosSinGuardar)
            {
                MessageBoxResult resultado = MessageBox.Show("Hay cambios sin guardar. ¿Desea continuar?", "Confirmar", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (resultado == MessageBoxResult.No)
                    return;
            }
            // Diálogo para abrir archivos
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Archivos de texto (*.txt)|*.txt|Todos los archivos (*.*)|*.*"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                rutaArchivo = openFileDialog.FileName;
                string contenido = File.ReadAllText(rutaArchivo); // Lee el contenido del archivo

                // Limpiar y cargar el texto en el RichTextBox
                txtCodigoFuente.Document.Blocks.Clear();
                txtCodigoFuente.Document.Blocks.Add(new Paragraph(new Run(contenido))); // Carga el contenido en el editor
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
            if (HayCambiosSinGuardar())
            {
                MessageBoxResult resultado = MessageBox.Show(
                    "¿Desea guardar los cambios antes de salir?",
                    "Confirmación",
                    MessageBoxButton.YesNoCancel,
                    MessageBoxImage.Warning);

                if (resultado == MessageBoxResult.Yes)
                {
                    Guardar_Click(sender, e); // Intenta guardar
                    if (HayCambiosSinGuardar()) // Si después de guardar aún hay cambios (usuario canceló el guardado)
                    {
                        return; // No cerrar
                    }
                    this.Close(); // Cerrar después de guardar
                }
                else if (resultado == MessageBoxResult.No)
                {
                    this.Close(); // Cerrar sin guardar
                }
                // Si es Cancel, no hace nada
            }
            else
            {
                this.Close(); // Cerrar directamente si no hay cambios
            }
        }
        // Nuevo método para manejar el cierre con la X
        private void Window1_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (HayCambiosSinGuardar())
            {
                MessageBoxResult resultado = MessageBox.Show(
                    "¿Desea guardar los cambios antes de salir?",
                    "Confirmación",
                    MessageBoxButton.YesNoCancel,
                    MessageBoxImage.Warning);

                if (resultado == MessageBoxResult.Yes)
                {
                    Guardar_Click(null, null);
                    if (HayCambiosSinGuardar())
                    {
                        e.Cancel = true; // Cancela el cierre si aún hay cambios
                    }
                }
                else if (resultado == MessageBoxResult.Cancel)
                {
                    e.Cancel = true; // Cancela el cierre
                }
                // Si es No, permite el cierre (e.Cancel sigue siendo false)
            }
        } 
        private void Copiar_Click(object sender, RoutedEventArgs e)
        {
            txtCodigoFuente.Copy(); // Copia el texto seleccionado al portapapeles
        }

        private void Pegar_Click(object sender, RoutedEventArgs e)
        {
            txtCodigoFuente.Paste(); // Pega texto del portapapeles
        }

        private void Deshacer_Click(object sender, RoutedEventArgs e)
        {
            if (txtCodigoFuente.CanUndo)
            {
                txtCodigoFuente.Undo(); // Deshace la última acción
            }
        }

        private void Rehacer_Click(object sender, RoutedEventArgs e)
        {
            if (txtCodigoFuente.CanRedo)
            {
                txtCodigoFuente.Redo(); // Rehace la última acción deshecha
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

            int lastNewLineIndex = textUpToCaret.LastIndexOf('\n'); // Encuentra la última línea antes del cursor

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

            lblPosicionCursor.Text = $"Línea: {line}, Columna: {column}"; // Actualiza la etiqueta con la posición del cursor
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
                TextRange textRange = new TextRange(txtCodigoFuente.Document.ContentStart, txtCodigoFuente.Document.ContentEnd); // Captura el código fuente
                string codigoFuente = textRange.Text;  // Extrae el texto plano del editor

                // Ejecuta el análisis léxico en segundo plano
                var tokens = await Task.Run(() =>
                {
                    var analizador = new Analizador.AnalizadorLexico(codigoFuente);
                    return analizador.Analizar();
                });

                // Verificar si hay errores
                var errores = tokens.Where(t => t.Tipo == Modelos.TipoToken.Error).ToList(); //filtra tokens de error

                // Usar Dispatcher.Invoke para actualizar la interfaz de usuario de manera segura
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
            var ventanaBuscar = new Window4(txtCodigoFuente); // Lleva a la ventana de buscar un patrón
            ventanaBuscar.ShowDialog();
        }
    }
}
