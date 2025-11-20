using System;
using System.Collections.Generic;
using System.Threading;

// 

namespace TP_Programacion_Objetos 
{
    class Program
    {
        public static Instituto miInstituto;
        static int opcion = 0; 

        public static void Main(string[] args)
        {
            miInstituto = Dato.Cargar();
            if (miInstituto == null)
            {
                return;
            }
            Console.Write("\nSistema de gestión " + miInstituto.NombreDeLaInstitucion + " cargado.");
            System.Threading.Thread.Sleep(1000);

            do
            {
                MostrarMenu(); 
                
                int opcionSeleccionada = 0;
                bool entradaValida = false;

                for (int intento = 1; intento <= 3; intento++)
                {
                    string entrada = Console.ReadLine() ?? "";
                    
                    if (int.TryParse(entrada, out opcionSeleccionada))
                    {
                       
                        if (opcionSeleccionada >= 1 && opcionSeleccionada <= 10)
                        {
                            entradaValida = true;
                            break;
                        }
                    }
                    
                    if (intento < 3)
                    {
                        
                        Console.WriteLine("Error: Opción no válida. Por favor, ingrese un número entre 1 y 10.");
                        Console.Write("Seleccione una opción: ");
                    }
                }
                
                if (!entradaValida)
                {
                    Console.WriteLine("\nSe superó el número de intentos. El programa se cerrará.");
                    opcion = 10; 
                }
                else
                {
                    opcion = opcionSeleccionada;
                }
                
                if (entradaValida)
                {
                   
                    switch (opcion)
                    {
                        case 1:
                            ManejarInscribirAlumno();
                            break;
                        case 2:
                            ManejarEliminarAlumno();
                            break;
                        case 3:
                            ManejarRegistrarNota();
                            break;
                        case 4:
                            ManejarMostrarPromedioCursos();
                            break;
                        case 5:
                            ManejarListarAlumnosPorCurso();
                            break;
                        case 6:
                            ManejarListarCursos();
                            break;
                        case 7:
                            ManejarListarAlumnosMultiplesCursos();
                            break;
                        case 8: 
                            ManejarTransferirAlumno();
                            break;
                        case 9: 
                            ManejarSalir();
                            break;
                        case 10: 
                            Console.WriteLine("Saliendo sin guardar. !OJO¡");
                            System.Threading.Thread.Sleep(1000);
                            break;
                    }
                    
                    if (opcion != 10) 
                    {
                        Console.WriteLine("\nPresione una tecla para continuar...");
                        Console.ReadKey();
                    }
                }

            } while (opcion != 10);

            Console.WriteLine("\nEl programa ha finalizado. Se cerrar la ventana.");
            System.Threading.Thread.Sleep(2000);
        }

        private static void MostrarMenu()
        {
            Console.Clear();
            Console.WriteLine("\n \t\tMENÚ DE OPCIONES ");
            Console.WriteLine("\t1-  Inscribir alumno en un curso");
            Console.WriteLine("\t2-  Eliminar alumno de un curso");
            Console.WriteLine("\t3-  Registrar nota de examen"); 
            Console.WriteLine("\t4-  Mostrar promedio general de cada curso"); 
            Console.WriteLine("\t5-  Mostrar alumnos por curso");
            Console.WriteLine("\t6-  Lista de cursos");
            Console.WriteLine("\t7-  Listar alumnos inscriptos en más de un curso"); 
            Console.WriteLine("\t8-  Transferir un alumno de un curso a otro"); 
            Console.WriteLine("\t9-  Guardar");
            Console.WriteLine("\t10- Salir sin guardar\n");
            Console.Write("Seleccione una opción: ");
        }
        
        public static string LeerEntrada(string texto)
        {
            while (true)
            {
                Console.Write(texto + ": ");
                string entrada = Console.ReadLine();
                if (entrada != null)
                {
                	entrada = entrada.Trim();
				}
                else
                {
                	entrada = "";
                }
                if (!string.IsNullOrEmpty(entrada))
                    return entrada;

                Console.WriteLine("Este campo no puede estar vacío.\n");
            }
        }

