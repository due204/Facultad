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

namespace SalonEventos.Servicios

{
    public class Servicio
    {
        // Atributos
        private string nombreServicio;
        private string descripcionServicio;
        private int cantidadServicio;
        private double costoUnitarioServicio;

        // Metodos
        public string ServicioNombre
        {
            get { return nombreServicio; }
            set { nombreServicio = value; }
        }
        public string ServicioDescripcion
        {
            get { return descripcionServicio; }
            set { descripcionServicio = value; }
        }
        public int ServicioCantidad
        {
            get { return cantidadServicio; }
            set { cantidadServicio = value; }
        }
        public double ServicioCostoUnitario
        {
            get { return costoUnitarioServicio; }
            set { costoUnitarioServicio = value; }
        }

        // Retorno el costo total 
        public double ServicioCostoTotal()
        {
            return cantidadServicio * costoUnitarioServicio;
        }

        public Servicio(string nom, string desc, int cant, double costo)
        {
            // Siempre, siempre, siempre, siempre, siempre el constructor lleva el nombre de la clase
            this.nombreServicio = nom;
            this.descripcionServicio = desc;
            this.cantidadServicio = cant;
            this.costoUnitarioServicio = costo;
        }
    }
}
