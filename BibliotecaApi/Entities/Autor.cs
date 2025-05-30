﻿using BibliotecaApi.Validations;
using System.ComponentModel.DataAnnotations;

namespace BibliotecaApi.Entities
{
    public class Autor
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido man.")]
        [StringLength(100, ErrorMessage = "El campo {0} debe tener menos de {1} caracteres.")]
        [FirstLetterMayus]
        public required string Names { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido man.")]
        [StringLength(100, ErrorMessage = "El campo {0} debe tener menos de {1} caracteres.")]
        [FirstLetterMayus]
        public required string LastNames { get; set; }
        [StringLength(20, ErrorMessage = "El campo {0} debe tener menos de {1} caracteres.")]
        public string? Identification { get; set; }
        public List<Libro> Libros { get; set; } = new List<Libro>();
    }
}
