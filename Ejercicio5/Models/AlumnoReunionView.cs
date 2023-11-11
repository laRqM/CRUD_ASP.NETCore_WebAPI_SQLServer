using System;
using System.Collections.Generic;

namespace Ejercicio5.Models;

public partial class AlumnoReunionView
{
    public int IdPersona { get; set; }

    public string? NombreUno { get; set; }

    public string? NombreDos { get; set; }

    public string? ApellidoUno { get; set; }

    public string? ApellidoDos { get; set; }

    public DateTime? DNacimiento { get; set; }

    public string? Matricula { get; set; }

    public string? Carrera { get; set; }

    public string? Semestre { get; set; }

    public string? Especialidad { get; set; }

    public int IdReunion { get; set; }

    public DateTime? Fecha { get; set; }

    public TimeSpan? Hora { get; set; }

    public string? Lugar { get; set; }

    public string? Tema { get; set; }
}