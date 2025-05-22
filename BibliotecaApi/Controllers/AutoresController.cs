using AutoMapper;
using BibliotecaApi.Data;
using BibliotecaApi.DTOs;
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
        private readonly IMapper mapper;

        public AutoresController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet]
        [HttpGet("/listado-autores")]
        public async Task<IEnumerable<AutorDTO>> Get()
        {
            var autores = await context.Autores.ToListAsync();
            var autoresDto = mapper.Map<IEnumerable<AutorDTO>>(autores);

            return autoresDto;
        }

        [HttpPost]
        public async Task<ActionResult> Post(AutorCreacionDTO autorCreacionDto)
        {
            var autor = mapper.Map<Autor>(autorCreacionDto);
            context.Add(autor);
            await context.SaveChangesAsync();
            var autorDto = mapper.Map<AutorDTO>(autor);
            return CreatedAtRoute("ObtenerAutor", new {id = autor.Id}, autorDto);
        }

        [HttpGet("{id:int}", Name = "ObtenerAutor")]
        public async Task<ActionResult<AutorDTO>> GetAutorById(int id)
        {
            var autor = await context.Autores
                .Include(l => l.Libros)
                .FirstOrDefaultAsync(a => a.Id == id);

            if(autor == null)
            {
                return NotFound();
            }

            var autorDto = mapper.Map<AutorDTO>(autor);

            return autorDto;
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, AutorCreacionDTO autorCreacionDto)
        {
            var autor = mapper.Map<Autor>(autorCreacionDto);
            autor.Id = id;
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
