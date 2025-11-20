using System;

/*

En esta parte se definen las excepciones personalizadas que se utilizaran en el proyecto.
Originalmente se pidio la excepcion CupoLlenoException pero se agrego otra para manejar 
 legajos negativos, alumnos inesitentes, cursos inexistentes y alumnos ya inscriptos en 
 un curso.

*/

namespace TP_Programacion_Objetos
{
    public class CupoLlenoException : Exception
    {
        public CupoLlenoException(string message) : base(message)
        {
        }

    }

    public class LegajoIncorrectoException : Exception
    {
        public LegajoIncorrectoException(string message) : base(message)
        {
        }

    }

    public class AlumnoInexistenteException : Exception
    {
        public AlumnoInexistenteException(string message) : base(message)
        {
        }

    }

    public class CursoInexistenteException : Exception
    {
        public CursoInexistenteException(string message) : base(message)
        {
        }

    }

    public class AlumnoInscriptoException : Exception
    {
        public AlumnoInscriptoException(string message) : base(message)
        {
        }

    }

    public class AlumnoRepetidoException : Exception
    {
        public AlumnoRepetidoException(string message) : base(message)
        {
        }

    }
}