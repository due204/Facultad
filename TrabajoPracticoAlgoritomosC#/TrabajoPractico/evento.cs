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

namespace SalonEventos.Eventos
{
    public class Evento
    {
        // Atributos
        private string nombreCliente;
        private int dniCliente;
        private DateTime fechaYHora;
        private string tipoEvento;
        private Encargado encargadoEvento;
        private ArrayList serviciosEvento;
        private double adelantoEvento;
        private double precioBaseTipoEvento;

        // Metodos
        public string ClienteNombre
        {
            get { return nombreCliente; }
            set { nombreCliente = value; }
        }
        public int ClienteDni
        {
            get { return dniCliente; }
            set { dniCliente = value; }
        }
        public DateTime FechaHora
        {
            get { return fechaYHora; }
            set { fechaYHora = value; }
        }
        public string TipoDeEvento
        {
            get { return tipoEvento; }
            set { tipoEvento = value; }
        }
        public Encargado EncargadoAsignado
        {
            get { return encargadoEvento; }
            set { encargadoEvento = value; }
        }
        public double AdelantoDado
        {
            get { return adelantoEvento; }
            set { adelantoEvento = value; }
        }
        public double PrecioBase
        {
            get { return precioBaseTipoEvento; }
            set { precioBaseTipoEvento = value; }
        }


        // Agregar un servicio nuevo a la lista de servicios contratados
        public void AgregarServicio(Servicio servi)
        {
            serviciosEvento.Add(servi);
        }

        // Retorno costos totales por los servicios contratados
        public double CostoTotal()
        {
            double total = precioBaseTipoEvento;
            foreach (Servicio serv in serviciosEvento)
                total += serv.ServicioCostoTotal();
            return total;
        }

        public Evento(string nomb, int dni, DateTime fecha, string tipo, Encargado encar, double ade, double preBa)
        {
            // Siempre, siempre, siempre, siempre, siempre el constructor lleva el nombre de la clase
            this.nombreCliente = nomb;
            this.dniCliente = dni;
            this.fechaYHora = fecha;
            this.tipoEvento = tipo;
            this.encargadoEvento = encar;
            this.adelantoEvento = ade;
            this.precioBaseTipoEvento = preBa;
            this.serviciosEvento = new ArrayList();
        }
    }
}
