using ApiDeEstudiantes.Context;
using ApiDeEstudiantes.Entidades;
using ApiDeEstudiantes.Models;
using ApiDeEstudiantes.Models.Carreras;
using ApiDeEstudiantes.Models.Estudiantes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiDeEstudiantes.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EstudianteController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public EstudianteController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("ObtenerEstudiantes")]
        public async Task<ActionResult<IEnumerable<EstudianteGetModel>>> Get()
        {
            var estudiantes = await _context.Estudiante
            .Select(e => new EstudianteGetModel
            {
                EstudianteId = e.EstudianteId,
                MatriculaId = e.MatriculaId,
                NombreEstudiante = e.NombreEstudiante,
                CarreraId = e.CarreraId,
                Carrera = new CarreraModel
                {
                    CarreraId = e.Carrera.CarreraId,
                    NombreCarrera = e.Carrera.NombreCarrera
                }
            })
            .ToListAsync();

            return Ok(estudiantes);
        }

        [HttpPost("AgregarEstudiantes")]
        public async Task <ActionResult<EstudianteModel>> Post (EstudianteModel estudiante)
        {
            if(estudiante == null)
            {
                return BadRequest();
            }
            var carreraExists = await _context.Carrera.AnyAsync(c => c.CarreraId == estudiante.CarreraId);
            if (!carreraExists)
            {
                return NotFound($"La carrera con ID {estudiante.CarreraId} no existe.");
            }

            var newEstudiante = new Estudiante
            {
                EstudianteId = estudiante.EstudianteId,
                MatriculaId = estudiante.MatriculaId,
                NombreEstudiante = estudiante.NombreEstudiante,
                CarreraId = estudiante.CarreraId

            };

            _context.Estudiante.Add(newEstudiante);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = estudiante.EstudianteId }, estudiante);
            
        }

        [HttpPut("EditarEstudiantes")]
        public async Task<IActionResult> Put(int id, EstudianteModel estudiante)
        {
            if (id != estudiante.EstudianteId)
            {
                return BadRequest();
            }
            var editEstudiante = new Estudiante
            {
                EstudianteId = estudiante.EstudianteId,
                MatriculaId = estudiante.MatriculaId,
                NombreEstudiante = estudiante.NombreEstudiante,
                CarreraId = estudiante.CarreraId

            };

            _context.Entry(editEstudiante).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("EliminarEstudiantes")]
        public async Task<IActionResult> Delete(int id)
        {
            var estudiante = await _context.Estudiante.FindAsync(id);
            if (estudiante == null)
            {
                return NotFound();
            }
            _context.Estudiante.Remove(estudiante);
            await _context.SaveChangesAsync();
            return NoContent();
        }

    }
}
