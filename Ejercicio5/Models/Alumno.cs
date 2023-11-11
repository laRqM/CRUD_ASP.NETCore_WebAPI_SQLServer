using System;
using System.Collections.Generic;

namespace Ejercicio5.Models;

public partial class Alumno
{
    public int IdPersona { get; set; }

    public string? Matricula { get; set; }

    public string? Carrera { get; set; }

    public string? Semestre { get; set; }

    public string? Especialidad { get; set; }

    public virtual Persona IdPersonaNavigation { get; set; } = null!;

    public virtual ICollection<Reunion> IdReunions { get; set; } = new List<Reunion>();
}