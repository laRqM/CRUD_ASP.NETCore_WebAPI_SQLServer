using System;
using System.Collections.Generic;

namespace Ejercicio5.Models;

public partial class Persona
{
    public int IdPersona { get; set; }

    public string? NombreUno { get; set; }

    public string? NombreDos { get; set; }

    public string? ApellidoUno { get; set; }

    public string? ApellidoDos { get; set; }

    public DateTime? DNacimiento { get; set; }

    public string? TipoRol { get; set; }

    public virtual Alumno? Alumno { get; set; }

    public virtual Instructor? Instructor { get; set; }
}