using System;
using System.Collections.Generic;

namespace TP_Programacion_Objetos
{
    public class Curso
    {
        // Atributos
        private string nombreCurso;
        private Docente docenteACargo;
        private int cupoMaximoAlumnos;
        private List<Alumno> alumnos;

        // Getters y Setters de los atributos

        public string Nombre
        {
            get { return nombreCurso; }
            set { nombreCurso = value; }
        }

        public Docente Docente
        {
            get { return docenteACargo; }
            set { docenteACargo = value; }
        }

        public int CupoMaximoAlumnos
        {
            get { return cupoMaximoAlumnos; }
            set { cupoMaximoAlumnos = value; }
        }

        public List<Alumno> Alumnos
        {
            get { return alumnos; }
            set { alumnos = value; }
        }

        // Metodos de la clase curso
        public bool AgregarAlumnoCurso(Alumno alumno)
        {
            // Verifico que alumno no sea nulo
            if (alumno == null)
            {
                return false;
            }

            // Verifico si ya esta inscrito por legajo
            for (int i = 0; i < alumnos.Count; i++)
            {
                if (alumnos[i].Legajo == alumno.Legajo)
                {
                    return false;
                }
            }

            // Verificar cupo
            if (alumnos.Count >= cupoMaximoAlumnos)
            {
                // Si el cupo esta lleno lanzo la excepcion
                throw new CupoLlenoException("El cupo del curso esta lleno.");
            }
            // Si pasa todas las verificaciones lo agrego
            alumnos.Add(alumno);
            return true;
        }

        // Eliminr un alumno del curso por el legajo
        public bool EliminarAlumnoCurso(Alumno alumno)
        {
            // Verifico que alumno no sea nulo
            if (alumno == null)
            {
                return false;
            }

            for (int i = alumnos.Count - 1; i >= 0; i--)
            {
                if (alumnos[i].Legajo == alumno.Legajo)
                {
                    alumnos.RemoveAt(i);
                    return true;
                }
            }
            return false;
        }

        public bool RegistrarNotaCurso(Alumno alumno, double nota)
        {
            // Verifico que alumno no sea nulo
            if (alumno == null)
            {
                return false;
            }

            // Verifico que el alumno este inscripto en este curso por por legajo
            bool encontrado = false;
            for (int i = 0; i < alumnos.Count; i++)
            {
                if (alumnos[i].Legajo == alumno.Legajo)
                {
                    encontrado = true;
                    break;
                }
            }

            if (!encontrado)
            {
                return false;
            }

            // Me aseguro que la coleccion de notas exista
            if (alumno.Notas == null)
            {
                alumno.Notas = new Dictionary<string, double>();
            }

            // Valido rango de notas
            alumno.Notas[nombreCurso] = nota;
            return true;
        }

        public double CalcularPromedioCurso()
        {
            double suma = 0.0;
            int cuenta = 0;

            // Iteramos sobre los alumnos inscritos en este curso
            foreach (Alumno a in this.alumnos)
            {
                // Verificamos si el alumno tiene una nota registrada para este curso
                if (a.Notas != null && a.Notas.ContainsKey(this.nombreCurso))
                {
                    suma += a.Notas[this.nombreCurso];
                    cuenta++;
                }
            }
            // Evitar división por cero si no hay notas
            if (cuenta == 0)
            {
                return 0.0;
            }

            double promedio = suma / cuenta;
            return promedio;
        }

        // Constructor si parametros necesario para la deserialización de JSON
        public Curso()
        {
            this.alumnos = new List<Alumno>();
            this.nombreCurso = "";
        }

        // Constructor parametrizado
        public Curso(string nombre, Docente docente, int maxAlumnos)
        {
            this.nombreCurso = nombre;
            this.docenteACargo = docente;
            this.cupoMaximoAlumnos = maxAlumnos;
            this.alumnos = new List<Alumno>();
        }  

    }
}
