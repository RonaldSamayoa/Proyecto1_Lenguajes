namespace AnalizadorLexico.Modelos
{
    public class Token
    {
        public TipoToken Tipo { get; }
        public string Valor { get; }
        public int Linea { get; }  // Nueva propiedad para rastrear línea
        public int Columna { get; } // Nueva propiedad para rastrear columna

        public Token(TipoToken tipo, string valor, int linea, int columna)
        {
            Tipo = tipo;
            Valor = valor;
            Linea = linea;
            Columna = columna;
        }

        public override string ToString()
        {
            return $"[L{Linea},C{Columna}] {Tipo}: {Valor}";
        }
    }
}