using AnalizadorLexico.Analizador.Automatas;
using AnalizadorLexico.Modelos;
namespace AnalizadorLexico.Analizador
{
    public class AnalizadorLexico
    {
        private readonly List<IAutomata> automatas;
        private readonly List<Token> tokens;
        private int posicion;
        private int linea;
        private int columna;
        private readonly string entrada;

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
                new AutomataNumero(),
                new AutomataOperadorRelacional(),
                new AutomataOperadorLogico(),
                new AutomataOperadorAritmetico(),
                new AutomataOperadorAsignacion(),
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
        public List<Token> Analizar()
        {
            tokens.Clear();

            while (posicion < entrada.Length)
            {
                char actual = entrada[posicion];
                IAutomata? automata = automatas.FirstOrDefault(a => a.PuedeAnalizar(actual));
                Token? token = automata?.Reconocer(entrada, ref posicion, ref linea, ref columna);

                if (token != null && token.Tipo != TipoToken.CaracterEspecial) // ignorar espacios/tabulaciones
                {
                    tokens.Add(token);
                }
            }

            tokens.Add(new Token(TipoToken.FinDeLinea, string.Empty, linea, columna));
            return tokens;
        }

        public List<Token> ObtenerTokens()
        {
            return tokens;
        }
    }
}
