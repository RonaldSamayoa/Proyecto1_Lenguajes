
using AnalizadorLexico.Modelos;

namespace AnalizadorLexico.Analizador
{
    public interface IAutomata
    {
        Token? Reconocer(string entrada, ref int posicion, ref int linea, ref int columna);
        bool PuedeAnalizar(char actual);
    }
}
