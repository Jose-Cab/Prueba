using System.ComponentModel.DataAnnotations;

namespace Prueba.Entidades
{
    public class Nota
    {
        //** Creamos entidad Nota con realación MUCHOS a MUCHOS
        //** Una Nota puede tener muchas CategoriasNota

        public int Id { get; set; }
        [StringLength(250)]
        [Required]
        public string Titulo { get; set; }  //Titulo de la Nota

        public List<CategoriaNota> CategoriasNota { get; set; }   //** Propiedad de Navegación que nos lleva a la Data Relacionada CategoriasNota

    }
}