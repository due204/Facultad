using System;
using System.IO;
using Newtonsoft.Json;

namespace TP_Programacion_Objetos
{
    // Clase estatica para guardar y cargar datos del instituto
    public static class Dato
    {
        // Retorno la ruta de la db desde un archivo de configuracion
        private static string GetRuta()
        {
            // Nombre del archivo de configuracion
            string archivoConf = "tp.conf";
            // Ruta por defecto si no se encuentra en el archivo de configuracion
            // Lo copie de stackoverflow
            string rutaPorDefecto = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "instituto_datos.json");
            string linea;
            bool encontrada = false;

            // Si el archivo no existe lo creo
            if (!File.Exists(archivoConf))
            {
                using (StreamWriter sw = new StreamWriter(archivoConf))
                {
                    sw.WriteLine("{0}", "DB_JSON:" + rutaPorDefecto);
                }
                return rutaPorDefecto;
            }

            // Si existe lo leo línea por línea
            using (StreamReader sr = new StreamReader(archivoConf))
            {
            	while ((linea = sr.ReadLine()) != null)
                {
                    if (linea.StartsWith("DB_JSON:"))
                    {
                        string[] partes = linea.Split(new char[] { ':' }, 2);
                        if (partes.Length == 2)
                        {
                            encontrada = true;
                            return partes[1].Trim();
                        }
                    }
                }
            }

            // Si no encuentro la ruta de json la agrego
            if (!encontrada)
            {
                using (StreamWriter sw = new StreamWriter(archivoConf, true))
                {
                    sw.WriteLine("{0}", "DB_JSON:" + rutaPorDefecto);
                }
            }

            return rutaPorDefecto;
        }

        // Ruta del archivo donde se guardaran los datos
        private static readonly string rutaArchivo = GetRuta();


        // Configuracion de json adaptada a Newtonsoft.Json por que 
        //   arranque el proyecto usando Text.Json
        private static readonly JsonSerializerSettings opciones = new JsonSerializerSettings
        {
            Formatting = Formatting.Indented, PreserveReferencesHandling = PreserveReferencesHandling.Objects
        };

        // Metodo estatico para guardar los datos (reescrito)
        public static bool Guardar(Instituto instituto)
        {
            try
            {
                // Use JsonConvert.SerializeObject
                string json = JsonConvert.SerializeObject(instituto, opciones);
                File.WriteAllText(rutaArchivo, json);
                // Si guarda corectamente devuelvo true
                return true;
            }
            catch (Exception)
            {
                // Si no se guarda correctamente devuelvo false
                return false;
            }
        }

        // Metodo estatico para cargar los datos (reescrito)
        public static Instituto Cargar()
        {
            try
            {
                if (!File.Exists(rutaArchivo))
                {
                    var inst = new Instituto("Aprender+", true);
                    // Guarda los datos de prueba iniciales
                    Guardar(inst);
                    return inst;
                }

                string json = File.ReadAllText(rutaArchivo);
                if (string.IsNullOrWhiteSpace(json))
                {
                    var inst = new Instituto("Aprender+", false);
                    Guardar(inst);
                    return inst;
                }

                Instituto instituto = JsonConvert.DeserializeObject<Instituto>(json, opciones);
                
                if (instituto == null)
                {
                    var inst = new Instituto("Aprender+", false);
                    Guardar(inst);
                    return inst;
                }

                return instituto;
            }
            // Como habia muchas excepciones posibles por caputrar opte por capturar todo
            catch (Exception ex)
            {
                Console.WriteLine("Error al cargar datos: " + ex.Message);
                return new Instituto("Aprender+", false);
            }
        }

    }
}
