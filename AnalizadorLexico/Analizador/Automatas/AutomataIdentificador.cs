
using AnalizadorLexico.Modelos;

namespace AnalizadorLexico.Analizador.Automatas
{
    public class AutomataIdentificador : IAutomata
    {
        public bool PuedeAnalizar(char actual)
        {
            return actual == '$';
        }

        public Token? Reconocer(string entrada, ref int posicion, ref int linea, ref int columna)
        {
            int inicio = posicion;
            int columnaInicio = columna;
            // Verifica que efectivamente comience con '$'
            if (entrada[posicion] != '$')
                return null;
            // Avanza una posición (consume el '$')
            Avanzar(entrada, ref posicion, ref linea, ref columna); // Salta el '$'
            // Mientras siga habiendo letras, números, guiones bajos o medios
            while (posicion < entrada.Length &&
                   (EsLetra(entrada[posicion]) || EsNumero(entrada[posicion]) || 
                   entrada[posicion] == '_' || entrada[posicion] == '-'))
            {
                Avanzar(entrada, ref posicion, ref linea, ref columna);
            }
            // Extrae el lexema completo
            string lexema = entrada.Substring(inicio, posicion - inicio);
            return new Token(TipoToken.Identificador, lexema, linea, columnaInicio);
        }
        private bool EsLetra(char c) =>
            (c >= 'a' && c <= 'z') || (c >= 'A' && c <= 'Z');

        private bool EsNumero(char c) =>
            c >= '0' && c <= '9';
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
