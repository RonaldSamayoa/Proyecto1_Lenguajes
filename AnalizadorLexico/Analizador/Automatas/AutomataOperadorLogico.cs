using AnalizadorLexico.Modelos;
namespace AnalizadorLexico.Analizador.Automatas
{
    public class AutomataOperadorLogico : IAutomata
    {
        public bool PuedeAnalizar(char actual)
        {
            return actual == '&' || actual == '|' || actual == 'A' || actual == 'O';
        }

        public Token? Reconocer(string entrada, ref int posicion, ref int linea, ref int columna)
        {
            int estado = 0;
            int inicio = posicion;
            int columnaInicio = columna;
            string lexema = "";

            while (posicion < entrada.Length)
            {
                char actual = entrada[posicion];

                switch (estado)
                {
                    case 0:
                        if (actual == '&') { estado = 1; lexema += actual; }
                        else if (actual == '|') { estado = 3; lexema += actual; }
                        else if (actual == 'A') { estado = 5; lexema += actual; }
                        else if (actual == 'O') { estado = 8; lexema += actual; }
                        else return null;
                        break;

                    // &&
                    case 1:
                        if (actual == '&') { estado = 2; lexema += actual; }
                        else return null;
                        break;

                    case 2:
                        Avanzar(entrada, ref posicion, ref linea, ref columna);
                        return new Token(TipoToken.OperadorLogico, "&&", linea, columnaInicio);

                    // ||
                    case 3:
                        if (actual == '|') { estado = 4; lexema += actual; }
                        else return null;
                        break;

                    case 4:
                        Avanzar(entrada, ref posicion, ref linea, ref columna);
                        return new Token(TipoToken.OperadorLogico, "||", linea, columnaInicio);

                    // AND
                    case 5:
                        if (actual == 'N') { estado = 6; lexema += actual; }
                        else return null;
                        break;

                    case 6:
                        if (actual == 'D') { estado = 7; lexema += actual; }
                        else return null;
                        break;

                    case 7:
                        Avanzar(entrada, ref posicion, ref linea, ref columna);
                        return new Token(TipoToken.OperadorLogico, "AND", linea, columnaInicio);

                    // OR
                    case 8:
                        if (actual == 'R') { estado = 9; lexema += actual; }
                        else return null;
                        break;

                    case 9:
                        Avanzar(entrada, ref posicion, ref linea, ref columna);
                        return new Token(TipoToken.OperadorLogico, "OR", linea, columnaInicio);
                }

                Avanzar(entrada, ref posicion, ref linea, ref columna);
            }

            return null;
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
