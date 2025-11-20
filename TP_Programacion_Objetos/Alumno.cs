using System;
using System.Collections.Generic;

namespace TP_Programacion_Objetos
{
    public class Alumno : Persona
    {
        // Atributos
        private List<Curso> cursos;
        private Dictionary<string, double> notas;
        private int legajo;
        
        // Getters y Setters de los atributos
        public List<Curso> Cursos
        {
            get { return cursos; }
            set { cursos = value; }
        }
        public Dictionary<string, double> Notas
        {
            get { return notas; }
            set { notas = value; }
        }
        public int Legajo
        {
            get { return legajo; }
            set
            {
                if (value <= 1000)
                {
                    throw new LegajoIncorrectoException("El legajo debe ser mayor a 1000.");
                }
                legajo = value;
            }
        }

        // Constructor si parametros necesario para la deserialización de JSON
        public Alumno() : base()
        {
            this.cursos = new List<Curso>();
            this.notas = new Dictionary<string, double>();
        }

        // Constructor parametrizado
        public Alumno(string nombre, string apellido, string dni, int legajo) : base(nombre, apellido, dni)
        {
            this.Legajo = legajo;
            this.cursos = new List<Curso>();
            // Inicializamos el diccionario de notas
            this.notas = new Dictionary<string, double>();
        }
    }
}