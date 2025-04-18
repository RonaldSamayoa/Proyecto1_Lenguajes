﻿using AnalizadorLexico.Modelos;
namespace AnalizadorLexico.Analizador.Automatas
{
    public class AutomataError : IAutomata
    {
        public bool PuedeAnalizar(char actual)
        {
            // Siempre puede analizar, es el último recurso
            return true;
        }
        public Token? Reconocer(string entrada, ref int posicion, ref int linea, ref int columna)
        {
            if (posicion >= entrada.Length) // Si ya se llegó al final de la entrada, no hay nada que reconocer
                return null;

            int columnaInicio = columna;
            char simbolo = entrada[posicion];
            Avanzar(entrada, ref posicion, ref linea, ref columna);

            return new Token(TipoToken.Error, simbolo.ToString(), linea, columnaInicio);
        }
        // Método auxiliar que avanza en la entrada y actualiza línea y columna
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
