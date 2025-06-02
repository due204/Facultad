using System;

namespace SalonEventos.Empleados
{
	public class Empleado{
		
		// Atributos del empleado
		private string nombreEmpleado;
		private string apellidoEmpleado;
		private int dniEmpleado;
		private int sueldoEmpleado;
		private string tareaEmpleado;
		
		// Metodos
		public string EmpleadoNombre{
			get {return nombreEmpleado;}
			set {nombreEmpleado = value;}
		}
		
		public string EmpleadoApellido{
			get {return apellidoEmpleado;}
			set {apellidoEmpleado = value;}
		}
		
		public int EmpleadoDni{
			get {return dniEmpleado;}
			set {dniEmpleado = value;}
		}
		
		public int EmpleadoSueldo{
			get {return sueldoEmpleado;}
			set {sueldoEmpleado = value;}
		}
		
		public string EmpleadoTarea{
			get {return tareaEmpleado;}
			set {tareaEmpleado = value;}
		}
		
		
		// Metodo para sobreescribir con override el plus del encargado
		// Polimorfismo puro loco
		public virtual int SueldoRecalculado(){
			return sueldoEmpleado;
		}
		
		
		public Empleado(string nombre, string apellido, int documento, int sueldo, string tarea){
			// Siempre
			// Siempre
			// Siempre
			// Siempre
			// Siempre
			// Siempre
			// Siempre el constructor lleva el nombre de la clase
			this.nombreEmpleado = nombre;
			this.apellidoEmpleado = apellido;
			this.dniEmpleado = documento;
			this.sueldoEmpleado = sueldo;
			this.tareaEmpleado = tarea;
			}
	}
}
