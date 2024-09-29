using ApiDeEstudiantes.Context;
using ApiDeEstudiantes.Entidades;
using ApiDeEstudiantes.Models.Carreras;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiDeEstudiantes.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CarreraController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CarreraController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("ObtenerCarrera")]
        public async Task<ActionResult<IEnumerable<CarreraModel>>> Get()
        {
            var carreras = await _context.Carrera
            .Select(c => new CarreraModel
            {
                CarreraId = c.CarreraId,
                NombreCarrera = c.NombreCarrera
            })
            .ToListAsync();

            return Ok(carreras);
        }

        [HttpPost("AgregarCarrera")]
        public async Task<ActionResult<CarreraModel>> Post(CarreraModel carrera)
        {
            if (carrera == null)
            {
                return BadRequest();
            }
            var newCarrera = new Carrera
            {
                CarreraId = carrera.CarreraId,
                NombreCarrera = carrera.NombreCarrera
            };

            _context.Carrera.Add(newCarrera);
             await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = carrera.CarreraId }, carrera);

        }

        [HttpPut("EditarCarrera")]
        public async Task <IActionResult> Put (int id, CarreraModel carrera)
        {
            if (id != carrera.CarreraId)
            {
                return BadRequest();
            }
            var editCarrera = new Carrera
            {
                CarreraId = carrera.CarreraId,
                NombreCarrera = carrera.NombreCarrera
            };

            _context.Entry(editCarrera).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("EliminarCarrera")]
        public async Task <IActionResult> Delete(int id)
        {
            var carrera = await _context.Carrera.FindAsync(id);
            if (carrera == null)
            {
                return NotFound();
            }
            _context.Carrera.Remove(carrera);
            await _context.SaveChangesAsync();
            return NoContent();
        }

    }
}
