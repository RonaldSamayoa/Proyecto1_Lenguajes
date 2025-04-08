using AnalizadorLexico.Modelos;
namespace AnalizadorLexico.Analizador.Automatas
{
    public class AutomataOperadorRelacional : IAutomata
    {
        public bool PuedeAnalizar(char actual)
        {
            return actual == '>' || actual == '<';
        }

        public Token? Reconocer(string entrada, ref int posicion, ref int linea, ref int columna)
        {
            if (posicion >= entrada.Length)
                return null;

            int inicio = posicion;
            int columnaInicio = columna;

            char actual = entrada[posicion];

            if (actual == '>' || actual == '<')
            {
                Avanzar(entrada, ref posicion, ref linea, ref columna);

                if (posicion < entrada.Length && entrada[posicion] == '=')
                {
                    Avanzar(entrada, ref posicion, ref linea, ref columna);
                    string lexema = entrada.Substring(inicio, 2);
                    return new Token(TipoToken.OperadorRelacional, lexema, linea, columnaInicio);
                }

                string simple = entrada.Substring(inicio, 1);
                return new Token(TipoToken.OperadorRelacional, simple, linea, columnaInicio);
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
