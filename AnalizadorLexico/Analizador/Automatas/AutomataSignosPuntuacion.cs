using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AnalizadorLexico.Modelos;

namespace AnalizadorLexico.Analizador.Automatas
{
    public class AutomataSignosPuntuacion : IAutomata
    {
        public bool PuedeAnalizar(char actual)
        {
            return actual == '.' || actual == ',' || actual == ':' || actual == ';'; // Detecta signos de puntuación comunes
        }

        public Token? Reconocer(string entrada, ref int posicion, ref int linea, ref int columna)
        {
            int columnaInicio = columna;

            if (posicion < entrada.Length)
            {
                char actual = entrada[posicion]; // Obtiene el carácter actual desde la entrada

                if (PuedeAnalizar(actual))  // Verifica si el carácter es un signo de puntuación válido
                {
                    Avanzar(entrada, ref posicion, ref linea, ref columna);
                    return new Token(TipoToken.SignoPuntuacion, actual.ToString(), linea, columnaInicio);
                }
            }
            return null;
        }

        private void Avanzar(string entrada, ref int posicion, ref int linea, ref int columna)
        {
            if (posicion < entrada.Length)
            {
                char actual = entrada[posicion++];
                if (actual == '\n')
                {
                    linea++;
                    columna = 1;
                }
                else
                {
                    columna++;
                }
            }
        }
    }
}
