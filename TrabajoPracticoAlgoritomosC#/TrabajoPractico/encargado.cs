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
	
	

namespace SalonEventos.Encargados
{

    public class Encargado : Empleado{

        // Atributos del encargado
        private int plusEncargado;

        // Metodos
        public int EncargadoPlus
        {
            get { return plusEncargado; }
            set { plusEncargado = value; }
        }

        // Metodo para sobreescribir con override el plus del encargado
        public override int SueldoRecalculado()
        {
            return base.EmpleadoSueldo + plusEncargado;
        }


        public Encargado(string nombre, string apellido, int documento, int sueldo, string tarea, int plus) : base(nombre, apellido, documento, sueldo, tarea)
        {
            // Siempre, siempre, siempre, siempre, siempre el constructor lleva el nombre de la clase
            this.EncargadoPlus = plus;
            this.EmpleadoSueldo = SueldoRecalculado();
        }
    }
}
