using System;

namespace TP_Programacion_Objetos
{
    public class Persona
    {
        // Atributos
        protected string nombre;
        protected string apellido;
        protected string dni;

        // Getters y Setters de los atributos
        public string Nombre
        {
            get { return nombre; }
            set { nombre = value; }
        }

        public string Apellido
        {
            get { return apellido; }
            set { apellido = value; }
        }

        public string Dni
        {
            get { return dni; }
            set { dni = value; }
        }

        // Constructor si parametros necesario para la deserialización de JSON
        public Persona()
            {
                nombre = "";
                apellido = "";
                dni = "";
            }

        // Constructor parametrizado
        public Persona(string nombre, string apellido, string dni)
        {
            this.nombre = nombre;
            this.apellido = apellido;
            this.dni= dni;
        }
    }
}