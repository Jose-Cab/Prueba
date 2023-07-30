using System.ComponentModel.DataAnnotations;

namespace Prueba.Entidades
{
    public class CategoriaNota
    {
        //** Creamos entidad CategoriaNota con realación MUCHOS a MUCHOS
        //** Una CategoriaNota puede tener muchas Notas

        public int Id { get; set; }
        [StringLength(100)]
        [Required]
        public string Nombre { get; set; } 

        public List<Nota> Notas { get; set; } //** Propiedad de Navegación que nos lleva a la Data Relacionada Notas
    }
}
