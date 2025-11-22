using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

/*
    * Clase para validar la entrada del usuario al crear un nuevo tablero.
    * Verifica que el nombre del tablero no esté vacío y que haya al menos dos columnas.
    * También verifica que los nombres de las columnas no estén vacíos.

*/

namespace KanbanCLI
{
    public class Validate
    {
        // Este metodo valida que el dato no este vacio o solo tenga espacios
        public bool ValidarDatos(string datoAValidar)
        {   
            if (string.IsNullOrWhiteSpace(datoAValidar))
            {
                return false;
            }
            return true;
        }

        // Este metodo pide los datos para crear un nuevo tablero
        public (string nombre, List<string> columnas) PedirDatosTableroNuevo()
        {
            Console.Write("Ingrese el nombre del nuevo tablero: ");
            string inputNombre = Console.ReadLine() ?? string.Empty;
            // verifico que el nombre no este vacio o solo tenga espacios
            if (string.IsNullOrWhiteSpace(inputNombre))
            {
                Console.WriteLine("El nombre del tablero no puede estar vacío.");
                return (string.Empty, new List<string>());
            }
            Console.Write("Ingrese los nombres de las columnas separadas por comas (mínimo 2): ");
            string inputColumnas = Console.ReadLine() ?? string.Empty;
            List<string> columnas = new List<string>(inputColumnas.Split(','));
            // verifico que haya al menos dos columnas
            if (columnas.Count < 2)
            {
                Console.WriteLine("Debes ingresar al menos dos columnas.");
                return (string.Empty, new List<string>());
            }
            // verifico que ninguna columna este vacia o solo tenga espacios
            else
            {
                foreach (var columna in columnas)
                {
                    if (string.IsNullOrWhiteSpace(columna))
                    {
                        Console.WriteLine("Los nombres de las columnas no pueden estar vacíos.");
                        return (string.Empty, new List<string>());
                    }
                }
            }
            return (inputNombre, columnas);
        }
    }
}