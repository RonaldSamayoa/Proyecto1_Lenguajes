﻿
namespace AnalizadorLexico.Modelos
{
    public enum TipoToken
    {
        CaracterEspecial, // Espacio, \n, \t, \r, \f
        ComentarioLinea,  // Empieza con #
        ComentarioBloque, // /* ... */ con saltos de línea
        NumeroDecimal, // Decimales con o sin signo (+ o -)
        NumeroEntero, // Enteros con o sin signo (+ o -)
        Identificador, // Variables que empiezan con $
        Literal, // Cadenas de texto entre " o '
        OperadorAsignacion, // =
        OperadorAritmetico, // +, -, *, /, ^ 
        OperadorLogico, // &&, ||, AND, OR
        OperadorRelacional, // <, >, <=, >=
        PalabraReservada, // if, else, while, return...
        SignoAgrupacion, // ( ) { } [ ]
        SignoPuntuacion, // ; , : .
        Error, // Cualquier otro símbolo no reconocido
        FinDeLinea // Para manejo explícito de \n
    } 
}
