using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnalizadorLexico.Modelos
{
    public class Token
    {
        /// <summary>
        /// Tipo del token, basado en el enum TipoToken.
        /// </summary>
        public TipoToken Tipo { get; }

        /// <summary>
        /// Valor del token encontrado en el código fuente.
        /// </summary>
        public string Valor { get; }

        /// <summary>
        /// Constructor de la clase Token.
        /// </summary>
        /// <param name="tipo">Tipo de token identificado.</param>
        /// <param name="valor">Texto del token.</param>
        public Token(TipoToken tipo, string valor)
        {
            Tipo = tipo;
            Valor = valor;
        }

        /// <summary>
        /// Representación en texto del token, útil para depuración.
        /// </summary>
        /// <returns>Cadena con tipo y valor del token.</returns>
        public override string ToString()
        {
            return $"{Tipo}: {Valor}";
        }
    }
}
