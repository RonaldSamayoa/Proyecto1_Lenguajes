
using AnalizadorLexico.Modelos;

namespace AnalizadorLexico.Analizador
{
    // Interfaz que define el contrato que deben seguir todos los autómatas léxicos
    public interface IAutomata
    {
        // Método que intenta reconocer un token a partir de la posición actual en la entrada
        Token? Reconocer(string entrada, ref int posicion, ref int linea, ref int columna);
        bool PuedeAnalizar(char actual); // Método que indica si este autómata puede analizar un carácter dado
    }
}
