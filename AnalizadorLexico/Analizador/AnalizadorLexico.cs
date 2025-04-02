using System;
using System.Collections.Generic;
using AnalizadorLexico.Modelos;

namespace AnalizadorLexico.Analizador
{
    public class AnalizadorLexico
    {
        private string codigoFuente; // Almacena el código fuente a analizar
        private int posicion; // Índice actual en la cadena de código fuente
        private List<Token> tokens; // Lista de tokens extraídos del código

        public AnalizadorLexico(string codigo)
        {
            codigoFuente = codigo; // Se asigna el código fuente ingresado
            posicion = 0; // Se inicializa la posición en la primera posición
            tokens = new List<Token>(); // Se crea la lista vacía de tokens
        }

        public List<Token> Analizar()
        {
            while (posicion < codigoFuente.Length) // Se recorre todo el código fuente
            {
                char actual = codigoFuente[posicion]; // Se obtiene el carácter actual

                // Ignorar espacios, tabulaciones y saltos de línea
                if (EsEspacio(actual))
                {
                    posicion++; // Se avanza la posición y continúa el análisis
                    continue;
                }

                // Identificadores que inician con '$'
                if (actual == '$')
                {
                    tokens.Add(ProcesarIdentificador()); // Se procesa el identificador
                    continue;
                }

                // Números (enteros o decimales, con o sin signo)
                if (EsDigito(actual) || actual == '+' || actual == '-')
                {
                    tokens.Add(ProcesarNumero()); // Se procesa el número
                    continue;
                }

                // Literales de cadena (comillas simples o dobles)
                if (actual == '"' || actual == '\'')
                {
                    tokens.Add(ProcesarLiteral(actual)); // Se procesa el literal
                    continue;
                }

                // Comentarios de línea
                if (actual == '#')
                {
                    tokens.Add(ProcesarComentarioLinea()); // Se procesa el comentario
                    continue;
                }

                // Comentarios en bloque
                if (actual == '/' && SiguienteCaracterEs('*'))
                {
                    tokens.Add(ProcesarComentarioBloque()); // Se procesa el comentario en bloque
                    continue;
                }

                // Operadores aritméticos
                if (EsOperadorAritmetico(actual))
                {
                    tokens.Add(new Token(TipoToken.OperadorAritmetico, actual.ToString())); // Se reconoce el operador
                    posicion++;
                    continue;
                }

                // Operadores relacionales
                if (EsOperadorRelacional(actual))
                {
                    tokens.Add(ProcesarOperadorRelacional()); // Se procesa el operador relacional
                    continue;
                }

                // Operadores lógicos
                if (EsOperadorLogico(actual))
                {
                    tokens.Add(ProcesarOperadorLogico()); // Se procesa el operador lógico
                    continue;
                }

                // Signos de agrupación
                if (EsSignoAgrupacion(actual))
                {
                    tokens.Add(new Token(TipoToken.SignoAgrupacion, actual.ToString())); // Se reconoce el signo
                    posicion++;
                    continue;
                }

                // Signos de puntuación
                if (EsSignoPuntuacion(actual))
                {
                    tokens.Add(new Token(TipoToken.SignoPuntuacion, actual.ToString())); // Se reconoce el signo
                    posicion++;
                    continue;
                }

                // Si el carácter no coincide con ninguna regla, se lanza error
                throw new Exception($"Carácter no reconocido: {actual}");
            }

            return tokens; // Se devuelve la lista de tokens generados
        }

        private Token ProcesarIdentificador()
        {
            int inicio = posicion;
            posicion++; // Se salta el '$' inicial

            // Se permiten letras, dígitos, guiones bajos (_) y medios (-)
            while (posicion < codigoFuente.Length && (EsLetraODigito(codigoFuente[posicion]) || codigoFuente[posicion] == '_' || codigoFuente[posicion] == '-'))
            {
                posicion++;
            }

            string valor = codigoFuente.Substring(inicio, posicion - inicio); // Se extrae el identificador

            // Si el identificador es una palabra reservada, se clasifica como tal
            if (EsPalabraReservada(valor))
                return new Token(TipoToken.PalabraReservada, valor);

            return new Token(TipoToken.Identificador, valor);
        }

        private Token ProcesarNumero()
        {
            int inicio = posicion;
            bool tienePunto = false;

            // Verificar si el número tiene signo
            if (codigoFuente[posicion] == '+' || codigoFuente[posicion] == '-')
            {
                posicion++; // Se avanza para evitar contar el signo como dígito
            }

            // Se leen dígitos y opcionalmente un punto decimal
            while (posicion < codigoFuente.Length && (EsDigito(codigoFuente[posicion]) || (!tienePunto && codigoFuente[posicion] == '.')))
            {
                if (codigoFuente[posicion] == '.')
                    tienePunto = true; // Se marca la existencia del punto decimal

                posicion++;
            }

            string valor = codigoFuente.Substring(inicio, posicion - inicio); // Se extrae el número
            return tienePunto ? new Token(TipoToken.NumeroDecimal, valor) : new Token(TipoToken.NumeroEntero, valor);
        }

