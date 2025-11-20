using System;
using System.Collections.Generic;
using System.Threading;
namespace TP_Programacion_Objetos
{
    public class Instituto
    {

        // Atributos
        private string nombreDeLaInstitucion = "Aprender+";
        private List<Docente> listaDocente;
        private List<Alumno> listaAlumnos;
        private List<Curso> listaCursos;

        // Propiedades

        public string NombreDeLaInstitucion
        {
            get { return nombreDeLaInstitucion; }
        }

        public List<Curso> ListaCursos
        {
            get { return listaCursos; }
            set { listaCursos = value; }
        }

        public List<Docente> ListaDocente
        {
            get { return listaDocente; }
            set { listaDocente = value; }
        }

        public List<Alumno> ListaAlumnos
        {
            get { return listaAlumnos; }
            set { listaAlumnos = value; }
        }

        // Metodos de busqueda
        // Busco un alumno por legajo devuelve null si no lo encuentra
        public Alumno BuscarAlumnoPorLegajo(int legajo)
        {
            foreach (Alumno alum in listaAlumnos)
            {
                if (alum.Legajo == legajo)
                {
                    return alum;
                }
            }
            return null;
        }

        // Busco un curso por nombre devuelve null si no lo encuentra.
        public Curso BuscarCursoPorNombre(string nombre)
        {
            foreach (Curso cur in listaCursos)
            {
                // Paso todo a minusculas para comparar
                if (cur.Nombre.ToLower() == nombre.ToLower())
                
                {
                    return cur;
                }
            }
            return null;
        }
            
        // Busco un docente por dni 
        public Docente BuscarDocentePorDni(string dni)
        {
            foreach (Docente doce in listaDocente)
            {
                if (doce.Dni == dni)
                {
                    return doce;
                }
            }
            return null;
        }

        // Verifico si un dni de alumno ya existe
        private bool ExisteDniAlumno(string dni)
        {
            foreach (Alumno alum in listaAlumnos)
            {
                if (alum.Dni == dni)
                {
                    return true;
                }
            }
            return false;
        }

        public void InscribirAlumnoEnCurso(int legajoAlumno, string nombreCurso)
        {
            Alumno alumno = BuscarAlumnoPorLegajo(legajoAlumno);
            if (alumno == null)
            {
                throw new AlumnoInexistenteException("El alumno con legajo " + legajoAlumno + " no existe.");
            }

            Curso curso = BuscarCursoPorNombre(nombreCurso);
            if (curso == null)
            {
                throw new CursoInexistenteException("El curso '" + nombreCurso + "' no existe.");
            }

            // Intento agregar alukmno al curso
            bool exito = curso.AgregarAlumnoCurso(alumno);
            if (!exito)
            {
                throw new AlumnoInscriptoException("El alumno ya está inscrito en este curso.");
            }

            // Si tengo exito en todo lo anterior agrego el curso a la lista de cursos del alumno
            alumno.Cursos.Add(curso); 
        }

        public bool EliminarAlumno(int legajoAlumno)
        {
            Alumno alumno = BuscarAlumnoPorLegajo(legajoAlumno);
            if (alumno == null)
            {
                return false;
            }

            // Saco de todos los cursos en los que este el alumno
            foreach (Curso cur in listaCursos)
            {
                cur.EliminarAlumnoCurso(alumno);
            }
            
            alumno.Cursos.Clear();

            // Lo saco de la lista principal del instituto
            listaAlumnos.Remove(alumno);
            return true;
        }

