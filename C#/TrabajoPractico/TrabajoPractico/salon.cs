using System;
using SalonEventos.Empleados;
using SalonEventos.Encagados;

namespace SalonEventos.Salones
{
    public class Salon{
        private string _eventos = "Eventos del salon por due204";
        private string _empleados = "Empleados del salon";
        
        public string Eventos {
            get { return _eventos; }
            set { _eventos = value; }
		}
		
        public string Empleados {
            get { return _empleados; }
            set { _empleados = value; }
        }
        
        
        Empleado empleado1 = new Empleado("Walter", "Corach", 34589061, 100, "Conductor");
        Encargado encargado1 = new Encargado("Walter", "Corach", 34589061, 100, "Conductor", 100);
        
        public Salon(){
			// Siempre
			// Siempre
			// Siempre
			// Siempre
			// Siempre
			// Siempre
			// Siempre el constructor lleva el nombre de la clase
		}
		
    }
}
