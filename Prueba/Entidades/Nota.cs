using System.ComponentModel.DataAnnotations;

namespace Prueba.Entidades
{
    public class Nota
    {
        public Nota() // Este es el constructor de la clase Nota
        {
            this.CategoriasNota = new List<CategoriaNota>(); // Inicializa CategoriasNota como una nueva lista de CategoriaNota
        }

        public int Id { get; set; }
        [StringLength(250)]
        [Required]
        public string Titulo { get; set; }  //Titulo de la Nota

        public List<CategoriaNota> CategoriasNota { get; set; }   //** Propiedad de Navegación que nos lleva a la Data Relacionada CategoriasNota

    }
}