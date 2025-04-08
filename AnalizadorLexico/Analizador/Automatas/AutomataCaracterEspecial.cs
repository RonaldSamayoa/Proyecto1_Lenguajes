using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AnalizadorLexico.Modelos;

namespace AnalizadorLexico.Analizador.Automatas
{
    public class AutomataCaracterEspecial : IAutomata
    {
        public bool PuedeAnalizar(char actual)
        {
            return actual == ' ' || actual == '\n' || actual == '\t' || actual == '\r' || actual == '\f';
        }

        public Token? Reconocer(string entrada, ref int posicion, ref int linea, ref int columna)
        {
            char actual = entrada[posicion];

            if (!PuedeAnalizar(actual))
                return null;

            int columnaInicio = columna;

            Avanzar(entrada, ref posicion, ref linea, ref columna);

            // Se reconoce pero se ignora: devuelve null para no registrar el token
            return null;
        }

        private void Avanzar(string entrada, ref int posicion, ref int linea, ref int columna)
        {
            if (posicion >= entrada.Length)
                return;

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
