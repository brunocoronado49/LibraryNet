using BibliotecaApi.Data;
using BibliotecaApi.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BibliotecaApi.Controllers
{
    [ApiController]
    [Route("api/libros")]
    public class LibrosController : ControllerBase
    {
        private readonly ApplicationDbContext context;

        public LibrosController(ApplicationDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<IEnumerable<Libro>> Get()
        {
            return await context.Libros.ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult> Post(Libro libro)
        {
            var autorExiste = await context.Autores.AnyAsync(a => a.Id == libro.AutorId);

            if (!autorExiste)
            {
                return BadRequest($"El Id del autor ({libro.AutorId}) no existe.");
            }

            context.Add(libro);
            await context.SaveChangesAsync();
            return CreatedAtRoute("ObtenerLibro", new {id = libro.Id}, libro);
        }

        [HttpGet("{id:int}", Name = "ObtenerLibro")]
        public async Task<ActionResult<Libro>> GetLibroById(int id)
        {
            var libro = await context.Libros
                .Include(a => a.Autor)
                .FirstOrDefaultAsync(l => l.Id == id);

            if(libro == null)
            {
                return NotFound();
            }

            return libro;
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, Libro libro)
        {
            var autorExiste = await context.Autores.AnyAsync(a => a.Id == libro.AutorId);

            if (!autorExiste)
            {
                ModelState.AddModelError(nameof(libro.AutorId), $"El Id del autor ({libro.AutorId}) no existe.");
                return ValidationProblem();
            }

            if (id != libro.Id)
            {
                return BadRequest("Los id's no coinciden");
            }

            context.Update(libro);
            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var registroBorrado = await context.Libros.Where(l =>  l.Id == id).ExecuteDeleteAsync();

            if(registroBorrado == 0)
            {
                return NotFound();
            }

            return Ok();
        }
    }
}
