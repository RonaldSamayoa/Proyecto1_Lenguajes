
using AnalizadorLexico.Modelos;

namespace AnalizadorLexico.Analizador.Automatas
{
    public class AutomataComentarioBloque : IAutomata
    {
        public bool PuedeAnalizar(char actual)
        {
            return actual == '/'; // Solo puede analizar si el carácter es una barra '/'
        }

        public Token? Reconocer(string entrada, ref int posicion, ref int linea, ref int columna)
        {
            int inicio = posicion;
            int columnaInicio = columna;
            // Verifica que haya al menos dos caracteres y que el siguiente sea '*'
            if (posicion + 1 >= entrada.Length || entrada[posicion + 1] != '*')
                return null;

            // Avanza por '/*'
            Avanzar(entrada, ref posicion, ref linea, ref columna);
            Avanzar(entrada, ref posicion, ref linea, ref columna);
            // Bucle que avanza hasta encontrar el cierre '*/' o llegar al final
            while (posicion + 1 < entrada.Length)
            {
                if (entrada[posicion] == '*' && entrada[posicion + 1] == '/') // Si encuentra el cierre '*/'
                {
                    Avanzar(entrada, ref posicion, ref linea, ref columna); // Avanza por '*'
                    Avanzar(entrada, ref posicion, ref linea, ref columna); 
                    string lexema = entrada.Substring(inicio, posicion - inicio); // Avanza por '/'
                    return new Token(TipoToken.ComentarioBloque, lexema, linea, columnaInicio);
                }
                // Sigue avanzando dentro del comentario
                Avanzar(entrada, ref posicion, ref linea, ref columna);
            }

            // Si llegó aquí, no encontró cierre y es un error
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
