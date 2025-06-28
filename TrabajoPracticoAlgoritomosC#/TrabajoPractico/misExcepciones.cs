using System;               //
using System.Collections;    //
using SalonEventos.Salons;    //
using SalonEventos.Eventos;    //
using SalonEventos.Empleados;   //
using SalonEventos.Servicios;    //
using SalonEventos.Encargados;    //
using SalonEventos.EventoNuevo;    //
using SalonEventos.Excepciones;     //


using SalonEventos.ListaEventos; namespace SalonEventos.Excepciones
{
    public class MisException
    {
        // Exceptios para las clases.

        // Excepcion que se lanza si la fecha ya esta asignada a un evento
        public class FechaDeReservaException : Exception
        {
            public FechaDeReservaException() : base("Ya existe una reserva para esa fecha.") { }
            public FechaDeReservaException(string mensaje) : base(mensaje) { }
        }

        // Excepcion que se lanza si no se encontró el evento
        public class EventoNoEncontradoException : Exception
        {
            public EventoNoEncontradoException() : base("No se encontró el evento.") { }
            public EventoNoEncontradoException(string mensaje) : base(mensaje) { }
        }
        
        // Excepcion que si hay ur errror en la reserva
        public class EventoErrorDeReservaException : Exception
        {
            public EventoErrorDeReservaException() : base("Error al reservar evento.") { }
            public EventoErrorDeReservaException(string mensaje) : base(mensaje) { }
        }

        // Excepcion que se lanza si encuantro otro error
        public class OtroErrorEncontradoException : Exception
        {
            public OtroErrorEncontradoException() : base("Error.") { }
            public OtroErrorEncontradoException(string mensaje) : base(mensaje) { }
        }

        // Excepcion que se lanza si no se ingresa ningún valor que se esta solicitando
        public class NoIngredaDatoException : Exception
        {
            public NoIngredaDatoException() : base("No a ingresado ningun valor.") { }
            public NoIngredaDatoException(string mensaje) : base(mensaje) { }
        }
    }
}
