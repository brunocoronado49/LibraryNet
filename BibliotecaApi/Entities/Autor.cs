using System.ComponentModel.DataAnnotations;

namespace BibliotecaApi.Entities
{
    public class Autor
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido man.")]
        [StringLength(10, ErrorMessage = "El campo {0} debe tener menos de {1} caracteres.")]
        public required string Name { get; set; }
        public List<Libro> Libros { get; set; } = new List<Libro>();
        [Range(18, 120)]
        public int Age { get; set; }
        [CreditCard]
        public string? CreditCard { get; set; }
        [Url]
        public string? URL { get; set; }
    }
}
