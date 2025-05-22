using BibliotecaApi.Entities;
using System.ComponentModel.DataAnnotations;

namespace BibliotecaApi.DTOs
{
    public class LibroCreacionDTO
    {
        [Required]
        [StringLength(100, ErrorMessage = "El campo {0} debe tener menos de {1} caracteres.")]
        public required string Titulo { get; set; }
        public int AutorId { get; set; }
        public Autor? Autor { get; set; }
    }
}
