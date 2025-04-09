
using AnalizadorLexico.Modelos;

namespace AnalizadorLexico.Analizador.Automatas
{
    public class AutomataSignosAgrupacion : IAutomata
    {
        public bool PuedeAnalizar(char actual) // Detecta paréntesis, llaves y corchetes
        {
            return actual == '(' || actual == ')' ||
                   actual == '{' || actual == '}' ||
                   actual == '[' || actual == ']';
        }

        public Token? Reconocer(string entrada, ref int posicion, ref int linea, ref int columna)
        {
            int columnaInicio = columna;

            if (posicion < entrada.Length)
            {
                char actual = entrada[posicion]; // Toma el carácter actual para evaluar

                if (PuedeAnalizar(actual)) // Verifica si el carácter es un signo de agrupación válido
                {
                    Avanzar(entrada, ref posicion, ref linea, ref columna);
                    return new Token(TipoToken.SignoAgrupacion, actual.ToString(), linea, columnaInicio);
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
