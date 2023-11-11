using System.ComponentModel.DataAnnotations;

namespace Ejercicio5.DTOs;

public class AlumnoUpdateDTO
{
    [Display(Name = "Identificador")]
    public int IdPersona { get; set; }
    
    [Required(ErrorMessage = "El campo {0} es requerido")]
    [Display(Name = "Primer Nombre")]
    [MinLength(2, ErrorMessage = "El campo {0} no puede ser menor a 2 caracteres")]
    public string? NombreUno { get; set; }
    
    [Display(Name = "Segundo Nombre")]
    public string? NombreDos { get; set; }
    
    [Required(ErrorMessage = "El campo {0} es requerido")]
    [Display(Name = "Apellido Paterno")]
    [MinLength(2, ErrorMessage = "El campo {0} no puede ser menor a 2 caracteres")]
    public string? ApellidoUno { get; set; }
    
    [Display(Name = "Apellido Materno")]
    public string? ApellidoDos { get; set; }
    
    [Required(ErrorMessage = "El campo {0} es requerido")]
    [Display(Name = "Fecha de Nacimiento")]
    public DateTime? FechaNacimiento { get; set; }
    
    [Display(Name = "Tipo de Rol")]
    public string? Rol { get; set; }
    
    [Required(ErrorMessage = "El campo {0} es requerido")]
    [Display(Name = "Matrícula")]
    [MinLength(2, ErrorMessage = "El campo {0} no puede ser menor a 2 caracteres")]
    public string? Matricula { get; set; }
    
    [Required(ErrorMessage = "El campo {0} es requerido")]
    [Display(Name = "Carrera")]
    [MinLength(2, ErrorMessage = "El campo {0} no puede ser menor a 2 caracteres")]
    public string? Carrera {get; set;}
    
    [Required(ErrorMessage = "El campo {0} es requerido")]
    [Display(Name = "Semestre")]
    public string? Semestre {get; set;}
    
    [Display(Name = "Especialidad")]
    public string? Especialidad {get; set;}
}