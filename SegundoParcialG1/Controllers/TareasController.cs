using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SegundoParcialG1.Data;
using SegundoParcialG1.Models;
using System.Collections.Generic;

namespace SegundoParcialG1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TareasController : ControllerBase
    {
        private readonly ProyectoDbContext _context;

        public TareasController(ProyectoDbContext context)
        {
            _context = context;
        }

        // -----------------------------------------------------------
        // 1. LISTAR TAREAS
        // -----------------------------------------------------------
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Tarea>>> GetTareas()
        {
            var tareas = await _context.Tareas
                .Include(t => t.Responsable)
                .Include(t => t.Prioridad)
                .ToListAsync();

            return Ok(tareas);
        }

        // -----------------------------------------------------------
        // 2. CREAR TAREA
        // -----------------------------------------------------------
        [HttpPost]
        public async Task<ActionResult<Tarea>> CrearTarea(Tarea tarea)
        {
            _context.Tareas.Add(tarea);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTareaById), new { id = tarea.Id }, tarea);
        }

        // Auxiliar para CreatedAtAction
        [HttpGet("{id}")]
        public async Task<ActionResult<Tarea>> GetTareaById(int id)
        {
            var tarea = await _context.Tareas
                .Include(t => t.Responsable)
                .Include(t => t.Prioridad)
                .FirstOrDefaultAsync(t => t.Id == id);

            if (tarea == null)
                return NotFound();

            return Ok(tarea);
        }

        // -----------------------------------------------------------
        // 3. EDITAR TAREA
        // -----------------------------------------------------------
        [HttpPut("{id}")]
        public async Task<IActionResult> EditarTarea(int id, Tarea tarea)
        {
            if (id != tarea.Id)
                return BadRequest("El ID no coincide.");

            _context.Entry(tarea).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Tareas.Any(t => t.Id == id))
                    return NotFound();
            }

            return NoContent();
        }

        // -----------------------------------------------------------
        // 4. ELIMINAR TAREA
        // -----------------------------------------------------------
        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarTarea(int id)
        {
            var tarea = await _context.Tareas.FindAsync(id);
            if (tarea == null)
                return NotFound();

            _context.Tareas.Remove(tarea);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // -----------------------------------------------------------
        // 5. ORDENAR TAREAS POR PRIORIDAD
        // -----------------------------------------------------------
        [HttpGet("ordenar/prioridad")]
        public async Task<ActionResult<IEnumerable<Tarea>>> OrdenarPorPrioridad()
        {
            var tareas = await _context.Tareas
                .Include(t => t.Prioridad)
                .OrderBy(t => t.Prioridad.Nombre)
                .ToListAsync();

            return Ok(tareas);
        }

        // -----------------------------------------------------------
        // 6. BUSCAR TAREAS POR TÍTULO
        // -----------------------------------------------------------
        [HttpGet("buscar-Tirulo")]
        public async Task<ActionResult<IEnumerable<Tarea>>> BuscarPorTitulo([FromQuery] string titulo)
        {
            var tareas = await _context.Tareas
                .Where(t => t.Titulo.Contains(titulo))
                .Include(t => t.Responsable)
                .Include(t => t.Prioridad)
                .ToListAsync();
            return Ok(tareas);
        }

        // -----------------------------------------------------------
        // 7. LISTAR TAREAS ASIGNADAS POR MIEMBRO
        // -----------------------------------------------------------
        [HttpGet("buscar-Miembro")]
        public async Task<ActionResult<IEnumerable<Tarea>>> BuscarPorMiembro(int idMiembro)
        {
            var tareas = await _context.Tareas
                .Include(t => t.Prioridad)
                .Include(t => t.Responsable)
                .Where(t => t.IdResponsable == idMiembro)
                .ToListAsync();
            return Ok(tareas);
        }
    }    
}
