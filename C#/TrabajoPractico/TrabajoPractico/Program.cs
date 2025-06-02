/*
 * Created by VIM.
 * User: Due204
 * Date: 26/5/2025
 * Time: 13:30
 * 
 */
using System;
using SalonEventos.Salones;

namespace SalonEventos
{
class Tp {

	static void Main(string[] args) {
		Console.WriteLine("Trabajo Practico: Salon de eventos\n");
		
		Salon salon = new Salon();
		
		Console.WriteLine("Instanciacion: " + salon.Eventos);
		salon.Eventos = "Hola mundo";
		Console.WriteLine("Instanciacion: " + salon.Eventos);

		//Muestro el menu
		menu();
		//Obtengo la entrada y la paso a minusculas por las moscas
		string optMenu = Console.ReadLine().ToLower();
		
		switch(optMenu)
			{
				case "a":
					Console.WriteLine("A");
					break;
				case "b":
					Console.WriteLine("B");
					break;
				case "c":
					Console.WriteLine("C");
					break;
				case "d":
					Console.WriteLine("D");
					break;
				case "e":
					Console.WriteLine("E");
					break;
				case "f":
					Console.WriteLine("F");
					break;
				case "g":
					Console.WriteLine("G");
					break;
				case "h":
					Console.WriteLine("H");
					break;
				default:
					Console.WriteLine("Opción inválida.");
					break;
			}
		
		Console.ReadKey();
	}
	
	static void menu(){
		// Menu principal
		Console.WriteLine("A - Agregar un servicio");
		Console.WriteLine("B - Eliminar un servicio");
		Console.WriteLine("C - Dar de alta un empleado/encargado");
		Console.WriteLine("D - Dar de baja un empleado/encargado");
		Console.WriteLine("E - Reservar el salon para un evento");
		Console.WriteLine("F - Cancelar un evento");
		Console.WriteLine("G - Submenu de impresion");
		Console.Write("? ");
	
	}
}
}
