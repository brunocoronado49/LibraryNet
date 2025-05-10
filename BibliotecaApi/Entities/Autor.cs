using System.ComponentModel.DataAnnotations;

namespace BibliotecaApi.Entities
{
    public class Autor
    {
        public int Id { get; set; }
        [Required]
        public required string Name { get; set; }
    }
}
