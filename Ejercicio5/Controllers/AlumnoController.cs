using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using Ejercicio3.Models.ViewModels;
using Ejercicio5.DTOs;
using Ejercicio5.Models;
using Swashbuckle.AspNetCore.Annotations;

namespace Ejercicio5.Controllers
{
    [Route("api/alumnos")]
    [SwaggerTag("Todo lo relacionado a los alumnos")]
    [ApiController]
    public class AlumnoController : ControllerBase
    {
        private readonly Ejercicio5DbContext _context;
        
        private Encriptacion _encrypt; // Inicializamos una variable de la clase Encriptacion.

        public AlumnoController(Ejercicio5DbContext context) { // Constructor que recibe un objeto DbContext.
            _context = context;
            _encrypt = new Encriptacion();
        }

        [HttpPost]
        [SwaggerOperation(
            Summary = "Endpoint para crear un alumno",
            Description = "Requiere permisos de administrador",
            OperationId = "CrearAlumno",
            Tags = new[] { "Alumno" }
        )]
        public async Task<ActionResult> CreateAlumnos(AlumnoCreateDTO model) {
            if (ModelState.IsValid) { // Si el estatus del modelo es válido...
                var persona = new Persona { // Declaramos la variable persona e inicializamos la instancia de la clase.
                    // Los campos de Persona(lado izquierdo) serán igual a los datos recibidos en el modelo(lado derecho).
                    NombreUno = _encrypt.Encriptar(model.NombreUno),
                    NombreDos = model.NombreDos,
                    ApellidoUno = model.ApellidoUno,
                    ApellidoDos = model.ApellidoDos,
                    DNacimiento = model.FechaNacimiento,
                    TipoRol = "Alumno" // Asignamos el rol directamente.
                };
                
                _context.Personas.Add(persona); // Agregamos el objeto Persona al contexto
                await _context.SaveChangesAsync();
        
                // Obtener el ID generado para la nueva persona
                var idPersona = persona.IdPersona;
        
                // Crear una instancia de Alumno y llenarla con los datos del modelo AlumnoViewModel
                var alumno = new Alumno
                {
                    IdPersona = idPersona, // Asociar el ID de la persona con el alumno
                    Matricula = model.Matricula,
                    Carrera = model.Carrera,
                    Semestre = model.Semestre,
                    Especialidad = model.Especialidad
                };
        
                // Agregar el objeto Alumno al contexto y guardar los cambios en la base de datos
                _context.Alumnos.Add(alumno);
                await _context.SaveChangesAsync();
                // await _context.SaveChangesAsync(); significa que se están guardando todos los cambios pendientes
                // en el contexto de datos de Entity Framework de manera asincrónica. Esto significa que que no bloqueará
                // el hilo de ejecución principal, permitiendo que el programa continúe ejecutándose de manera eficiente
                // mientras se espera la respuesta de la base de datos.
            }
            return Ok();
        }

        [HttpGet]
        
        [SwaggerOperation(
            Summary = "Endpoint para listar los alumnos",
            Description = "No se requieren permisos de administrador",
            OperationId = "ListarAlumnos",
            Tags = new[] { "Alumno" }
        )]
        public async Task<ActionResult<IEnumerable<AlumnoViewModel>>> GetAlumnos()
        {
            var alumnos = await _context.Alumnos
                .Select(a => new AlumnoViewModel
                {
                    IDPersona = a.IdPersona,
                    NombreUno = _encrypt.Desencriptar(a.IdPersonaNavigation.NombreUno),
                    NombreDos = a.IdPersonaNavigation.NombreDos,
                    ApellidoUno = a.IdPersonaNavigation.ApellidoUno,
                    ApellidoDos = a.IdPersonaNavigation.ApellidoDos,
                    FechaNacimiento = a.IdPersonaNavigation.DNacimiento,
                    Rol = a.IdPersonaNavigation.TipoRol,
                    Matricula = a.Matricula,
                    Carrera = a.Carrera,
                    Semestre = a.Semestre,
                    Especialidad = a.Especialidad
                })
                .ToListAsync();

            return Ok(alumnos);
        }

        [HttpPut("{id:int}")]
        
