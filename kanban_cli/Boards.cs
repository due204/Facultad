using System;
using System.IO;
using System.Collections.Generic;
using System.Text.RegularExpressions;

/*
Esta clase se encarga de gestionar los tableros.
Proporciona métodos para crear, guardar, listar y eliminar tableros.
Todavia no decido en que formato de salida guardar los datos de los tableros (txt, scv, db).
*/



namespace KanbanCLI
{
    public class Boards
    {
        // Códigos de colores ANSI para la consola
        // Todabia no los he usado mucho pero los dejo aquí por si acaso
        private const string Reset = "\u001b[0m";
        private const string Red = "\u001b[31m";
        private const string Green = "\u001b[32m";
        private const string Yellow = "\u001b[33m";
        private const string Blue = "\u001b[34m";
        private const string Magenta = "\u001b[35m";
        private const string Cyan = "\u001b[36m";
        private const string White = "\u001b[37m";

        // Ruta del archivo donde se guardaran los tableros.
        private string rutaArchivo = "/home/due204/Cs/kambas_cli2/tableros.txt";
        // Nombre del tablero.
        private string nombreTablero = string.Empty;
        // Lista de nombres de las columnas del tablero.
        private List<string> nombresColumnas = new List<string>();
        // nombre de los datos del tablero
        private string datosTablero = string.Empty;



        public string NombreTablero
        {
            get { return nombreTablero; }
            set { nombreTablero = value; }
        }

        public List<string> NombresColumnas
        {
            get { return nombresColumnas; }
            set { nombresColumnas = value; }
        }

        public string DatosTablero
        {
            get { return datosTablero; }
            set { datosTablero = value; }
        }


        public void CrearTablero(string nombre, List<string> columnas)
        {
            // Esta metodo va a crear un nuevo tablero
            string rutaTableros = rutaArchivo;
            using (StreamWriter sw = new StreamWriter(rutaTableros, true))
            {
                sw.WriteLine($"{nombre}[");
                foreach (var columna in columnas)
                {
                    sw.WriteLine($"{columna}{{}}");
                }
                sw.WriteLine("]\n");
            }
        }

        public Dictionary<string, List<string>> CargarTablero(string nombreTablero)
        {
            Dictionary<string, List<string>> columnas = new Dictionary<string, List<string>>();

            string contenido = File.ReadAllText(rutaArchivo);

            string patron = $@"{Regex.Escape(nombreTablero)}\[(.*?)\]";
            var match = Regex.Match(contenido, patron, RegexOptions.Singleline);

            if (!match.Success){
                return columnas;
            }
            string cuerpo = match.Groups[1].Value.Trim();
            foreach (string linea in cuerpo.Split('\n'))
            {
                if (string.IsNullOrWhiteSpace(linea)) 
                    continue;

                var m = Regex.Match(linea, @"(.*?)\{(.*?)\}");
                if (m.Success)
                {
                    string nombreColumna = m.Groups[1].Value.Trim();
                    string tareasRaw = m.Groups[2].Value.Trim();

                    List<string> tareas = new List<string>();

                    if (!string.IsNullOrWhiteSpace(tareasRaw))
                        tareas.AddRange(tareasRaw.Split(';'));

                    columnas[nombreColumna] = tareas;
                }
            }
            return columnas;
        }

        public void MostrarContenidoTablero(string nombreTablero, Dictionary<string, List<string>> columnas)
        {
            Console.WriteLine($"{Yellow}{nombreTablero}{Reset}:");

            int numColumna = 1;

            foreach (var col in columnas)
            {
                Console.WriteLine($"{Red}{numColumna}{Reset} - {Cyan}[{col.Key}]{Reset}");

                for (int i = 0; i < col.Value.Count; i++)
                {
                    Console.WriteLine($"    {Red}{i + 1}{Reset}. {col.Value[i]}");
                }

                Console.WriteLine();
                numColumna++;
            }
        }

