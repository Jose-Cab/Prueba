using Microsoft.EntityFrameworkCore;
using Prueba.Entidades;
using Prueba.Models;

namespace Prueba
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }


        public DbSet<Nota> Notas { get; set; }
        public DbSet<CategoriaNota> CategoriasNota { get; set; }
    }
}
