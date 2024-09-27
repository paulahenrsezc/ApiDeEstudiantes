using ApiDeEstudiantes.Context;
using ApiDeEstudiantes.Entidades;
using ApiDeEstudiantes.Models;
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
        public async Task<ActionResult<IEnumerable<Estudiante>>> Get()
        {
            return await _context.Estudiante.Include(e => e.Carrera).ToListAsync();
        }

        [HttpPost("AgregarEstudiantes")]
        public async Task <ActionResult<EstudianteModel>> Post (EstudianteModel estudiante)
        {
            if(estudiante == null)
            {
                return BadRequest();
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

        [HttpPut("ActualizarEstudiantes")]
        public async

    }
}
