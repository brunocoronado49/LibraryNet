using AutoMapper;
using BibliotecaApi.Data;
using BibliotecaApi.DTOs;
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
        private readonly IMapper mapper;

        public LibrosController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<LibroDTO>> Get()
        {
            var libros = await context.Libros.ToListAsync();
            var librosDto = mapper.Map<IEnumerable<LibroDTO>>(libros);

            return librosDto;
        }

        [HttpPost]
        public async Task<ActionResult> Post(LibroCreacionDTO libroCreacionDto)
        {
            var autorExiste = await context.Autores.AnyAsync(a => a.Id == libroCreacionDto.AutorId);

            if (!autorExiste)
            {
                return BadRequest($"El Id del autor ({libroCreacionDto.AutorId}) no existe.");
            }

            var libro = mapper.Map<Libro>(libroCreacionDto);
            context.Add(libro);
            await context.SaveChangesAsync();
            var libroDto = mapper.Map<LibroDTO>(libro);
            return CreatedAtRoute("ObtenerLibro", new {id = libro.Id}, libroDto);
        }

        [HttpGet("{id:int}", Name = "ObtenerLibro")]
        public async Task<ActionResult<LibroDTO>> GetLibroById(int id)
        {
            var libro = await context.Libros
                .Include(a => a.Autor)
                .FirstOrDefaultAsync(l => l.Id == id);

            if(libro == null)
            {
                return NotFound();
            }

            var libroDto = mapper.Map<LibroDTO>(libro);

            return libroDto;
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, LibroCreacionDTO libroCreacionDto)
        {
            var autorExiste = await context.Autores.AnyAsync(a => a.Id == libroCreacionDto.AutorId);

            if (!autorExiste)
            {
                ModelState.AddModelError(
                    nameof(libroCreacionDto.AutorId),
                    $"El Id del autor ({libroCreacionDto.AutorId}) no existe.");

                return ValidationProblem();
            }

            var libro = mapper.Map<Libro>(libroCreacionDto);
            libro.Id = id;
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
