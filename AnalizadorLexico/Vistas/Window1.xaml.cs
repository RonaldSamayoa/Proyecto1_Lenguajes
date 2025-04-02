using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Microsoft.Win32;
using System.IO;

namespace AnalizadorLexico.Vistas
{
    /// <summary>
    /// Lógica de interacción para Window1 (Ventana Inicial)
    /// // </summary>
    public partial class Window1 : Window
    {
        private bool cambiosSinGuardar = false; // Para saber si hay cambios en el editor
        private string rutaArchivo = string.Empty; // Para almacenar la ruta del archivo abierto
        public Window1()
        {
            InitializeComponent();
            txtCodigoFuente.TextChanged += TxtCodigoFuente_TextChanged;
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
            MessageBox.Show("Opción 'Copiar' seleccionada.");
        }

        private void Pegar_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Opción 'Pegar' seleccionada.");
        }

        private void Deshacer_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Opción 'Deshacer' seleccionada.");
        }

        private void Rehacer_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Opción 'Rehacer' seleccionada.");
        }

        private void AcercaDe_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Aplicación desarrollada por [Tu Nombre].");
        }
        private void txtCodigoFuente_TextChanged(object sender, TextChangedEventArgs e)
        {
            cambiosSinGuardar = true;
        }

    }
}
