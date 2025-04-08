
using AnalizadorLexico.Modelos;

namespace AnalizadorLexico.Analizador.Automatas
{
    public class AutomataLiteral : IAutomata
    {
        public bool PuedeAnalizar(char actual)
        {
            return actual == '"' || actual == '\'';
        }

        public Token? Reconocer(string entrada, ref int posicion, ref int linea, ref int columna)
        {
            if (posicion >= entrada.Length)
                return null;

            int inicio = posicion;
            int columnaInicio = columna;
            char comillaApertura = entrada[posicion];

            // Asegurarse que empieza con comilla válida
            if (comillaApertura != '"' && comillaApertura != '\'')
                return null;

            Avanzar(entrada, ref posicion, ref linea, ref columna); // Consumimos la comilla inicial

            while (posicion < entrada.Length)
            {
                char actual = entrada[posicion];

                if (actual == '\n')
                {
                    // No se permite salto de línea en cadena
                    return new Token(TipoToken.Error, entrada.Substring(inicio, posicion - inicio), linea, columnaInicio);
                }

                if (actual == comillaApertura)
                {
                    // Fin de la cadena
                    Avanzar(entrada, ref posicion, ref linea, ref columna); // Consumimos la comilla de cierre
                    string lexema = entrada.Substring(inicio, posicion - inicio);
                    return new Token(TipoToken.Literal, lexema, linea, columnaInicio);
                }

                Avanzar(entrada, ref posicion, ref linea, ref columna);
            }

            // Si llegamos al final sin encontrar comilla de cierre
            string errorLexema = entrada.Substring(inicio, posicion - inicio);
            return new Token(TipoToken.Error, errorLexema, linea, columnaInicio);
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
