/*
 * Created by VIM.
 * User: Due204
 * Date: 26/5/2025
 * Time: 13:30
 * 
 */
using System;               //
using System.Collections;    //
using SalonEventos.Salons;    //
using SalonEventos.Eventos;    //
using SalonEventos.Empleados;   //
using SalonEventos.Servicios;    //
using SalonEventos.Encargados;    //
using SalonEventos.EventoNuevo;    //
using SalonEventos.Excepciones;     //
using SalonEventos.ListaEventos;     //


namespace SalonEventos
{
    class Tp
    {
        static void Main(string[] args)
        {
            Salon salon = new Salon();
            //ListaDeEventos listaEventos = new ListaDeEventos();
            Console.WriteLine("Trabajo Práctico: Salón de Eventos\n");

            bool salirMenu = false;
            while (!salirMenu)
            {
                Console.WriteLine("A - Agregar servicio");
                Console.WriteLine("B - Eliminar servicio");
                Console.WriteLine("C - Alta de empleado/encargado");
                Console.WriteLine("D - Baja de empleado/encargado");
                Console.WriteLine("E - Reservar evento");
                Console.WriteLine("F - Cancelar evento");
                Console.WriteLine("G - Imprimir listados");
                Console.WriteLine("H - Salir");

                string opcion = Console.ReadLine();

                switch (opcion)
                {
                    case "a":
                        AgregarServicio(salon);
                        break;
                    case "b":
                        EliminarServicio(salon);
                        break;
                    case "c":
                        AltaEmpleado(salon);
                        break;
                    case "d":
                        BajaEmpleado(salon);
                        break;
                    case "e":
                        ReservarEvento(salon);
                        break;
                    case "f":
                        CancelarEvento(salon);
                        break;
                    case "g":
                        SubMenuImpresion(salon);
                        break;
                    case "H":
                        salirMenu = true;
                        break;
                    default:
                        Console.WriteLine("Opción inválida.");
                        break;
                }
            }
        }

        static void AgregarServicio(Salon salon)
        {
            Console.WriteLine("Ingrese los siguientes datos\n\n");
            Console.WriteLine("Nombre del servicio:");
            string nombreServicio = Console.ReadLine();

            Console.WriteLine("Descripción del servicio:");
            string descripcionServicio = Console.ReadLine();

            Console.WriteLine("Cantidad de servicios:");
            int cantidadServicio = int.Parse(Console.ReadLine());

            Console.WriteLine("Costo unitario del servicio:");
            double costoServicio = double.Parse(Console.ReadLine());

            Servicio nuevo = new Servicio(nombreServicio, descripcionServicio, cantidadServicio, costoServicio);
            salon.AgregarServicioNuevo(nuevo);

            Console.WriteLine("Servicio agregado exitosamente.");
        }

        static void EliminarServicio(Salon salon)
        {
            Console.WriteLine("Ingrese nombre del servicio a eliminar:");
            string nombre = Console.ReadLine();

            Servicio aEliminar = null;
            foreach (Servicio s in salon.SalonServicios)
            {
                if (s.ServicioNombre == nombre)
                {
                    aEliminar = s;
                    break;
                }
            }

            if (aEliminar != null)
            {
                salon.SalonServicios.Remove(aEliminar);
                Console.WriteLine("Servicio eliminado.");
            }
            else
            {
                Console.WriteLine("Servicio no encontrado.");
            }
        }

