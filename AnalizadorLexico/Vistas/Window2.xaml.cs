using System.Windows;
using AnalizadorLexico.Modelos;

namespace AnalizadorLexico.Vistas
{
    /// <summary>
    /// Errores detectados 
    /// </summary>
    public partial class Window2 : Window
    {
        public Window2(List<Token> errores)
        {
            InitializeComponent();
            lstErrores.ItemsSource = errores;
        }
    }
}
