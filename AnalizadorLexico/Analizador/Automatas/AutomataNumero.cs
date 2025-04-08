using AnalizadorLexico.Modelos;

namespace AnalizadorLexico.Analizador.Automatas
{
    public class AutomataNumero : IAutomata
    {
        public bool PuedeAnalizar(char actual)
        {
            return actual == '+' || actual == '-' || (actual >= '0' && actual <= '9');
        }

        public Token? Reconocer(string entrada, ref int posicion, ref int linea, ref int columna)
        {
            int inicio = posicion;
            int columnaInicio = columna;
            bool esDecimal = false;

            // Paso 1: signo opcional (solo si va seguido de dígito)
            if (entrada[posicion] == '+' || entrada[posicion] == '-')
            {
                if (posicion + 1 >= entrada.Length || !EsNumero(entrada[posicion + 1]))
                {
                    return null; // No es número, dejar que AutomataOperadores lo procese
                }
                Avanzar(entrada, ref posicion, ref linea, ref columna);
                if (posicion >= entrada.Length) return null;
            }

            // Paso 2: primer dígito
            if (!EsNumero(entrada[posicion]))
                return null;

            // Paso 3: validación de ceros a la izquierda
            if (entrada[posicion] == '0')
            {
                Avanzar(entrada, ref posicion, ref linea, ref columna);

                // Si hay un punto, es decimal tipo "0.x"
                if (posicion < entrada.Length && entrada[posicion] == '.')
                {
                    esDecimal = true;
                    Avanzar(entrada, ref posicion, ref linea, ref columna);

                    // Requiere al menos un dígito luego del punto
                    if (posicion >= entrada.Length || !EsNumero(entrada[posicion]))
                        return null;

                    while (posicion < entrada.Length && EsNumero(entrada[posicion]))
                        Avanzar(entrada, ref posicion, ref linea, ref columna);
                }
                else if (posicion < entrada.Length && EsNumero(entrada[posicion]))
                {
                    // No se permiten ceros al inicio seguidos de más dígitos
                    return null;
                }
            }
            else
            {
                // Primer dígito distinto de cero, avanzar por los enteros
                while (posicion < entrada.Length && EsNumero(entrada[posicion]))
                    Avanzar(entrada, ref posicion, ref linea, ref columna);

                // Si hay punto → es decimal
                if (posicion < entrada.Length && entrada[posicion] == '.')
                {
                    esDecimal = true;
                    Avanzar(entrada, ref posicion, ref linea, ref columna);

                    if (posicion >= entrada.Length || !EsNumero(entrada[posicion]))
                        return null;

                    while (posicion < entrada.Length && EsNumero(entrada[posicion]))
                        Avanzar(entrada, ref posicion, ref linea, ref columna);
                }
            }

            string lexema = entrada.Substring(inicio, posicion - inicio);
            return new Token(esDecimal ? TipoToken.NumeroDecimal : TipoToken.NumeroEntero, lexema, linea, columnaInicio);
        }

        private bool EsNumero(char c)
        {
            return c >= '0' && c <= '9';
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