        static void AltaEmpleado(Salon salon)
        {
            Console.WriteLine("Agregar empleado (1) encargado (2)?");
            string rta = Console.ReadLine();

            while (rta == "1" || rta == "2")
            {
                Console.WriteLine("Ingrese el nombre: ");
                string nombre = Console.ReadLine();
                Console.WriteLine("Ingrese el apellido: ");
                string apellido = Console.ReadLine();
                Console.WriteLine("Ingrese el DNI: ");
                int dni = int.Parse(Console.ReadLine());
                Console.WriteLine("Ingrese un sueldo:");
                int sueldo = int.Parse(Console.ReadLine());

                if (rta == "1")
                {
                    Console.WriteLine("Ingrese una tarea:");
                    string tarea = Console.ReadLine();
                    Empleado empNuevo = new Empleado(nombre, apellido, dni, sueldo, tarea);
                    salon.AgregarEmpleadoNuevo(empNuevo);
                    Console.WriteLine("Empleado agregado.");
                }
                else
                {
                    string tarea = "Encargado";
                    Console.WriteLine("Ingrese el plus del encargado:");
                    int plus = int.Parse(Console.ReadLine());
                    Encargado encNuevo = new Encargado(nombre, apellido, dni, sueldo, tarea, plus);
                    salon.AgregarEmpleadoNuevo(encNuevo);
                    Console.WriteLine("Encargado agregado.");
                }

                Console.WriteLine("Agregar otro (1)empleado (2)encargado o presione otra tecla para salir)");
                rta = Console.ReadLine();
            }
        }

        static void BajaEmpleado(Salon salon)
        {
            Console.WriteLine("Ingrese DNI del empleado a eliminar:");
            int dni = int.Parse(Console.ReadLine());

            salon.EliminarEmpleado(dni);
            Console.WriteLine("Empleado eliminado si existía.");
        }

        static void ReservarEvento(Salon salon)
        {
            Console.WriteLine("Ingrese nombre del cliente:");
            string nombre = Console.ReadLine();

            Console.WriteLine("Ingrese DNI del cliente:");
            int dni = int.Parse(Console.ReadLine());

            Console.WriteLine("Ingrese tipo de evento:");
            string tipo = Console.ReadLine();

            Console.WriteLine("Ingrese fecha del evento (yyyy-mm-dd):");
            DateTime fecha = DateTime.Parse(Console.ReadLine());

            Console.WriteLine("Ingrese monto de la seña:");
            double seña = double.Parse(Console.ReadLine());

            // Buscar un encargado disponible
            Encargado encargado = null;
            foreach (Empleado e in salon.SalonEmpleados)
            {
                if (e is Encargado)
                {
                    encargado = (Encargado)e;
                    break;
                }
            }

            if (encargado == null)
            {
                Console.WriteLine("No hay encargados disponibles.");
                return;
            }

            Evento nuevoEvento = new Evento(nombre, dni, fecha, tipo, encargado, seña);

            Console.WriteLine("¿Cuántos servicios desea agregar al evento?");
            int cant = int.Parse(Console.ReadLine());

            for (int i = 0; i < cant; i++)
            {
                Console.WriteLine("Nombre del servicio:");
                string nombreServ = Console.ReadLine();
                Servicio serv = null;

                foreach (Servicio s in salon.SalonServicios)
                {
                    if (s.ServicioNombre == nombreServ)
                    {
                        serv = s;
                        break;
                    }
                }

                if (serv != null)
                    nuevoEvento.AgregarServicio(serv);
                else
                    Console.WriteLine("Servicio no encontrado.");
            }

            try
            {
                salon.ReservarEvento(nuevoEvento);
                Console.WriteLine("Reserva realizada correctamente.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al reservar: {ex.Message}");
            }
        }

        static void CancelarEvento(Salon salon)
        {
            Console.WriteLine("Ingrese fecha del evento a cancelar (yyyy-mm-dd):");
            DateTime fecha = DateTime.Parse(Console.ReadLine());

            Console.WriteLine("Ingrese DNI del cliente:");
            int dni = int.Parse(Console.ReadLine());

            try
            {
                salon.CancelarEvento(fecha, dni);
                Console.WriteLine("Evento cancelado correctamente.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al cancelar evento: {ex.Message}");
            }
        }

