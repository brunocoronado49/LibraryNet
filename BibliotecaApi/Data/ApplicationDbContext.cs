using BibliotecaApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace BibliotecaApi.Data
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Autor>().Property(a => a.Name).HasMaxLength(100);
        }

        public DbSet<Autor> Autores { get; set; }
        public DbSet<Libro> Libros { get; set; }

    }
}
