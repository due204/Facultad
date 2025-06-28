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
    class Tp : MisException
    {
        static void Main(string[] args)
        {
            // Creo el salon y le paso su nombre
            Salon salon = new Salon("Trabajo Práctico: Salón de Eventos re copado loco\n");
            salon.InicializarPorDefecto();
        
            Console.WriteLine(salon.SalonNombre);

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

                string opcion = Console.ReadLine().ToLower();

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
                    case "h":
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
            string cantidadServicio = Console.ReadLine();

            Console.WriteLine("Costo unitario del servicio:");
            string costoServicio = Console.ReadLine();

            // paso todo por strin para verificar que no haya nada vacio y luego casteo a int y double
            if (nombreServicio == "" && descripcionServicio == "" && cantidadServicio == "" && costoServicio == "")
            {
                Console.WriteLine("Tiene que completar todos los campos");
                Console.WriteLine("No puede hacer campos en blancos\n");
            }
            else
            {
                int canServ = int.Parse(cantidadServicio);
                double conServ = double.Parse(costoServicio);
                Servicio nuevo = new Servicio(nombreServicio, descripcionServicio, canServ, conServ);
                salon.AgregarServicioNuevo(nuevo);
                Console.WriteLine("Servicio agregado exitosamente.");
            }


        }

        static void EliminarServicio(Salon salon)
        {
            Console.WriteLine("Ingrese nombre del servicio a eliminar:");
            string nombre = Console.ReadLine();

            // declaro un atributo del tipo Salon nula
            Servicio servicioAeliminar = null;
            foreach (Servicio servi in salon.SalonServicios)
            {
                // busco el servicio y si lo encuentro se lo asigno a servicioAeliminar
                if (servi.ServicioNombre == nombre)
                {
                    servicioAeliminar = servi;
                    break;
                }
            }

            // Si servicioAeliminar no es nulo entonces borro servicioAeliminar
            // Caso contrario no encuentro el servicio
            if (servicioAeliminar != null)
            {
                salon.SalonServicios.Remove(servicioAeliminar);
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
                string dni = Console.ReadLine();
                Console.WriteLine("Ingrese un sueldo:");
                string sueldo = Console.ReadLine();

                if (rta == "1")
                {
                    Console.WriteLine("Ingrese una tarea:");
                    string tarea = Console.ReadLine();
                    if (nombre == "" && apellido == "" && dni == "" && sueldo == "" && tarea == "")
                    {
                        Console.WriteLine("No pueden haber campos en blanco.\n");
                    }
                    else
                    {
                        int intDNI = int.Parse(dni);
                        int intSUELDO = int.Parse(sueldo);
                        Empleado empNuevo = new Empleado(nombre, apellido, intDNI, intSUELDO, tarea);
                        salon.AgregarEmpleadoNuevo(empNuevo);
                        Console.WriteLine("Empleado agregado.");
                    }
                }
                else
                {
                    string tarea = "Encargado";
                    Console.WriteLine("Ingrese el plus del encargado:");
                    string plus = Console.ReadLine();
                    if (nombre == "" && apellido == "" && dni == "" && sueldo == "" && plus == "")
                    {
                        Console.WriteLine("No pueden haber campos en blanco.\n");
                    }
                    else
                    {
                        int intDNI = int.Parse(dni);
                        int intSUELDO = int.Parse(sueldo);
                        int intPLUS = int.Parse(plus);
                        Encargado encNuevo = new Encargado(nombre, apellido, intDNI, intSUELDO, tarea, intPLUS);
                        salon.AgregarEmpleadoNuevo(encNuevo);
                        Console.WriteLine("Encargado agregado.");
                    }
                }

                Console.WriteLine("Agregar otro (1)empleado (2)encargado o presione otra tecla para salir)");
                rta = Console.ReadLine();
            }
        }

        static void BajaEmpleado(Salon salon)
        {
            Console.WriteLine("Lista empleados:");
            salon.ListarEmpleados();
            
            Console.WriteLine("Ingrese DNI del empleado a eliminar:");
            string dniAeliminar = Console.ReadLine();
            if (dniAeliminar == "")
            {
                Console.WriteLine("Debe de ingresar un dni para continuar\n");
            }
            else
            {
                int dniEliminar = int.Parse(dniAeliminar);
                salon.EliminarEmpleado(dniEliminar);
            }
        }

        static void ReservarEvento(Salon salon)
        {
            // Pongo todo en un try por que me esta dando muchos dolores de cabeza el tener que validar tantos campos diferentes
            try
            {

                Console.WriteLine("Ingrese nombre del cliente:");
                string nombre = Console.ReadLine();

                Console.WriteLine("Ingrese DNI del cliente:");
                int dni = int.Parse(Console.ReadLine());

                Console.WriteLine("Ingrese fecha del evento (yyyy-mm-dd):");
                DateTime fecha = DateTime.Parse(Console.ReadLine());

                // Listado de eventos disponibles
                ListaDeEventos listaEventos = new ListaDeEventos();
                Console.WriteLine("Tipos de eventos disponibles:");
                int count = 1;
                ArrayList listaDeEventosDisponibles = new ArrayList();
                foreach (TipoEventoNuevo tipoE in listaEventos.EventosTipos)
                {
                    Console.WriteLine(count + " - " + tipoE.EventoNuevoNombre + " " + "$" + tipoE.EventoNuevoPrecio);
                    listaDeEventosDisponibles.Add(tipoE.EventoNuevoNombre);
                    count++;
                }
                int numeroEvento = int.Parse(Console.ReadLine());
                TipoEventoNuevo tipoSeleccionado = (TipoEventoNuevo)listaEventos.EventosTipos[numeroEvento - 1];
                string tipoEvento = tipoSeleccionado.EventoNuevoNombre;
                double precioBase = tipoSeleccionado.EventoNuevoPrecio;


                Console.WriteLine("Ingrese monto de la seña:");
                double seña = double.Parse(Console.ReadLine());

                // Buscar un encargado disponible
                count = 1;
                Encargado encargado = null;
                ArrayList listaDeEncargadosDisponible = new ArrayList();
                Console.WriteLine("Encargados disponibles: ");
                foreach (Empleado enc in salon.SalonEmpleados)
                {
                    if (enc is Encargado)
                    {
                        listaDeEncargadosDisponible.Add(enc);
                        Console.WriteLine(count + " - " + enc.EmpleadoNombre + " " + enc.EmpleadoApellido);
                        count++;
                    }
                }
                int numeroEncargado = int.Parse(Console.ReadLine());
                encargado = (Encargado)listaDeEncargadosDisponible[numeroEncargado - 1];

                if (encargado == null)
                {
                    Console.WriteLine("No hay encargados disponibles.");
                    return;
                }

                Evento nuevoEvento = new Evento(nombre, dni, fecha, tipoEvento, encargado, seña, precioBase);

                Console.WriteLine("¿Que servicios desea agregar al evento?");
                ArrayList listaDeServiciosDisponibles = new ArrayList();
                ArrayList listaDeServiciosDisponiblesF = new ArrayList();
                count = 1;
                // Listo los servicios disponibles
                Console.WriteLine("Seleccione los servicios que va a utilizar: ");
                foreach (Servicio servi in salon.SalonServicios)
                {
                    listaDeServiciosDisponibles.Add(servi);
                    Console.WriteLine(count + " - " + servi.ServicioNombre + " - " + servi.ServicioCostoUnitario + " - " + servi.ServicioDescripcion);
                    count++;
                }
                Console.WriteLine("Si va a usar mas de uno separelo por una coma (,) Ej: 2,4,10 ");
                // guardo la lista de servicios seleccionados
                string numeroServicio = Console.ReadLine();
                string[] arrayDeNumeros = numeroServicio.Split(',');
                foreach (string num in arrayDeNumeros)
                {
                    listaDeServiciosDisponiblesF.Add(listaDeServiciosDisponibles[int.Parse(num) - 1]);
                }
                // Imprimo la lista de servicios que voy a usar
                foreach (Servicio serviF in listaDeServiciosDisponiblesF)
                {
                    Console.WriteLine("- " + serviF.ServicioNombre);
                }
                foreach (Servicio serviF in listaDeServiciosDisponiblesF)
                {
                    nuevoEvento.AgregarServicio(serviF);
                }

                try
                {
                    salon.ReservaEvento(nuevoEvento);
                    Console.WriteLine("Reserva realizada correctamente.");
                    Console.WriteLine("Costo total del evento: " + "$" + nuevoEvento.CostoTotal());
                    Console.WriteLine("Seña dada: " + seña);
                    double restante = nuevoEvento.CostoTotal() - seña;
                    Console.WriteLine("Restan: " + restante);

                }
                catch (FechaDeReservaException e)
                {
                    Console.WriteLine("Error: " + e.Message);
                }
            }
            catch (Exception exce)
            {
                Console.WriteLine("Faltan ingresar datos o a ingresado mal un dato: " + exce.Message);
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
            catch (Exception exce)
            {
                Console.WriteLine("Error al cancelar evento: " + exce.Message);
            }
        }

        static void SubMenuImpresion(Salon salon)
        {
            Console.WriteLine("Submenú de impresión:");
            Console.WriteLine("1 - Listado de empleados");
            Console.WriteLine("2 - Listado de clientes");
            Console.WriteLine("3 - Listado de eventos");
            Console.WriteLine("4 - Listado de eventos por mes");

            string opcionSubMenu = Console.ReadLine();

            switch (opcionSubMenu)
            {
                case "1":
                    salon.ListarEmpleados();
                    break;
                case "2":
                    // Si hay 0 eventos quiere decir que no hay clientes
                    if (salon.SalonEventos.Count == 0)
                    {
                        Console.WriteLine("No hay clientes registrados todavía.");
                    }
                    else
                    {
                        Console.WriteLine("Clientes que hicieron una recerva:");
                        foreach (Evento e in salon.SalonEventos)
                        {
                            Console.WriteLine("Nombre: " + e.ClienteNombre + " - DNI: " + e.ClienteDni + " - Fecha de reserva: " + e.FechaHora.ToShortDateString());
                        }
                    }
                    break;
                case "3":
                    Console.WriteLine("Lista de eventos reservados");
                    foreach (Evento e in salon.SalonEventos)
                        Console.WriteLine("Tipo de evento: " + e.TipoDeEvento + " - Nombre del cliente: " + e.ClienteNombre + " - Fecha de reserva: " + e.FechaHora.ToShortDateString());
                    break;
                case "4":
                    Console.WriteLine("Ingrese número del mes (1-12):");
                    int mes = int.Parse(Console.ReadLine());
                    foreach (Evento e in salon.SalonEventos)
                        if (e.FechaHora.Month == mes)
                            Console.WriteLine("Tipo de evento: " + e.TipoDeEvento + " - Nombre del cliente: " + e.ClienteNombre + " - Fecha de reserva: " + e.FechaHora.ToShortDateString());
                    break;
                default:
                    Console.WriteLine("Opción inválida.");
                    break;
            }
        }
    }
}