        public string EliminarAlumnoDeCursoYVerificar(int legajo, string nombreCurso)
        {
            // Valido los datos
            Alumno alumno = BuscarAlumnoPorLegajo(legajo);
            if (alumno == null)
            {
                throw new AlumnoInexistenteException("El alumno con legajo " + legajo + " no existe.");
            }

            Curso curso = BuscarCursoPorNombre(nombreCurso);
            if (curso == null)
            {
                throw new CursoInexistenteException("El curso '" + nombreCurso + "' no existe.");
            }

            // Inteno eliminarlo del curso
            if (!curso.EliminarAlumnoCurso(alumno))
            {
                // Si falla es porque el alumno no estaba en el curso
                throw new AlumnoInscriptoException("El alumno " + alumno.Apellido + " no estaba inscripto en " + curso.Nombre);
            }

            alumno.Cursos.Remove(curso);
            
            // Verifico si cursa en otro curso
            bool estaEnOtroCurso = false;
            
            // Recorro todos los cursos del instituto
            foreach (Curso cur in this.listaCursos)
            {
                // Recorro los alumnos de cada curso
                foreach (Alumno a in cur.Alumnos)
                {
                    // Si encuentro al alumno por legajo salgo del bucle
                    if (a.Legajo == alumno.Legajo)
                    {
                        estaEnOtroCurso = true;
                        break;
                    }
                }
                if (estaEnOtroCurso)
                {
                    // Si esta en otro curso salgo del bucle
                    break;
                }
            }

            // Decision final
            if (estaEnOtroCurso)
            {
                // Si sigue en otra materia solo retorno un string informando
                return "Alumno eliminado de " + curso.Nombre + " exitosamente.";
            }
            else
            {
                // Si no esta en otros cursos lo borro del instituto y retorno un string informando
                this.listaAlumnos.Remove(alumno);
                return "Alumno eliminado de " + curso.Nombre + " y dado de baja del instituto (no le quedaban más cursos).";
            }
        }
        public bool TransferirAlumno(int legajo, string cursoOrigenNombre, string cursoDestinoNombre)
        {
            Alumno alumno = BuscarAlumnoPorLegajo(legajo);
            Curso cursoOrigen = BuscarCursoPorNombre(cursoOrigenNombre);
            Curso cursoDestino = BuscarCursoPorNombre(cursoDestinoNombre);

            if (alumno == null || cursoOrigen == null || cursoDestino == null)
            {
                return false;
            }

            // Verifico si el alumno esta en el curso de origen
            bool estaEnOrigen = false;
            foreach (Alumno alum in cursoOrigen.Alumnos)
            {
                if (alum.Legajo == alumno.Legajo)
            {
                    // Si esta en el curso retorno true
                    estaEnOrigen = true;
                    break;
                }
            }
            
            if (!estaEnOrigen)
            {   
                // Si no esta en el curso retorno false
                return false;
            }

            try
            {
                // Inscribo al alumno en el curso de destino
                if (cursoDestino.AgregarAlumnoCurso(alumno))
                {
                    // Si tuvo exito lo elimino del viejo
                    cursoOrigen.EliminarAlumnoCurso(alumno);
                    alumno.Cursos.Remove(cursoOrigen);
                    alumno.Cursos.Add(cursoDestino);
                    return true;
                }
                else
                {
                    // Si paso algo malo retorno false
                    return false;

                }
            }

            catch (CupoLlenoException)
            {
                // Si el curso destino esta lleno al transferir
                // Capturo la excepcion y retorno false
                return false;
            }
        }

        public bool IngresarAlumnoNuevo(string nombre, string apellido, string dni, int legajo)
        {
            // Valido que el legajo no este repetido
            if (BuscarAlumnoPorLegajo(legajo) != null)
            {
                throw new AlumnoRepetidoException("Ya existe un alumno con el legajo " + legajo);
            }

            // Valido que el dni no este repetido
            if (ExisteDniAlumno(dni))
            {
                throw new AlumnoRepetidoException("Ya existe un alumno con el DNI " + dni);
            }

            try
            {
                Alumno nuevoAlumno = new Alumno(nombre, apellido, dni, legajo);
                listaAlumnos.Add(nuevoAlumno);
                return true;
            }
            catch (LegajoIncorrectoException)
            {
                throw;
            }
        }
            
        public bool RegistrarNota(int legajo, string nombreCurso, double nota)
        {
            Alumno alumno = BuscarAlumnoPorLegajo(legajo);
            if (alumno == null)
            {
                return false;
            }

            Curso curso = BuscarCursoPorNombre(nombreCurso);
            if (curso == null)
            {
                return false;
            }
            
            // Agrego una validacion de nota
            if (nota < 0.0 || nota > 10.0)
            {
                return false;
            }

            bool exito = curso.RegistrarNotaCurso(alumno, nota);

            if (!exito)
            {
                // Si el alumno existe pero no esta en ese curso
                return false;
            }
            return true;
        }

        // Walter este metodo lo movi desde Curso
        public List<Curso> CursosDadosPorUnDocente(string dniDocente)
        {
            Docente docente = BuscarDocentePorDni(dniDocente);
            if (docente != null)
            {
                // Si lo encuentro retorno la lista de cursos
                return docente.ObtenerCursosDados();
            }
            // En caso contrario retorno la lista vacia
            return new List<Curso>();
        }
            
        public List<Alumno> ListarAlumnos()
        {
            return listaAlumnos;
        }

        public List<Curso> ListarCursos()
        {
            return listaCursos;
        }

