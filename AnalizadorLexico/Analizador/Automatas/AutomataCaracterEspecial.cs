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
            return actual == ' ' || actual == '\n' || actual == '\r' || actual == '\t' || actual == '\f';
        }

        public Token? Reconocer(string entrada, ref int posicion, ref int linea, ref int columna)
        {
            if (posicion >= entrada.Length)
                return null;

            char actual = entrada[posicion];

            if (actual == '\n')
            {
                linea++;
                columna = 1;
            }
            else
            {
                columna++;
            }

            posicion++;

            // No se retorna token porque debe ser ignorado
            return new Token(TipoToken.CaracterEspecial, actual.ToString(), linea, columna);
        }
    }
}
