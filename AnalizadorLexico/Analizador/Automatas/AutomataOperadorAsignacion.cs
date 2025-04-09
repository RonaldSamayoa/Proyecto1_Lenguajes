using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AnalizadorLexico.Modelos;

namespace AnalizadorLexico.Analizador.Automatas
{
    public class AutomataOperadorAsignacion : IAutomata
    {
        public bool PuedeAnalizar(char actual)
        {
            return actual == '=';  // Solo analiza el signo igual para asignación
        }

        public Token? Reconocer(string entrada, ref int posicion, ref int linea, ref int columna)
        {
            int inicio = posicion;
            int columnaInicio = columna;

            if (posicion < entrada.Length && entrada[posicion] == '=') // Verifica si el carácter actual es '='
            {
                Avanzar(entrada, ref posicion, ref linea, ref columna);
                return new Token(TipoToken.OperadorAsignacion, "=", linea, columnaInicio);
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
