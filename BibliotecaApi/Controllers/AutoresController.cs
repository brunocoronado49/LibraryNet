using BibliotecaApi.Data;
using BibliotecaApi.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BibliotecaApi.Controllers
{
    [ApiController]
    [Route("api/autores")]
    public class AutoresController: ControllerBase
    {
        private readonly ApplicationDbContext context;
        public AutoresController(ApplicationDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<IEnumerable<Autor>> Get()
        {
            return await context.Autores.ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult> Post(Autor autor)
        {
            context.Add(autor);
            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Autor>> GetAutorById(int id)
        {
            var autor = await context.Autores.FirstOrDefaultAsync(a => a.Id == id);

            if(autor == null)
            {
                return NotFound();
            }

            return autor;
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, Autor autor)
        {
            if(id != autor.Id)
            {
                return BadRequest("Los id's no coinciden.");
            }

            context.Update(autor);
            await context.SaveChangesAsync();

            return Ok();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var registroBorrado = await context.Autores.Where(a => a.Id == id).ExecuteDeleteAsync();

            if(registroBorrado == 0)
            {
                return NotFound();
            }

            return Ok();
        }
    }
}
