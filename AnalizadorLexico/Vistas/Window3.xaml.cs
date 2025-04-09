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
using AnalizadorLexico.Modelos;

namespace AnalizadorLexico.Vistas
{
    // tokens reconocidos
    public partial class Window3 : Window
    {
        public Window3(List<Token> tokens)
        {
            InitializeComponent();
            lstTokens.ItemsSource = tokens; // Asigna la lista de tokens a la fuente de datos del ListBox
        }
    }
}
