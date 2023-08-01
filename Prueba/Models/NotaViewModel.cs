using Prueba.Entidades;

namespace Prueba.Models
{
    public class NotaViewModel
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public List<CategoriaNota> CategoriasNota { get; set; }   //** Propiedad de Navegación que nos lleva a la Data Relacionada CategoriasNota
        public List<int> CategoriaSeleccionadaId { get; set; }
    }
}