        static void SubMenuImpresion(Salon salon)
        {
            Console.WriteLine("Submenú de impresión:");
            Console.WriteLine("1 - Listado de empleados");
            Console.WriteLine("2 - Listado de eventos");
            Console.WriteLine("3 - Listado de eventos por mes");

            string op = Console.ReadLine();

            switch (op)
            {
                case "1":
                    salon.ListarEmpleados();
                    break;
                case "2":
                    foreach (Evento e in salon.SalonEventos)
                        Console.WriteLine($"{e.ClienteNombre} - {e.TipoDeEvento} - {e.FechaHora.ToShortDateString()}");
                    break;
                case "3":
                    Console.WriteLine("Ingrese número del mes (1-12):");
                    int mes = int.Parse(Console.ReadLine());
                    foreach (Evento e in salon.SalonEventos)
                        if (e.FechaHora.Month == mes)
                            Console.WriteLine($"{e.ClienteNombre} - {e.TipoDeEvento} - {e.FechaHora.ToShortDateString()}");
                    break;
                default:
                    Console.WriteLine("Opción inválida.");
                    break;
            }
        }
    }
}


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
        public virtual void EmpleadoListar()
        {
            Console.WriteLine($"Empleado: {nombreEmpleado} {apellidoEmpleado} - DNI: {dniEmpleado} - Sueldo: {sueldoEmpleado} - Tarea: {tareaEmpleado}");
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

        // Agregar un servicio nuevo a la lista de servicios contratados
        public void AgregarServicio(Servicio servi)
        {
            serviciosEvento.Add(servi);
        }

        // Retorno costos totales por los servicios contratados
        public double CostoTotal()
        {
            double total = 0;
            foreach (Servicio serv in serviciosEvento)
                total += serv.ServicioCostoTotal();
            return total;
        }

        public Evento(string nomb, int dni, DateTime fecha, string tipo, Encargado encar, double ade)
        {
            // Siempre, siempre, siempre, siempre, siempre el constructor lleva el nombre de la clase
            this.nombreCliente = nomb;
            this.dniCliente = dni;
            this.fechaYHora = fecha;
            this.tipoEvento = tipo;
            this.encargadoEvento = encar;
            this.adelantoEvento = ade;
            this.serviciosEvento = new ArrayList();
        }
    }
}

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
            tiposDeEventos.Add(new TipoEventoNuevo("Cumpleaño de 15", 88000));
            tiposDeEventos.Add(new TipoEventoNuevo("Bautismo", 69000));
            tiposDeEventos.Add(new TipoEventoNuevo("Comunion", 60000));
        }
    }
}

namespace SalonEventos.Salons

{
    public class Salon : MisException
    {
        // Atributos
        private ArrayList empleadosSalon;
        private ArrayList serviciosSalon;
        private ArrayList eventosSalon;

        // Metodos
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

        public Salon()
        {
            // Siempre, siempre, siempre, siempre, siempre el constructor lleva el nombre de la clase
            empleadosSalon = new ArrayList();
            serviciosSalon = new ArrayList();
            eventosSalon = new ArrayList();
        }

        // Agregar un nuevo empleado (o encargado) al arraylist de empleados
        public void AgregarEmpleadoNuevo(Empleado empleadoAgregado)
        {
            empleadosSalon.Add(empleadoAgregado);
        }

        // Eliminar un empleado (o encargado) por su dni
        public void EliminarEmpleado(int dni)
        {
            foreach (Empleado emple in empleadosSalon)
            {
                if (emple.EmpleadoDni == dni)
                {
                    empleadosSalon.Remove(emple);
                    break;
                }
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
        public void ReservarEvento(Evento eventoReservar)
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
            eventosSalon.Remove(eventoaCancelar);
        }
    }
}

namespace SalonEventos.Excepciones
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

        // Excepcion que se lanza si no se ingresa ningún valor que se esta solicitando
        public class NoIngredaDatoException : Exception
        {
            public NoIngredaDatoException() : base("No a ingresado ningun valor.") { }
            public NoIngredaDatoException(string mensaje) : base(mensaje) { }
        }
    }
}
