using System;
using System.Collections.Generic;

namespace Ejercicio5.Models;

public partial class Instructor
{
    public int IdPersona { get; set; }

    public string? Folio { get; set; }

    public virtual Persona IdPersonaNavigation { get; set; } = null!;

    public virtual ICollection<Reunion> IdReunions { get; set; } = new List<Reunion>();
}