        static void ManejarInscribirAlumno()
		{
		    try
		    {
		        int legajo = int.Parse(LeerEntrada("Ingrese el legajo del alumno"));
		        if (legajo <= 1000)
		        {
		            throw new LegajoIncorrectoException("El legajo debe ser mayor a 1000.");
		        }
		        
		        Alumno alumno = miInstituto.BuscarAlumnoPorLegajo(legajo);
		        
		        if (alumno == null)
                {
                    Console.WriteLine("El alumno con legajo " + legajo + " no existe.");
                    Console.WriteLine("Se procederá a darlo de alta.");
		            string nombre = LeerEntrada("Ingrese nombre del nuevo alumno");
                    string apellido = LeerEntrada("Ingrese apellido del nuevo alumno");
                    string dni = LeerEntrada("Ingrese DNI del nuevo alumno");
                    miInstituto.IngresarAlumnoNuevo(nombre, apellido, dni, legajo);
                    Console.WriteLine("\nAlumno nuevo ingresado exitosamente");
		        }
		        else
		        {
		            Console.WriteLine("\nAlumno encontrado: " + alumno.Nombre + " " + alumno.Apellido);
		        }
		        
		        Curso cursoSeleccionado = SeleccionarCursoDesdeLista();
		        if (cursoSeleccionado == null)
		        {
		            Console.WriteLine("No se selecciono ningún curso.");
		            return;
		        }
		        
		        miInstituto.InscribirAlumnoEnCurso(legajo, cursoSeleccionado.Nombre);
		        
		        
		        Console.WriteLine("\n¡Inscripción al curso exitosa!");
		        
		    }
		    catch (Exception ex) 
		    {
		       
		        Console.WriteLine("\nError en la inscripción: " + ex.Message);
		       
		    }
		}
        
        static void ManejarEliminarAlumno()
		{
		    try
		    {
		        int legajo = int.Parse(LeerEntrada("Ingrese el legajo del alumno a eliminar"));
		        		
		        Curso cursoSeleccionado = SeleccionarCursoDesdeLista();
		        if (cursoSeleccionado == null)
		        {
		            Console.WriteLine("No se selecciono ningun curso.");
		            return;
		        }
		
		        string mensajeResultado = miInstituto.EliminarAlumnoDeCursoYVerificar(legajo, cursoSeleccionado.Nombre);
			        
		        Console.WriteLine("\n" + mensajeResultado);
		    }
		    catch (Exception ex)
		    {
		        // Esto captura "Alumno no existe", "Curso no existe"
		        // o "Alumno no estaba en ese curso".
		       
		        Console.WriteLine("\nError al eliminar: " + ex.Message);
		       
		    }
		}
    
        static void ManejarRegistrarNota()
        {
            try
            {
                int legajo = int.Parse(LeerEntrada("Ingrese el legajo del alumno"));
                Curso cursoSeleccionado = SeleccionarCursoDesdeLista();
                if (cursoSeleccionado == null)
                {
                    throw new Exception("No se seleccionó ningún curso.");
                }
                double nota = double.Parse(LeerEntrada("Ingrese la nota (ej: 7 o 7,5)"));
                miInstituto.RegistrarNota(legajo, cursoSeleccionado.Nombre, nota);
                Console.WriteLine("\nNota registrada exitosamente");
            }
            catch (Exception ex)
            {
                Console.WriteLine("\nError al registrar nota: " + ex.Message);
            }
        }

        static void ManejarMostrarPromedioCursos()
        {
            List<Curso> cursos = miInstituto.ListarCursos();
            if (cursos.Count == 0)
            {
                Console.WriteLine("No hay cursos registrados para calcular promedios.");
                return;
            }
            Console.WriteLine("\n--- Promedio General por Curso ---");
            foreach (Curso c in cursos)
            {
                double promedio = c.CalcularPromedioCurso();
                Console.WriteLine("Curso: " + c.Nombre + " | Promedio General: " + promedio);
            }
        }

        static void ManejarListarAlumnosPorCurso()
		{
		    Curso cursoSeleccionado = SeleccionarCursoDesdeLista();
		    if (cursoSeleccionado == null)
		    {
		        Console.WriteLine("No se seleccionó ningún curso.");
		        return;
		    }
		    Console.WriteLine("\n\t Alumnos en " + cursoSeleccionado.Nombre + " ---");
		    List<Alumno> alumnosEnCurso = cursoSeleccionado.Alumnos;
		    if (alumnosEnCurso.Count == 0)
		    {
		        Console.WriteLine("No hay alumnos inscritos en este curso.");
		        return;
		    }
		    foreach (Alumno a in alumnosEnCurso)
		    {
		        string notaParaMostrar = "Sin nota";
                if (a.Notas != null)
		        {
                    if (a.Notas.ContainsKey(cursoSeleccionado.Nombre))
		            {
                        double notaNumerica = a.Notas[cursoSeleccionado.Nombre];
		                notaParaMostrar = notaNumerica.ToString();
		            }
		       }
		
		    Console.WriteLine("Legajo: " + a.Legajo + " |\n\t DNI: " + a.Dni + " |\n\t Nombre: " + a.Nombre + " " + a.Apellido + "|\n\t Nota: " + notaParaMostrar);
		}
		}
             
