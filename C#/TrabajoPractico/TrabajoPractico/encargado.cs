using System;
using SalonEventos.Empleados;
	
	
namespace SalonEventos.Encagados
{
	public class Encargado : Empleado{
		
		// Atributos del encargado
		private int plusEncargado;
		
		// Metodos
		public int EncargadoPlus{
			get {return plusEncargado;}
			set {plusEncargado = value;}
		}
		
		public override int SueldoRecalculado(){
			return base.EmpleadoSueldo + plusEncargado;
		}
				
		
		public Encargado(string nombre, string apellido, int documento, int sueldo, string tarea, int plus) : base(nombre, apellido, documento, sueldo, tarea){
			// Siempre
			// Siempre
			// Siempre
			// Siempre
			// Siempre
			// Siempre
			// Siempre el constructor lleva el nombre de la clase
			this.plusEncargado = plus;
			}
	}
}