        public bool GuardarDatos()
        {
            return Dato.Guardar(this); 
        }

        public string MostrarDatos()
        {
            string datos = "--- Datos del Instituto " + nombreDeLaInstitucion + " ---" + "\n";

            datos += "Total Alumnos: " + listaAlumnos.Count + "\n";
            datos += "Total Docentes: " + listaDocente.Count + "\n";
            datos += "Total Cursos: "+ listaCursos.Count; 

            // Retorno el string
            return datos;
        }
            

        // Calculo promedio general de un alumno 
        public double CalcularPromedioGeneral(int legajo)
        {
            Alumno alumno = BuscarAlumnoPorLegajo(legajo);
            if (alumno == null || alumno.Notas.Count == 0)
            {
                return 0.0;
            }

            double suma = 0.0;
            //.Notas.Values es la coleccion de todos los "double" (las notas)
            foreach (double nota in alumno.Notas.Values)
            {
                suma += nota;
            }

            // Evito division por cero si la coleccion de notas estuviera vacia
            if (alumno.Notas.Count == 0)
            {
                return 0.0;
            }

            return suma / alumno.Notas.Count;
        }

        //listar alumnos de multiples cursos
        public List<Alumno> ListarAlumnosMultiplesCursos()
        {
            // Usamos un Diccionario para contar apariciones
            // Clave = Legajo del alumno
            // Valor = Cantidad de cursos en los que esta
            Dictionary<int, int> conteoPorLegajo = new Dictionary<int, int>();

            // Recorro todos los cursos
            foreach (Curso cur in this.listaCursos)
            {
                // Recorro los alumnos de cada curso
                foreach (Alumno alumno in cur.Alumnos)
                {
                    if (conteoPorLegajo.ContainsKey(alumno.Legajo))
                    {
                        // Si lo encuentro sumo 1
                        conteoPorLegajo[alumno.Legajo]++;
                    }
                    else
                    {
                        // Si es la primera vez que lo encuentro lo agrego con 1
                        conteoPorLegajo[alumno.Legajo] = 1;
                    }
                }
            }

            // Filtro los que tienen un conteo mayor a 1
            List<Alumno> alumnosEncontrados = new List<Alumno>();
            foreach (int legajo in conteoPorLegajo.Keys)
            {
                // La clave (el legajo) ahora buscamos su valor (el conteo) usando el diccionario.
                int conteo = conteoPorLegajo[legajo];

                // Hago la misma validacion pero con la variable conteo
                if (conteo > 1)
                {
                    // Busco al alumno usando la variable legajo
                    Alumno alumno = this.BuscarAlumnoPorLegajo(legajo);
                    if (alumno != null)
                    {
                        alumnosEncontrados.Add(alumno);
                    }
                }
            }
            // Retorno la lista de alumnos encontrados
            return alumnosEncontrados;
        }


         // Constructor si parametros necesario para la deserialización de JSON
        public Instituto()
        {
            listaDocente = new List<Docente>();
            listaAlumnos = new List<Alumno>();
            listaCursos = new List<Curso>();
        }

