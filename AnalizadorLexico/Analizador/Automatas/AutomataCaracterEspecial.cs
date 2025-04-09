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
            // Retorna true si el carácter es un espacio, tabulación, salto de línea o retorno de carro
            return actual == ' ' || actual == '\n' || actual == '\r' || actual == '\t' || actual == '\f';
        }

        public Token? Reconocer(string entrada, ref int posicion, ref int linea, ref int columna)
        {
            if (posicion >= entrada.Length) // Verifica que la posición actual esté dentro del rango del texto
                return null;

            char actual = entrada[posicion];

            if (actual == '\n') // Si el carácter es un salto de línea, se incrementa la línea y se reinicia la columna
            {
                linea++;
                columna = 1;
            }
            else
            {
                columna++; // Para otros caracteres especiales, solo se avanza una columna
            }
            //avanza de posición
            posicion++;

            // No se retorna token porque debe ser ignorado
            return new Token(TipoToken.CaracterEspecial, actual.ToString(), linea, columna);
        }
    }
}
