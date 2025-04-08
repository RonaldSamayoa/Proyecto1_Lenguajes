using AnalizadorLexico.Modelos;

namespace AnalizadorLexico.Analizador.Automatas
{
    public class AutomataComentarioLinea : IAutomata
    {
        public bool PuedeAnalizar(char actual)
        {
            return actual == '#';
        }

        public Token? Reconocer(string entrada, ref int posicion, ref int linea, ref int columna)
        {
            int inicio = posicion;
            int columnaInicio = columna;

            while (posicion < entrada.Length && entrada[posicion] != '\n')
            {
                Avanzar(entrada, ref posicion, ref linea, ref columna);
            }

            string lexema = entrada.Substring(inicio, posicion - inicio);
            return new Token(TipoToken.ComentarioLinea, lexema, linea, columnaInicio);
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