        // Constructor parametrizado
        public Instituto(string nombreInstitucion, bool inicializarDatosDePrueba)
        {
            nombreDeLaInstitucion = nombreInstitucion;

            // Inicializo las listas
            listaDocente = new List<Docente>();
            listaAlumnos = new List<Alumno>();
            listaCursos = new List<Curso>();

            if (inicializarDatosDePrueba)
            {
                /*
                    Datos de prueba para facilitar la evaluación del proyecto.
                    Se crean 10 alumnos, 10 docentes y 10 cursos.
                    Se asignan los cursos a los docentes y se agregan los alumnos a la lista del instituto.
                    Solo se carga la estructura basica sin inscribir alumnos en cursos ni asignar notas.
                    Y por ultimo solo se cargan si no se encutra el archivo JSON de datos.
                */
                // Depureishon xD
                Console.WriteLine("\nCargado datos de prueba y creando instituto_datos.json y tp.conf, espere.\n");
                Console.Write("");
                for (int i = 0; i < 65; i++)
                {
                    System.Threading.Thread.Sleep(70); 
                    Console.Write(">");
                }
                Console.WriteLine(" ¡Listo!\n");
                System.Threading.Thread.Sleep(1000);

                // Creo los alumnos
                Alumno alumno1 = new Alumno("Juan", "Perez", "20888333", 1001);
                Alumno alumno2 = new Alumno("Maria", "Gomez", "22999222", 1002);
                Alumno alumno3 = new Alumno("Luis", "Lopez", "19555777", 1003);
                Alumno alumno4 = new Alumno("Ana", "Martinez", "27777444", 1004);
                Alumno alumno5 = new Alumno("Carlos", "Sanchez", "23333111", 1005);
                Alumno alumno6 = new Alumno("Laura", "Diaz", "24444555", 1006);
                Alumno alumno7 = new Alumno("Jorge", "Ramirez", "21111222", 1007);
                Alumno alumno8 = new Alumno("Sofia", "Torres", "26666777", 1008);
                Alumno alumno9 = new Alumno("Diego", "Flores", "25555999", 1009);
                Alumno alumno10 = new Alumno("Marta", "Vega", "28888000", 1010);

                // Creo los docentes
                Docente docente1 = new Docente("Flavia", "Choren", "36000123", 1000);
                Docente docente2 = new Docente("Juan", "Garcia", "39444888", 1000);
                Docente docente3 = new Docente("Pedro", "Paso", "30111444", 1000);
                Docente docente4 = new Docente("Carolina", "Carrazco", "36999000", 1000);
                Docente docente5 = new Docente("Emiliano", "Segura", "35777888", 1000);
                Docente docente6 = new Docente("Vicente", "Arroyo", "49555666", 1000);
                Docente docente7 = new Docente("Daminan", "Pugliese", "28333444", 1000);
                Docente docente8 = new Docente("Patricia", "Garay", "60222333", 1000);
                Docente docente9 = new Docente("Noelia", "Basan", "36222111", 1000);
                Docente docente10 = new Docente("Sergio", "Besares", "37555666", 1000);

                // Creo los cursos
                Curso ProgramacionconObejetos = new Curso("Programacion con Objetos", docente1, 5);
                Curso Algebra = new Curso("Algebra I", docente2, 3);
                Curso MatematicaDiscreta = new Curso("Matematica discreta", docente3, 10);
                Curso BaseDatos = new Curso("Base de datos I", docente4, 7);
                Curso Historia = new Curso("Problematica de la hitoria argentina", docente5, 4);
                Curso TallerIngenieria = new Curso("Taller de ingeniaria", docente6, 11);
                Curso Ingles = new Curso("Ingles tecnico I", docente7, 9);
                Curso Electronica = new Curso("Electronica digital y microcontroladores", docente8, 2);
                Curso Calculo = new Curso("Calculo I", docente9, 10);
                Curso SistemasOperativos = new Curso("Sistemas operativos I", docente10, 9);

                // Vinculo los cursos con los docentes que los dan
                docente1.CursosDados.Add(ProgramacionconObejetos);
                docente2.CursosDados.Add(Algebra);
                docente3.CursosDados.Add(MatematicaDiscreta);
                docente4.CursosDados.Add(BaseDatos);
                docente5.CursosDados.Add(Historia);
                docente6.CursosDados.Add(TallerIngenieria);
                docente7.CursosDados.Add(Ingles);
                docente8.CursosDados.Add(Electronica);
                docente9.CursosDados.Add(Calculo);
                docente10.CursosDados.Add(SistemasOperativos);

                // Agrego los alumnos a la lista
                listaAlumnos.Add(alumno1);
                listaAlumnos.Add(alumno2);
                listaAlumnos.Add(alumno3);
                listaAlumnos.Add(alumno4);
                listaAlumnos.Add(alumno5);
                listaAlumnos.Add(alumno6);
                listaAlumnos.Add(alumno7);
                listaAlumnos.Add(alumno8);
                listaAlumnos.Add(alumno9);
                listaAlumnos.Add(alumno10);

                // Agrego los docentes a la lista
                listaDocente.Add(docente1);
                listaDocente.Add(docente2);
                listaDocente.Add(docente3);
                listaDocente.Add(docente4);
                listaDocente.Add(docente5);
                listaDocente.Add(docente6);
                listaDocente.Add(docente7);
                listaDocente.Add(docente8);
                listaDocente.Add(docente9);
                listaDocente.Add(docente10);

                // Agrego los cursos a la lista
                listaCursos.Add(ProgramacionconObejetos);
                listaCursos.Add(Algebra);
                listaCursos.Add(MatematicaDiscreta);
                listaCursos.Add(BaseDatos);
                listaCursos.Add(Historia);
                listaCursos.Add(TallerIngenieria);
                listaCursos.Add(Ingles);
                listaCursos.Add(Electronica);
                listaCursos.Add(Calculo);
                listaCursos.Add(SistemasOperativos);
            }
        }

    }
}