using AnalizadorLexico.Modelos;

namespace AnalizadorLexico.Analizador.Automatas
{
    public class AutomataPalabraReservada : IAutomata
    {
        private readonly string[] palabrasReservadas = new string[]
        {
            "if", "class", "for", "then", "else", "public", "private", "package",
            "import", "static", "void", "int", "true", "false", "extends", "short",
            "boolean", "float", "interface", "final", "protected", "return", "while",
            "case", "implements"
        };

        public bool PuedeAnalizar(char actual)
        {
            return EsLetra(actual); // Comienzan con letra obligatoriamente
        }

        public Token? Reconocer(string entrada, ref int posicion, ref int linea, ref int columna)
        {
            int inicio = posicion;
            int columnaInicio = columna;

            while (posicion < entrada.Length && EsLetra(entrada[posicion])) // Consume letras consecutivas para formar un posible lexema
            {
                Avanzar(entrada, ref posicion, ref linea, ref columna);
            }

            string lexema = entrada.Substring(inicio, posicion - inicio);

            // Validar si es palabra reservada exacta
            foreach (var palabra in palabrasReservadas) // Recorre todas las palabras reservadas
            {
                if (lexema == palabra) // Verifica coincidencia exacta con una palabra reservada
                {
                    return new Token(TipoToken.PalabraReservada, lexema, linea, columnaInicio);
                }
            }

            // Si no es palabra reservada, restaura la posición
            posicion = inicio;
            columna = columnaInicio;
            return null;
        }
        private bool EsLetra(char c) =>
            (c >= 'a' && c <= 'z') || (c >= 'A' && c <= 'Z');

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