        static void ManejarListarCursos()
        {
            List<Curso> cursos = miInstituto.ListarCursos();
            if (cursos.Count == 0)
            {
                Console.WriteLine("No hay cursos registrados.");
                return;
            }
            Console.WriteLine("\n--- Lista de Cursos ---");
            foreach (Curso c in cursos)
            {
                Console.WriteLine("Curso: " + c.Nombre + " | Docente: " + c.Docente.Apellido + " | Cupo: " + c.Alumnos.Count + "/" + c.CupoMaximoAlumnos);
            }
        }

        static void ManejarListarAlumnosMultiplesCursos()
        {
            List<Alumno> alumnos = miInstituto.ListarAlumnosMultiplesCursos();
            if (alumnos.Count == 0)
            {
                Console.WriteLine("\nNo se encontraron alumnos inscriptos en más de un curso.");
                return;
            }

            Console.WriteLine("\n--- Alumnos en Múltiples Cursos ---");
            foreach (Alumno a in alumnos)
            {
                Console.WriteLine("Legajo: " + a.Legajo + "| DNI: " + a.Dni + "| Nombre: " + a.Nombre + " " + a.Apellido);
            }
        }

        static void ManejarTransferirAlumno()
        {
            try
            {
                int legajo = int.Parse(LeerEntrada("Ingrese el legajo del alumno a transferir"));
                Console.WriteLine("\nSeleccione el CURSO DE ORIGEN:");
                Curso cursoOrigen = SeleccionarCursoDesdeLista();
                if (cursoOrigen == null)
                {
                    throw new Exception("No se seleccionó curso de origen.");
                }
                Console.WriteLine("\nSeleccione el CURSO DE DESTINO:");
                Curso cursoDestino = SeleccionarCursoDesdeLista();
                if (cursoDestino == null)
                {
                    throw new Exception("No se seleccionó curso de destino.");
                }
                bool exito = miInstituto.TransferirAlumno(legajo, cursoOrigen.Nombre, cursoDestino.Nombre);
                if (exito)
                {    
                     Console.WriteLine("\n¡Alumno transferido exitosamente!");    
                }
                else
                {
                    throw new Exception("No se pudo completar la transferencia (verifique los datos).");
                }
            }
            catch (CupoLlenoException ex) 
            {
                Console.WriteLine("\nError: " + ex.Message);  
            }
            catch (Exception ex)
            {
                Console.WriteLine("\nError: " + ex.Message);   
            }
        }

        static void ManejarSalir()
        {
            Console.WriteLine("Guardando datos...");
            if (miInstituto.GuardarDatos())
            {
                Console.WriteLine("Datos guardados exitosamente.");
            }
            else
            {
                Console.WriteLine("Error al guardar los datos.");
            }
        }
        
        static Curso SeleccionarCursoDesdeLista()
        {
            List<Curso> cursos = miInstituto.ListarCursos();
            if (cursos.Count == 0)
            {
                Console.WriteLine("No hay cursos disponibles.");
                return null;
            }
            Console.WriteLine("Cursos disponibles:");
            for (int i = 0; i < cursos.Count; i++)
            {
                Console.WriteLine("  " + i + " - " + cursos[i].Nombre + " " + cursos[i].Alumnos.Count + "/" + cursos[i].CupoMaximoAlumnos + " cupos");
            }

            while (true)
            {
                int numeroSeleccionado = int.Parse(LeerEntrada("Seleccione un numero de curso (o 0 para cancelar)"));
                if (numeroSeleccionado == 0)
                {
                    return null;
                }
                if (numeroSeleccionado >= 1 && numeroSeleccionado <= cursos.Count)
                {
                    return cursos[numeroSeleccionado - 1];
                }
            }    
        }
    }
}