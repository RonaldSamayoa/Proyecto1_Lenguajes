
using AnalizadorLexico.Modelos;

namespace AnalizadorLexico.Analizador.Automatas
{
    public class AutomataComentarioBloque : IAutomata
    {
        public bool PuedeAnalizar(char actual)
        {
            return actual == '/';
        }

        public Token? Reconocer(string entrada, ref int posicion, ref int linea, ref int columna)
        {
            int inicio = posicion;
            int columnaInicio = columna;

            if (posicion + 1 >= entrada.Length || entrada[posicion + 1] != '*')
                return null;

            // Avanza por '/*'
            Avanzar(entrada, ref posicion, ref linea, ref columna);
            Avanzar(entrada, ref posicion, ref linea, ref columna);

            while (posicion + 1 < entrada.Length)
            {
                if (entrada[posicion] == '*' && entrada[posicion + 1] == '/')
                {
                    Avanzar(entrada, ref posicion, ref linea, ref columna);
                    Avanzar(entrada, ref posicion, ref linea, ref columna);
                    string lexema = entrada.Substring(inicio, posicion - inicio);
                    return new Token(TipoToken.ComentarioBloque, lexema, linea, columnaInicio);
                }

                Avanzar(entrada, ref posicion, ref linea, ref columna);
            }

            // Si llegó aquí, no encontró cierre => es un error
            string error = entrada.Substring(inicio, posicion - inicio);
            return new Token(TipoToken.Error, error, linea, columnaInicio);
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
