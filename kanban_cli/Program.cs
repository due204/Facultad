using System;
using System.Reflection.Metadata;

namespace KanbanCLI
{
    class Program
    {
        static void Main(string[] args)
        {
            // Variable para controlar el bucle principal
            bool exit = false;
            // Crear una instancia de la clase Boards
            Boards nuevoTablero = new Boards();
            // Crear una instancia de la clase Validate
            Validate validarEntrada = new Validate();
            while (!exit)
            {
                string input;
                Console.Write("\nkanban_cli  > ");
                input = Console.ReadLine() ?? string.Empty;
                input = input.ToLower();
                if (!validarEntrada.ValidarDatos(input))
                {
                    // Si la entrada no es válida, mostrar un mensaje y continuar el bucle
                    Console.WriteLine("Entrada no válida. Por favor, inténtelo de nuevo.");
                    continue;
                }

                else if (input == "new")
                {
                    // var permite que el compilador infiera automáticamente el tipo de la variable según el valor asignado.
                    var (nombre, columnas) = validarEntrada.PedirDatosTableroNuevo();
                    if (!string.IsNullOrWhiteSpace(nombre) && columnas != null && columnas.Count >= 2)
                    {
                        nuevoTablero.CrearTablero(nombre, columnas);
                    }
                }

                else if (input == "help")
                {
                    Help.MostrarAyuda();
                    continue;
                }

                else if (input == "exit")
                {
                    exit = true;
                    continue;
                }

                else if (input == "clean")
                {
                    Console.Clear();
                    continue;
                }

                else if (input == "ls")
                {
                    Console.WriteLine("Listado de tableros existentes:");
                    foreach (string tablero in nuevoTablero.ListarTablero())
                    {
                        Console.WriteLine(tablero);
                    }
                    continue;
                }

                else if (input == "rm")
                {
                    Console.Write("Tableros disponibles: ");
                    foreach (string tablero in nuevoTablero.ListarTablero())
                    {
                        Console.Write(tablero + " | ");
                    }
                    Console.Write("\nIngrese el nombre del tablero a eliminar: ");
                    string nombreTableroEliminar = Console.ReadLine() ?? string.Empty;
                    if (!validarEntrada.ValidarDatos(nombreTableroEliminar))
                        continue;
                    if (!nuevoTablero.UsarTablero(nombreTableroEliminar))
                    {
                        Console.WriteLine($"El tablero {nombreTableroEliminar} no se encontró.");
                        continue;
                    }
                    // Confirmacion antes de eliminar
                    Console.Write($"¿Seguro que desea eliminar el tablero {nombreTableroEliminar}? (y/n): ");
                    string? confirm = Console.ReadLine()?.Trim().ToLower();
                    if (confirm != "y" && confirm != "yes")
                    {
                        Console.WriteLine("Operación cancelada.");
                        continue;
                    }
                    if (nuevoTablero.EliminarTablero(nombreTableroEliminar))
                    {
                        Console.WriteLine($"Tablero {nombreTableroEliminar} eliminado correctamente.");
                    }
                    else
                    {
                        Console.WriteLine("Hubo un problema al eliminar el tablero.");
                    }
                    continue;
                }

                else if (input == "use")
                {
                    Console.Write("Tableros disponibles: ");
                    foreach (string tablero in nuevoTablero.ListarTablero())
                    {
                        Console.Write(tablero + " | ");
                    }
                    Console.Write("\nIngrese el nombre del tablero a usar: ");
                    string nombreTableroUsar = Console.ReadLine() ?? string.Empty;
                    if (validarEntrada.ValidarDatos(nombreTableroUsar))
                    {
                        if (nuevoTablero.UsarTablero(nombreTableroUsar))
                        {
                            Console.WriteLine($"Usando el tablero {nombreTableroUsar}");
                            nuevoTablero.GestionarTableroActual(nombreTableroUsar);
                        }
                        else
                        {
                            Console.WriteLine($"El tablero {nombreTableroUsar} no se encontro.");
                        }
                    }
                    continue;
                }

                else
                {
                    Console.WriteLine($"kanban_cli: {input}: Orden no encontrada \nEscriba 'help' para ver los comandos disponibles.");
                }
            }
        }
    }
}