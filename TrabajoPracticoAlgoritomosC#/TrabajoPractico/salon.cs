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

namespace SalonEventos.Salons

{
    public class Salon : MisException
    {
        // Atributos
        private string nombreDelSalon;
        private ArrayList empleadosSalon;
        private ArrayList serviciosSalon;
        private ArrayList eventosSalon;

        // Metodos
        public string SalonNombre
        {
            get { return nombreDelSalon; }
            set { nombreDelSalon = value; }
        }
        public ArrayList SalonEmpleados
        {
            get { return empleadosSalon; }
        }
        public ArrayList SalonEventos
        {
            get { return eventosSalon; }
        }
        public ArrayList SalonServicios
        {
            get { return serviciosSalon; }
        }

        // Agregar un nuevo empleado (o encargado) al arraylist de empleados
        public void AgregarEmpleadoNuevo(Empleado empleadoAgregado)
        {
            empleadosSalon.Add(empleadoAgregado);
        }

        // Eliminar un empleado (o encargado) por su dni
        public void EliminarEmpleado(int dni)
        {
            bool eliminadoDni = false;
            foreach (Empleado emple in empleadosSalon)
            {
                if (emple.EmpleadoDni == dni)
                {
                    empleadosSalon.Remove(emple);
                    Console.WriteLine("Empleado eliminado");
                    eliminadoDni = true;
                    break;
                }
            }
            if (!eliminadoDni)
            {
                Console.WriteLine("DNI: " + dni + " no encontrado en la lista de empleados");    
            }
        }

        // Listar empleados
        public void ListarEmpleados()
        {
            foreach (Empleado emple in empleadosSalon)
            {
                emple.EmpleadoListar();
            }
        }



        // Agregar servicio nuevo
        public void AgregarServicioNuevo(Servicio servi)
        {
            serviciosSalon.Add(servi);
        }

        // Reservar un evento
        public void ReservaEvento(Evento eventoReservar)
        {
            foreach (Evento even in eventosSalon)
                if (even.FechaHora.Date == eventoReservar.FechaHora.Date)
                    throw new FechaDeReservaException();
            eventosSalon.Add(eventoReservar);
        }

        // Eliminar un evento
        public void CancelarEvento(DateTime fecha, int dniCliente)
        {
            Evento eventoaCancelar = null;
            foreach (Evento even in eventosSalon)
                if (even.FechaHora == fecha && even.ClienteDni == dniCliente)
                {
                    eventoaCancelar = even;
                    break;
                }
            if (eventoaCancelar == null)
                throw new EventoNoEncontradoException();
            TimeSpan diferenciaTiempo = eventoaCancelar.FechaHora - DateTime.Now;
            if (diferenciaTiempo.TotalDays > 30)
            {
                Console.WriteLine("Se cancelo el evento con mas de un mes de anticipacion y no se reintegra la seña.");
            }
            else
            {
                double total = eventoaCancelar.CostoTotal();
                double adeuda = total - eventoaCancelar.AdelantoDado;
                Console.WriteLine("Cancelación con menos de un mes: el cliente debe abonar el total.");
                Console.WriteLine("Costo total: $" + total +" - Seña: $" + eventoaCancelar.AdelantoDado + " - Restan: $" + adeuda);
            }
            eventosSalon.Remove(eventoaCancelar);

        }

        public Salon(string nombresalon)
        {
            // Siempre, siempre, siempre, siempre, siempre el constructor lleva el nombre de la clase
            this.nombreDelSalon = nombresalon;
            empleadosSalon = new ArrayList();
            serviciosSalon = new ArrayList();
            eventosSalon = new ArrayList();
        }

        public void InicializarPorDefecto()
        {
            empleadosSalon.Add(new Encargado("Ana", "García", 11111111, 300000, "Encargado", 50000));
            empleadosSalon.Add(new Encargado("Luis", "Martínez", 22222222, 310000, "Encargado", 55000));
            empleadosSalon.Add(new Encargado("Sofía", "Rodríguez", 33333333, 290000, "Encargado", 48000));
            empleadosSalon.Add(new Encargado("Pedro", "Fernández", 44444444, 305000, "Encargado", 52000));
            empleadosSalon.Add(new Encargado("Elena", "López", 55555555, 320000, "Encargado", 60000));
            empleadosSalon.Add(new Encargado("Carlos", "Díaz", 66666666, 295000, "Encargado", 49000));
            empleadosSalon.Add(new Encargado("Marta", "Pérez", 77777777, 300000, "Encargado", 50000));
            empleadosSalon.Add(new Empleado("Juan", "Gómez", 88888888, 180000, "Preparación de salón"));
            empleadosSalon.Add(new Empleado("María", "Sánchez", 99999999, 175000, "Limpieza y mantenimiento"));
            empleadosSalon.Add(new Empleado("Pablo", "Ramírez", 10101010, 190000, "Montaje de equipamiento"));
            empleadosSalon.Add(new Empleado("Laura", "Torres", 12121212, 185000, "Asistencia en cocina"));
            empleadosSalon.Add(new Empleado("Diego", "Flores", 13131313, 170000, "Recepción de invitados"));
            empleadosSalon.Add(new Empleado("Valeria", "Castro", 14141414, 180000, "Atención en mesas"));
            empleadosSalon.Add(new Empleado("Andrés", "Vargas", 15151515, 175000, "Servicio de barra"));
            empleadosSalon.Add(new Empleado("Camila", "Herrera", 16161616, 190000, "Manejo de sonido e iluminación"));
            empleadosSalon.Add(new Empleado("Javier", "Morales", 17171717, 180000, "Seguridad"));
            empleadosSalon.Add(new Empleado("Daniela", "Jiménez", 18181818, 175000, "Asistencia general"));
            empleadosSalon.Add(new Empleado("Ricardo", "Ruiz", 19191919, 180000, "Mantenimiento eléctrico"));
            empleadosSalon.Add(new Empleado("Natalia", "Silva", 20202020, 170000, "Apoyo en decoración"));
            empleadosSalon.Add(new Empleado("Fernando", "Acosta", 21212121, 185000, "Asistencia técnica"));
            // Servicios para el Salón
            serviciosSalon.Add(new Servicio("Catering", "Servicio de comida completo con opciones de menú", 1, 8000));
            serviciosSalon.Add(new Servicio("DJ Profesional", "Música variada y equipo de sonido de alta calidad", 1, 2500));
            serviciosSalon.Add(new Servicio("Animadores", "Entretenimiento para invitados de todas las edades", 1, 1500));
            serviciosSalon.Add(new Servicio("Fotografía y Video", "Cobertura completa del evento con entrega digital y álbum", 1, 2500));
            serviciosSalon.Add(new Servicio("Barra de Tragos", "Barra libre con bartender y cócteles variados", 1, 2200));
            serviciosSalon.Add(new Servicio("Música en Vivo", "Banda o solista para un ambiente único y personalizado", 1, 3000));
            serviciosSalon.Add(new Servicio("Iluminación Decorativa", "Luces ambientales y efectos especiales para el salón", 1, 1500));
            serviciosSalon.Add(new Servicio("Decoración Temática", "Diseño y montaje de la decoración según la temática del evento", 1, 1200));
            serviciosSalon.Add(new Servicio("Servicio de Seguridad", "Personal de seguridad para control de acceso y vigilancia", 2, 800));
            serviciosSalon.Add(new Servicio("Personal Adicional", "Mozos, ayudantes de cocina y personal de apoyo", 5, 1000));
        }
    }
}
