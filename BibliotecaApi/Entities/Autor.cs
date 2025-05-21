using BibliotecaApi.Validations;
using System.ComponentModel.DataAnnotations;

namespace BibliotecaApi.Entities
{
    public class Autor
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido man.")]
        [StringLength(100, ErrorMessage = "El campo {0} debe tener menos de {1} caracteres.")]
        [FirstLetterMayus]
        public required string Name { get; set; }
        public List<Libro> Libros { get; set; } = new List<Libro>();

    }
}
