using System;
using System.Collections.Generic;


namespace TP_Programacion_Objetos
{
    public class Docente : Persona
    {
        // Atributos
        private List<Curso> cursosDados;
        private double sueldo;

        // Getters y Setters de los atributos
        public List<Curso> CursosDados
        {
            get { return cursosDados; }
            set { cursosDados = value; }
        }

        public double Sueldo
        {
            get { return sueldo; }
            set { sueldo = value; }
        }

        public List<Curso> ObtenerCursosDados()
        {
            return cursosDados;
        }

        // Constructor si parametros necesario para la deserialización de JSON
        public Docente() : base()
        {
            cursosDados = new List<Curso>();
        }

        // Constructor parametrizado
        public Docente(string nombre, string apellido, string dni, double sueldo) : base(nombre, apellido, dni)
        {
            this.sueldo = sueldo;
            cursosDados = new List<Curso>();
        }
    }
}