        public List<string> ListarTablero()
        {
            // Esta metodo va a retornar una lista los tableros existentes
            using (StreamReader archivoLeido = new StreamReader(rutaArchivo))
            {
                // el ? permite que la variable pueda ser nula
                string? linea;
                List<string> listaRetornar = new List<string>();
                while ((linea = archivoLeido.ReadLine()) != null)
                {
                    if (linea.EndsWith("["))
                    {
                        // Usar Split para separar por el carácter '['
                        string[] partes = linea.Split('[');
                        listaRetornar.Add(partes[0]);
                    }
                }
                return listaRetornar;
            }
        }

        public bool EliminarTablero(string nombreTableroEliminar)
        {
            foreach (string nombre in ListarTablero())
            {
                // El tablero existe procedo a eliminarlo
                if (nombre == nombreTableroEliminar)
                {
                    string textoCompleto = File.ReadAllText(rutaArchivo);
                    // Uso de expresiones regulares para encontrar y eliminar el bloque del tablero
                    string patron = $@"{Regex.Escape(nombreTableroEliminar)}\[[\s\S]*?\]\s*";
                    string resultado = Regex.Replace(textoCompleto, patron, "");
                    bool eliminado = textoCompleto != resultado;
                    using (StreamWriter sobreEscribiendo = new StreamWriter(rutaArchivo, false))
                    {
                        sobreEscribiendo.Write(resultado);
                    }
                    return eliminado;
                }
            }
        // Si no encontro el tablero, retornar false
        return false;
        }

        public bool UsarTablero(string nombreTableroUsar)
        {
            foreach (string nombre in ListarTablero())
            {
                // El tablero existe procedo a usarlo
                if (nombre == nombreTableroUsar)
                {
                    return true;
                }
            }
            // Si no encontro el tablero, retornar false
            return false;
        }

        public void GuardarTablero(string nombreTablero, Dictionary<string, List<string>> columnas)
        {
            string contenido = File.ReadAllText(rutaArchivo);

            // Construir el nuevo bloque del tablero
            string nuevoBloque = $"{nombreTablero}[\n";
            foreach (var col in columnas)
            {
                string tareas = string.Join(";", col.Value);
                nuevoBloque += $"{col.Key}{{{tareas}}}\n";
            }
            nuevoBloque += "]\n\n";

            // Quitar el bloque viejo
            string patron = $@"{Regex.Escape(nombreTablero)}\[[\s\S]*?\]\s*";
            string reemplazo = Regex.Replace(contenido, patron, "");

            // Agregar el bloque nuevo al final
            reemplazo += nuevoBloque;

            File.WriteAllText(rutaArchivo, reemplazo);
        }


