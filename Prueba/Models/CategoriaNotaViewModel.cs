namespace Prueba.Models
{
    public class CategoriaNotaViewModel
    {
        public int Id { get; set; }
        public string Nombre { get; set; }

        public List<NotaViewModel> Notas { get; set; }               //** Propiedad de Navegación que nos lleva a la Data Relacionada Notas
        public List<int> NotasDeCategoria { get; set; }     //** Notas pertenecientes a cada Categoria
    }
}
