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

namespace SalonEventos.EventoNuevo

{
    // Esta clase es para crear tipos de eventos nuevos
    public class TipoEventoNuevo
    {
        // Atributos
        private string nombreEventoNuevo;
        private double precioEventoNuevo;

        // Mertodos
        public string EventoNuevoNombre
        {
            get { return nombreEventoNuevo; }
            set { nombreEventoNuevo = value; }
        }
        public double EventoNuevoPrecio
        {
            get { return precioEventoNuevo; }
            set { precioEventoNuevo = value; }
        }

        public TipoEventoNuevo(string nombre, double precio)
        {
            // Siempre, siempre, siempre, siempre, siempre el constructor lleva el nombre de la clase
            this.nombreEventoNuevo = nombre;
            this.precioEventoNuevo = precio;
        }
    }

}

namespace SalonEventos.ListaEventos
{
    public class ListaDeEventos
    {
        // atributos
        private ArrayList tiposDeEventos;

        // Metodos
        public ArrayList EventosTipos
        {
            get { return tiposDeEventos; }
        }

        public void AgregarTipoEventoNuevo(string nombre, double valorEnPesos)
        {
            // El valor es en pesos por que si cobro en dolares nadie va a cotratar a mi salon :(
            tiposDeEventos.Add(new TipoEventoNuevo(nombre, valorEnPesos));
        }

        public ListaDeEventos()
        {
            // Siempre, siempre, siempre, siempre, siempre el constructor lleva el nombre de la clase
            // Inicializo tiposDeEventos vacia y luego asigno algunos eventos para tener por defecto
            tiposDeEventos = new ArrayList();
            tiposDeEventos.Add(new TipoEventoNuevo("Casamiento", 100000));
            tiposDeEventos.Add(new TipoEventoNuevo("Cumpleaños", 80000));
            tiposDeEventos.Add(new TipoEventoNuevo("Cumpleaño de 15", 99000));
            tiposDeEventos.Add(new TipoEventoNuevo("Cumpleaños tematicos", 87000));
            tiposDeEventos.Add(new TipoEventoNuevo("Bautismo", 69000));
            tiposDeEventos.Add(new TipoEventoNuevo("Comunion", 60000));
            tiposDeEventos.Add(new TipoEventoNuevo("Fiesta de egresados", 68000));
            tiposDeEventos.Add(new TipoEventoNuevo("Aniversario de bodas", 70000));
            tiposDeEventos.Add(new TipoEventoNuevo("Seminarios", 50000));
        }
    }
}