        [SwaggerOperation(
            Summary = "Endpoint para actualizar un alumno",
            Description = "Requiere permisos de administrador",
            OperationId = "ActualizarAlumno",
            Tags = new[] { "Alumno" }
        )]
        public async Task<ActionResult> UpdateAlumnos(int id, AlumnoUpdateDTO model)
        {
            var persona = await _context.Personas
                .Where(persona => persona.IdPersona == id)
                .Include(persona => persona.Alumno)
                .FirstOrDefaultAsync();
            
            // Declaramos a la variable persona 
            // await _context.Personas es una operación asincrónica para obtener todos los registros de la
            // tabla "Personas" de la base de datos.
            
            //  El método .Include() se utiliza para cargar de manera anticipada la propiedad de navegación "Alumno"
            // de las personas seleccionadas. Esto significa que, cuando se recuperen los estudiantes, también se cargarán
            // en memoria las propiedades relacionadas de "Alumno" para evitar cargarlas de manera diferida más adelante.
            // La carga anticipada se conoce como "eager loading" y la carga diferida como "lazy loading".
            
            // .FirstOrDefaultAsync(): Este método se utiliza para seleccionar la primera persona que cumple con el
            // filtro establecido. La operación se realiza de manera asincrónica, lo que significa que se espera la
            // respuesta de la base de datos de manera asincrónica y, una vez que se completa, se obtiene el primer
            // objeto que cumple con el filtro o null si no se encuentra ninguna coincidencia.

            if (persona != null && persona.TipoRol == "Alumno") // Si la variable persona es distinta de núlo y el tipo de rol es "Alumno"
            {
                // Actualizar solo los campos que han sido modificados
                // Esto se hace para garantizar que, si el nombre no fue modificado, no se lo actualice.
                if (!string.IsNullOrEmpty(model.NombreUno))
                {
                    persona.NombreUno = _encrypt.Encriptar(model.NombreUno);
                }

                // Actualizar los demás campos del alumno en la tabla persona.
                persona.NombreDos = model.NombreDos;
                persona.ApellidoUno = model.ApellidoUno;
                persona.ApellidoDos = model.ApellidoDos;
                persona.DNacimiento = model.FechaNacimiento;

                // Actualizar datos en la tabla alumno
                var alumno = persona.Alumno;
                if (alumno != null)
                {
                    alumno.Matricula = model.Matricula;
                    alumno.Carrera = model.Carrera;
                    alumno.Semestre = model.Semestre;
                    alumno.Especialidad = model.Especialidad;
                }

                _context.Update(persona); // Actualizamos los datos.

                await _context.SaveChangesAsync(); // Guardamos los cambios en la base de datos.
                return Ok(); // Retornamos un 200.
            }
            else
            {
                return NotFound(); // El ID ingresado no corresponde a un alumno o bien, no existe en la base de datos. Así que retornamos un 404 Not Found.
            }
        }


        [HttpDelete("{id:int}")]
        
        [SwaggerOperation(
            Summary = "Endpoint para eliminar un alumno",
            Description = "Requiere permisos de administrador",
            OperationId = "EliminarAlumno",
            Tags = new[] { "Alumno" }
        )]
        public async Task<ActionResult> DeleteAlumnos(int id)
        {
            var alumno = await _context.Personas
                .Where(alumno => alumno.IdPersona == id && alumno.TipoRol == "Alumno")
                .ExecuteDeleteAsync();
            
            // Declaramos a la variable alumno 
            // await _context.Personas es una operación asincrónica para obtener todos los registros de la
            // tabla "Personas" de la base de datos.
            
            // .Where(alumno => alumno.IdPersona == id && alumno.TipoRol == "Alumno") filtra la colección de personas seleccionando
            // a aquella cuyo id_persona sea igual al id que está recibiendo en la función y además que en TipoRol tenga "Alumno".
            
            // .ExecuteDeleteAsync() Elimina de forma asincrónica filas de la base de datos para las instancias de entidad que
            // coinciden con la consulta LINQ de la base de datos.

            if (alumno == 0) // Si alumno es igual a 0, significa que no hay datos que coincidan...
            {
                return NotFound(); // Así que retornamos un 404 Not Found.
            }

            return NoContent(); // Si hay datos, por lo que retornamos un No Content, que es similar a un 200. Solo que no devuelve nada.
        }
    }
}
