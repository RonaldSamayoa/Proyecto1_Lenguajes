using AnalizadorLexico.Modelos;
namespace AnalizadorLexico.Analizador.Automatas
{
    public class AutomataOperadorRelacional : IAutomata
    {
        public bool PuedeAnalizar(char actual)
        {
            return actual == '>' || actual == '<'; // Analiza comparadores relacionales básicos
        }

        public Token? Reconocer(string entrada, ref int posicion, ref int linea, ref int columna)
        {
            if (posicion >= entrada.Length)
                return null;

            int inicio = posicion;
            int columnaInicio = columna;

            char actual = entrada[posicion];

            if (actual == '>' || actual == '<') // Verifica que sea uno de los símbolos relacionales
            {
                Avanzar(entrada, ref posicion, ref linea, ref columna);

                if (posicion < entrada.Length && entrada[posicion] == '=') // Verifica si es comparador compuesto (>= o <=)
                {
                    Avanzar(entrada, ref posicion, ref linea, ref columna);
                    string lexema = entrada.Substring(inicio, 2); // Extrae operador compuesto de dos caracteres
                    return new Token(TipoToken.OperadorRelacional, lexema, linea, columnaInicio);
                }

                string simple = entrada.Substring(inicio, 1); // Extrae operador simple de un solo carácter
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
