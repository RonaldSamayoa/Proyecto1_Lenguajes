using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnalizadorLexico.Analizador
{
    public class Automata
    {
        private Dictionary<(int, char), int> transiciones; // Mapeo de (estado, símbolo) -> nuevo estado
        private HashSet<int> estadosAceptacion; // Conjunto de estados de aceptación
        private int estadoInicial; // Estado de inicio

        public Automata()
        {
            transiciones = new Dictionary<(int, char), int>();
            estadosAceptacion = new HashSet<int>();
            estadoInicial = 0; // Por defecto, el estado inicial es 0
        }

        /// <summary>
        /// Agrega una transición al autómata.
        /// </summary>
        public void AgregarTransicion(int estadoDesde, char simbolo, int estadoHacia)
        {
            transiciones[(estadoDesde, simbolo)] = estadoHacia;
        }

        /// <summary>
        /// Define un estado como estado de aceptación.
        /// </summary>
        public void AgregarEstadoAceptacion(int estado)
        {
            estadosAceptacion.Add(estado);
        }

        /// <summary>
        /// Intenta reconocer un token evaluando la entrada con el autómata.
        /// </summary>
        public bool ReconocerToken(string entrada)
        {
            int estadoActual = estadoInicial;

            foreach (char c in entrada)
            {
                if (transiciones.TryGetValue((estadoActual, c), out int nuevoEstado))
                {
                    estadoActual = nuevoEstado;
                }
                else
                {
                    return false; // No hay transición válida, la cadena no es aceptada
                }
            }

            return estadosAceptacion.Contains(estadoActual); // Verifica si termina en un estado de aceptación
        }
    }
}