        public void GestionarTableroActual(string nombreTablero)
        {
            var columnas = CargarTablero(nombreTablero);

            if (columnas.Count == 0)
            {
                Console.WriteLine("No se pudo cargar el tablero.");
                return;
            }

            // While para gestionar el tablero
            while (true)
            {
                Console.Write($"{Yellow}{nombreTablero}{Reset} $ ");
                string? input = Console.ReadLine();

                if (input == null) continue;
                input = input.ToLower();

                if (input == "q")
                {
                    break;
                }

                else if (input == "help")
                {
                    Help.MostrarAyuda();
                    continue;
                }

                else if (input == "ls")
                {
                    MostrarContenidoTablero(nombreTablero, columnas);
                    continue;
                }

                else if (input == "cl")
                {
                    Console.Clear();
                    continue;
                }

                else if (input.StartsWith("ad "))
                {
                    string tarea = input.Substring(3).Trim();
                    if (string.IsNullOrWhiteSpace(tarea))
                    {
                        Console.WriteLine($"{Magenta}Debe escribir una tarea.{Reset}");
                        continue;
                    }
                    // Agregar siempre a la primera columna
                    string primeraColumna = columnas.Keys.First();
                    columnas[primeraColumna].Add(tarea);
                    GuardarTablero(nombreTablero, columnas);
                    Console.WriteLine("Tarea agregada con éxito.");
                }

                else if (input.StartsWith("mv "))
                {
                    string[] partes = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                    if (partes.Length != 4)
                    {
                        Console.WriteLine($"{Magenta}Uso correcto: mv <ColumnaOrigen> <Tarea> <ColumnaDestino>{Reset}");
                        Console.WriteLine($"{Magenta}Ejemplo: mv 1 2 4 (mueve de la columna 1 la tares 2 a la columna 4){Reset}");
                        continue;
                    }
                    int colOrigen;
                    int idTarea;
                    int colDestino;
                    try
                    {
                        colOrigen = int.Parse(partes[1]);
                        idTarea = int.Parse(partes[2]);
                        colDestino = int.Parse(partes[3]);
                    }
                    catch(FormatException)
                    {
                        Console.WriteLine($"{Magenta}Todos los parámetros deben ser números.{Reset}");
                        continue;
                    }
                    catch(OverflowException)
                    {
                        Console.WriteLine($"{Magenta}Número fuera de rango.{Reset}");
                        continue;
                    }
                    if (colOrigen <= 0 || idTarea <= 0 || colDestino <= 0)
                    {
                        Console.WriteLine($"{Magenta}Los números deben ser mayores que cero.{Reset}");
                        continue;
                    }
                    var columnasOrdenadas = columnas.Keys.ToList();
                    if (colOrigen > columnasOrdenadas.Count || colDestino > columnasOrdenadas.Count)
                    {
                        Console.WriteLine($"{Magenta}La columna de origen o destino no existe.{Reset}");
                        continue;
                    }
                    string nombreColOrigen = columnasOrdenadas[colOrigen - 1];
                    string nombreColDestino = columnasOrdenadas[colDestino - 1];
                    var listaOrigen = columnas[nombreColOrigen];
                    if (idTarea > listaOrigen.Count)
                    {
                        Console.WriteLine($"{Magenta}Esa tarea no existe en la columna origen.{Reset}");
                        continue;
                    }
                    string tarea = listaOrigen[idTarea - 1];
                    listaOrigen.RemoveAt(idTarea - 1);
                    columnas[nombreColDestino].Add(tarea);
                    GuardarTablero(nombreTablero, columnas);
                    Console.WriteLine($"Tarea movida de {nombreColOrigen} a {nombreColDestino}.");
                }

                else if (input.StartsWith("ed "))
                {
                    // Dividimos SOLO las tres primeras partes (ed, col, id)
                    string[] partes = input.Split(' ', 4, StringSplitOptions.RemoveEmptyEntries);
                    if (partes.Length < 4)
                    {
                        Console.WriteLine($"{Magenta}Uso correcto: ed <numColumna> <idTarea> <nuevo texto>{Reset}");
                        Console.WriteLine($"{Magenta}Ejemplo: ed 1 2 Nueva descripcion (De la columna 1 renombra la tarea 2).{Reset}");
                        continue;
                    }
                    int colOrigen;
                    int idTarea;
                    try
                    {
                        colOrigen = int.Parse(partes[1]);
                        idTarea  = int.Parse(partes[2]);
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine($"{Magenta}Los parámetros deben ser números.{Reset}");
                        continue;
                    }
                    catch (OverflowException)
                    {
                        Console.WriteLine($"{Magenta}Número fuera de rango.{Reset}");
                        continue;
                    }
                    if (colOrigen <= 0 || idTarea <= 0)
                    {
                        Console.WriteLine($"{Magenta}Los números deben ser mayores que cero.{Reset}");
                        continue;
                    }
                    var columnasOrdenadas = columnas.Keys.ToList();
                    if (colOrigen > columnasOrdenadas.Count)
                    {
                        Console.WriteLine($"{Magenta}La columna indicada no existe.{Reset}");
                        continue;
                    }
                    string nombreColumna = columnasOrdenadas[colOrigen - 1];
                    var lista = columnas[nombreColumna];
                    if (idTarea > lista.Count)
                    {
                        Console.WriteLine($"{Magenta}Esa tarea no existe en la columna indicada.{Reset}");
                        continue;
                    }
                    string textoNuevo = partes[3].Trim();
                    if (string.IsNullOrWhiteSpace(textoNuevo))
                    {
                        Console.WriteLine($"{Magenta}El texto nuevo no puede estar vacío.{Reset}");
                        continue;
                    }
                    string textoViejo = lista[idTarea - 1];
                    // Confirmacion antes de editar
                    Console.Write($"¿Editar la tarea {textoViejo} por {textoNuevo}? (y/n): ");
                    string? confirm = Console.ReadLine()?.Trim().ToLower();

                    if (confirm != "y" && confirm != "yes")
                    {
                        Console.WriteLine("Operación cancelada.");
                        continue;
                    }
                    lista[idTarea - 1] = textoNuevo;
                    GuardarTablero(nombreTablero, columnas);
                    Console.WriteLine("Tarea editada correctamente.");
                }

                else if (input.StartsWith("rm "))
                {
                    string[] partes = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                    if (partes.Length != 3)
                    {
                        Console.WriteLine($"{Magenta}Uso correcto: rm <Columna> <Tarea>{Reset}");
                        Console.WriteLine($"{Magenta}Ejemplo: rm 1 2 (De la columna 1 eliminar la tarea 2).{Reset}");
                        continue;
                    }
                    int colOrigen;
                    int idTarea;
                    try
                    {
                        colOrigen = int.Parse(partes[1]);
                        idTarea  = int.Parse(partes[2]);
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine($"{Magenta}Ambos parámetros deben ser números.{Reset}");
                        continue;
                    }
                    catch (OverflowException)
                    {
                        Console.WriteLine($"{Magenta}Número fuera de rango.{Reset}");
                        continue;
                    }
                    if (colOrigen <= 0 || idTarea <= 0)
                    {
                        Console.WriteLine($"{Magenta}Los números deben ser mayores que cero.{Reset}");
                        continue;
                    }
                    // Obtener columnas ordenadas
                    var columnasOrdenadas = columnas.Keys.ToList();

                    if (colOrigen > columnasOrdenadas.Count)
                    {
                        Console.WriteLine($"{Magenta}La columna indicada no existe.{Reset}");
                        continue;
                    }
                    string nombreColumna = columnasOrdenadas[colOrigen - 1];
                    var lista = columnas[nombreColumna];
                    if (idTarea > lista.Count)
                    {
                        Console.WriteLine($"{Magenta}Esa tarea no existe en la columna indicada.{Reset}");
                        continue;
                    }
                    string tareaBorrada = lista[idTarea - 1];
                    // Confirmacion antes de eliminar
                     Console.Write($"¿Seguro que desea eliminar la tarea {tareaBorrada}? (y/n): ");
                    string? confirm = Console.ReadLine()?.Trim().ToLower();
                    if (confirm != "y" && confirm != "yes")
                    {
                        Console.WriteLine("Operación cancelada.");
                        continue;
                    }
                    // Eliminar la tarea
                    lista.RemoveAt(idTarea - 1);
                    // Guardar cambios
                    GuardarTablero(nombreTablero, columnas);
                    Console.WriteLine($"Tarea {tareaBorrada} eliminada.");
                }

                else
                {
                    Console.WriteLine($"Comando desconocido: {input}");
                    Console.WriteLine("Use: ad <Tarea>, cl, ed <Col> <Tarea> <TextoNuevo>, ls, mv <ColOrig> <Tarea> <ColDest>, rm <Col> <Tarea>, q");
                }
            }
        }


        public Boards()
        {
            /* 
            Por ahora en el constructor solo verifico si 
            el archivo donde se guardaran los tableros existe,
            si no existe, lo crea.
            */
            // Verifico si el erchivo no existe.
            if (!File.Exists(rutaArchivo))
            {
                // Si no existe lo crea y lo cierra inmediatamente.
                using (File.Create(rutaArchivo)) { }
            }
        }
    }
}