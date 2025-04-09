namespace AnalizadorLexico.Modelos
{
    public class Token
    {
        public TipoToken Tipo { get; }
        public string Valor { get; }
        public int Linea { get; }  // propiedad para rastrear línea
        public int Columna { get; } // propiedad para rastrear columna

        public Token(TipoToken tipo, string valor, int linea, int columna)
        {
            Tipo = tipo;    //tipo de token
            Valor = valor; //lexema
            Linea = linea;
            Columna = columna;
        }

        public override string ToString()
        {
            return $"[Linea{Linea},Columna{Columna}] {Tipo}: {Valor}";
        }
    }
}