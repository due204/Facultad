using System;

namespace KanbanCLI
{
    public class Help
    {
        private const string Green = "\u001b[32m";
        private const string Reset = "\u001b[0m";

        public static void MostrarAyuda()
        {
            Console.WriteLine($"{Green}==== Ayuda General ===={Reset}");
            Console.WriteLine("1. clean - Limpiar el terminal");
            Console.WriteLine("2. exit - Salir de la aplicación");
            Console.WriteLine("3. help - Mostrar esta ayuda");
            Console.WriteLine("4. ls - Listar los tableros existentes");
            Console.WriteLine("5. new - Crear un nuevo tablero");
            Console.WriteLine("6. rm - Eliminar un tablero existente");
            Console.WriteLine("7. use - Usar un tablero existente");
            Console.WriteLine(" ");
            Console.WriteLine($"{Green}==== Ayuda dentro de los tableros ===={Reset}");
            Console.WriteLine("1. ad - <Tarea> - Añadir tarea");
            Console.WriteLine("2. cl - Limpiar el terminal");
            Console.WriteLine("3. ed - <Columna> <Tarea> <NuevoNombre> - Editar el nombre de una tarea");
            Console.WriteLine("4. ls - Listar tareas en el tablero actual");
            Console.WriteLine("5. mv - <Origen> <Tarea> <Destino> - Mover tarea a una columna específica");
            Console.WriteLine("6. rm - <Origen> <id> - Eliminar tarea");
            Console.WriteLine("7. q - Cerrar el tablero actual y volver al menú principal");
            Console.WriteLine(" ");

        }
    }
}