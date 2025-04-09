using AnalizadorLexico.Analizador.Automatas;
using AnalizadorLexico.Modelos;
namespace AnalizadorLexico.Analizador
{
    public class AnalizadorLexico
    {
        private readonly List<IAutomata> automatas;// Lista de autómatas para reconocer los distintos tipos de tokens
        private readonly List<Token> tokens; // Lista de tokens reconocidos tras el análisis
        private int posicion;
        private int linea;
        private int columna;
        private readonly string entrada; // Código fuente que se desea analizar

        public AnalizadorLexico(string codigoFuente)
        {
            automatas = new List<IAutomata>
            {
                new AutomataCaracterEspecial(),
                new AutomataComentarioLinea(),
                new AutomataComentarioBloque(),
                new AutomataLiteral(),
                new AutomataPalabraReservada(),
                new AutomataIdentificador(),
                new AutomataOperadorRelacional(),
                new AutomataOperadorLogico(),
                new AutomataOperadorAritmetico(),
                new AutomataOperadorAsignacion(),
                new AutomataNumero(),
                new AutomataSignosAgrupacion(),
                new AutomataSignosPuntuacion(),
                new AutomataError()
            };

            tokens = new List<Token>();
            entrada = codigoFuente;
            posicion = 0;
            linea = 1;
            columna = 1;
        }
        // Método principal para analizar el texto de entrada y generar los tokens
        public List<Token> Analizar()
        {
            tokens.Clear(); // Limpiar cualquier resultado previo

            while (posicion < entrada.Length) // Se analiza mientras no se llegue al final de la entrada
            {
                char actual = entrada[posicion];
                Token? tokenReconocido = null;

                // Se prueba cada autómata para ver cuál puede procesar el carácter actual
                foreach (var automata in automatas)
                {
                    if (automata.PuedeAnalizar(actual))
                    {
                        tokenReconocido = automata.Reconocer(entrada, ref posicion, ref linea, ref columna);
                        break;
                    }
                }

                // Si no se reconoció nada, usamos AutomataError
                if (tokenReconocido == null)
                {
                    var automataError = automatas.OfType<AutomataError>().FirstOrDefault();
                    if (automataError != null)
                    {
                        tokenReconocido = automataError.Reconocer(entrada, ref posicion, ref linea, ref columna);
                    }
                    else
                    {
                        // Seguridad extrema: avanzar a mano
                        posicion++;
                        columna++;
                        continue;
                    }
                }
                // Se agrega el token reconocido, salvo que sea un carácter especial
                if (tokenReconocido != null && tokenReconocido.Tipo != TipoToken.CaracterEspecial)
                {
                    tokens.Add(tokenReconocido);
                }
            }
            // Al final, se agrega un token que indica fin de línea o fin de archivo
            tokens.Add(new Token(TipoToken.FinDeLinea, string.Empty, linea, columna));
            return tokens;
        }

        // Método para avanzar en la entrada
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
        public List<Token> ObtenerTokens()
        {
            return tokens;
        }
    }
}
