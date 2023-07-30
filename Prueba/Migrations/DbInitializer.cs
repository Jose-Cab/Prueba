using System.Linq;
using System.Collections.Generic;
using Prueba.Entidades;
using Prueba.Migrations;

namespace Prueba
{
    public static class DbInitializer
    {
        public static void Initialize(ApplicationDbContext context)
        {
            context.Database.EnsureCreated();

            // Si hay alguna categoría, no hacer nada
            if (context.CategoriasNota.Any())
            {
                return;
            }

            // Crear categorías
            var categorias = new CategoriaNota[]
            {
                new CategoriaNota{Nombre = "Categoria 1"},
                new CategoriaNota{Nombre = "Categoria 2"},
                new CategoriaNota{Nombre = "Categoria 3"},
                new CategoriaNota{Nombre = "Categoria 4"},
                new CategoriaNota{Nombre = "Categoria 5"},
                new CategoriaNota{Nombre = "Categoria 6"},

            };

            foreach (CategoriaNota c in categorias)
            {
                context.CategoriasNota.Add(c);
            }
            context.SaveChanges();

            // Crear notas
            var notas = new Nota[]
            {
                new Nota{Titulo = "Nota 1"},
                new Nota{Titulo = "Nota 2"},
                new Nota{Titulo = "Nota 3"},
                new Nota{Titulo = "Nota 4"},

            };

            foreach (Nota n in notas)
            {
                context.Notas.Add(n);
            }
            context.SaveChanges();

            // Añadir notas a las categorías
            var categoriaNota1 = context.CategoriasNota.Single(c => c.Nombre == "Categoria 1");
            categoriaNota1.Notas = new List<Nota> { notas[0], notas[1] };
            context.CategoriasNota.Update(categoriaNota1);

            var categoriaNota2 = context.CategoriasNota.Single(c => c.Nombre == "Categoria 2");
            categoriaNota2.Notas = new List<Nota> { notas[0] };
            context.CategoriasNota.Update(categoriaNota2);

            var categoriaNota3= context.CategoriasNota.Single(c => c.Nombre == "Categoria 3");
            categoriaNota3.Notas = new List<Nota> { notas[4], notas[2], notas[5] };
            context.CategoriasNota.Update(categoriaNota3);

            var categoriaNota5 = context.CategoriasNota.Single(c => c.Nombre == "Categoria 5");
            categoriaNota5.Notas = new List<Nota> { notas[1], notas[3], notas[4] };
            context.CategoriasNota.Update(categoriaNota5);

            //// Añadir categorías a las notas
            //var nota1 = context.Notas.Single(n => n.Titulo == "Nota 1");
            //nota1.CategoriasNota = new List<CategoriaNota> { categorias[0], categorias[1] };
            //context.Notas.Update(nota1);

            //var nota2 = context.Notas.Single(n => n.Titulo == "Nota 2");
            //nota2.CategoriasNota = new List<CategoriaNota> { categorias[0] };
            //context.Notas.Update(nota2);

            //var nota3 = context.Notas.Single(n => n.Titulo == "Nota 3");
            //nota3.CategoriasNota = new List<CategoriaNota> { categorias[2], categorias[5] };
            //context.Notas.Update(nota3);

            //var nota4 = context.Notas.Single(n => n.Titulo == "Nota 4");
            //nota4.CategoriasNota = new List<CategoriaNota> { categorias[1], categorias[3], categorias[2], categorias[5] };
            //context.Notas.Update(nota4);


            context.SaveChanges();
        }
    }
}