        private Token ProcesarLiteral(char delimitador)
        {
            int inicio = posicion;
            posicion++; // Se omite la comilla inicial

            // Se avanza hasta encontrar la comilla de cierre
            while (posicion < codigoFuente.Length && codigoFuente[posicion] != delimitador)
            {
                posicion++;
            }

            if (posicion >= codigoFuente.Length)
            {
                throw new Exception("Error: Literal sin cerrar."); // Error si no se cierra el literal
            }

            posicion++; // Se omite la comilla final
            string valor = codigoFuente.Substring(inicio, posicion - inicio);
            return new Token(TipoToken.Literal, valor);
        }

        private Token ProcesarComentarioLinea()
        {
            int inicio = posicion;
            while (posicion < codigoFuente.Length && codigoFuente[posicion] != '\n')
            {
                posicion++; // Se avanza hasta el final de la línea
            }

            string valor = codigoFuente.Substring(inicio, posicion - inicio);
            return new Token(TipoToken.ComentarioLinea, valor);
        }

        private Token ProcesarComentarioBloque()
        {
            int inicio = posicion;
            posicion += 2; // Se omite '/*'

            // Se avanza hasta encontrar '*/'
            while (posicion < codigoFuente.Length - 1 && !(codigoFuente[posicion] == '*' && codigoFuente[posicion + 1] == '/'))
            {
                posicion++;
            }

            if (posicion >= codigoFuente.Length - 1)
            {
                throw new Exception("Error: Comentario en bloque sin cerrar.");
            }

            posicion += 2; // Se omite '*/'
            string valor = codigoFuente.Substring(inicio, posicion - inicio);
            return new Token(TipoToken.ComentarioBloque, valor);
        }

        private Token ProcesarOperadorRelacional()
        {
            int inicio = posicion;
            char actual = codigoFuente[posicion];
            posicion++; // Avanzamos al siguiente carácter

            // Verificamos si el siguiente carácter es '=' para operadores >= o <=
            if (posicion < codigoFuente.Length && codigoFuente[posicion] == '=' && (actual == '<' || actual == '>'))
            {
                posicion++; // Avanzamos una posición más para incluir '='
            }

            // Extraemos el operador relacional del código fuente
            string valor = codigoFuente.Substring(inicio, posicion - inicio);
            return new Token(TipoToken.OperadorRelacional, valor);
        }

        private Token ProcesarOperadorLogico()
        {
            int inicio = posicion;
            char actual = codigoFuente[posicion];

            // Verificamos si es un operador lógico de dos caracteres: && o ||
            if (posicion + 1 < codigoFuente.Length &&
                ((actual == '&' && codigoFuente[posicion + 1] == '&') ||
                 (actual == '|' && codigoFuente[posicion + 1] == '|')))
            {
                posicion += 2; // Avanzamos dos posiciones para consumir ambos caracteres
                return new Token(TipoToken.OperadorLogico, codigoFuente.Substring(inicio, 2));
            }

            // Verificamos si es un operador lógico en palabra: AND o OR
            string posiblePalabra = codigoFuente.Substring(inicio, Math.Min(3, codigoFuente.Length - inicio));

            if (posiblePalabra == "AND" || posiblePalabra == "OR")
            {
                posicion += 3; // Avanzamos tres posiciones para consumir la palabra
                return new Token(TipoToken.OperadorLogico, posiblePalabra);
            }

            // Si no es un operador lógico válido, lanzamos un error
            throw new Exception($"Operador lógico no válido en posición {inicio}");
        }


        // Métodos auxiliares
        private bool EsOperadorAritmetico(char c) => "+-*/^".Contains(c);
        private bool EsOperadorRelacional(char c) => "<>=".Contains(c);
        private bool EsOperadorLogico(char c) => "&|".Contains(c);
        private bool EsSignoAgrupacion(char c) => "[]{}()".Contains(c);
        private bool EsSignoPuntuacion(char c) => ",;:.".Contains(c);
        private bool SiguienteCaracterEs(char c) => posicion + 1 < codigoFuente.Length && codigoFuente[posicion + 1] == c;

        private bool EsPalabraReservada(string palabra)
        {
            HashSet<string> palabrasReservadas = new HashSet<string> {
                "if", "class", "for", "then", "else", "public", "private", "package", "import",
                "static", "void", "int", "true", "false", "extends", "short", "boolean",
                "float", "interface", "final", "protected", "return", "while", "case", "implements"
            };
            return palabrasReservadas.Contains(palabra);
        }

        private bool EsDigito(char c) => c >= '0' && c <= '9';
        private bool EsLetra(char c) => (c >= 'a' && c <= 'z') || (c >= 'A' && c <= 'Z');
        private bool EsLetraODigito(char c) => EsLetra(c) || EsDigito(c);
        private bool EsEspacio(char c) => c == ' ' || c == '\t' || c == '\n' || c == '\r' || c == '\f';
    }
}
