using AnalizadorLexico.Modelos;
namespace AnalizadorLexico.Analizador.Automatas
{
    public class AutomataOperadorAritmetico : IAutomata
    {
        public bool PuedeAnalizar(char actual)
        {
            return actual == '+' || actual == '-' || actual == '*' || actual == '/' || actual == '^';
        }

        public Token? Reconocer(string entrada, ref int posicion, ref int linea, ref int columna)
        {
            int inicio = posicion;
            int columnaInicio = columna;

            if (posicion >= entrada.Length)
                return null;

            char actual = entrada[posicion];

            if (PuedeAnalizar(actual))
            {
                Avanzar(entrada, ref posicion, ref linea, ref columna);
                string lexema = entrada.Substring(inicio, 1);
                return new Token(TipoToken.OperadorAritmetico, lexema, linea, columnaInicio);
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
