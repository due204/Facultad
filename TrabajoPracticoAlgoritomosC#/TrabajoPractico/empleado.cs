using System;               //
using System.Collections;    //
using SalonEventos.Salons;    //
using SalonEventos.Eventos;    //
using SalonEventos.Empleados;   //
using SalonEventos.Servicios;    //
using SalonEventos.Encargados;    //
using SalonEventos.EventoNuevo;    //
using SalonEventos.Excepciones;     //
using SalonEventos.ListaEventos; 

namespace SalonEventos.Empleados
{
    public class Empleado
    {
        // Atributos del empleado
        private string nombreEmpleado;
        private string apellidoEmpleado;
        private int dniEmpleado;
        private int sueldoEmpleado;
        private string tareaEmpleado;

        // Metodos
        public string EmpleadoNombre
        {
            get { return nombreEmpleado; }
            set { nombreEmpleado = value; }
        }

        public string EmpleadoApellido
        {
            get { return apellidoEmpleado; }
            set { apellidoEmpleado = value; }
        }

        public int EmpleadoDni
        {
            get { return dniEmpleado; }
            set { dniEmpleado = value; }
        }

        public int EmpleadoSueldo
        {
            get { return sueldoEmpleado; }
            set { sueldoEmpleado = value; }
        }

        public string EmpleadoTarea
        {
            get { return tareaEmpleado; }
            set { tareaEmpleado = value; }
        }

        // Metodo para sobreescribir con override el plus del encargado
        public virtual int SueldoRecalculado()
        {
            return EmpleadoSueldo;
        }

        // Metodo para listar empleados
        public void EmpleadoListar()
        {
            Console.WriteLine("Empleado: " + nombreEmpleado + " " + apellidoEmpleado + " - DNI: " + dniEmpleado + " - Sueldo: " + sueldoEmpleado + " - Tarea: " + tareaEmpleado);
        }


        public Empleado(string nombre, string apellido, int documento, int sueldo, string tarea)
        {
            // Siempre, siempre, siempre, siempre, siempre el constructor lleva el nombre de la clase
            this.nombreEmpleado = nombre;
            this.apellidoEmpleado = apellido;
            this.dniEmpleado = documento;
            this.sueldoEmpleado = sueldo;
            this.sueldoEmpleado = SueldoRecalculado();
            this.tareaEmpleado = tarea;
        }
    }
}