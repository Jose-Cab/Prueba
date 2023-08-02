using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Prueba;
using Prueba.Models;
using Prueba.Entidades;

namespace Prueba.Controllers
{
    public class NotasController : Controller
    {
        private readonly ApplicationDbContext context;

        public NotasController(ApplicationDbContext context)
        {
            this.context = context;
        }

        // GET: Listado Notas
        // Muestra un listado de TODAS LAS NOTAS a no ser que enviemos
        // /Notas?categoriaId=5 en cuyo caso mostrará solo las notas que pertenecen a la categoria con Id 5
        [HttpGet]
        public async Task<IActionResult> Index(int? categoriaId=null)
        {
            IQueryable<Nota> query = context.Notas.Include(c => c.CategoriasNota);

            if (categoriaId != null)
            {
                query = query.Where(n => n.CategoriasNota.Any(cn => cn.Id == categoriaId));
            }
            var notas = await query.AsNoTracking().ToListAsync();

            //var notas = await context.Notas.Include(c => c.CategoriasNota).AsNoTracking().ToListAsync();
            var notaViewModel = notas.Select(n => new NotaViewModel
            {
                Id = n.Id,
                Titulo = n.Titulo,
                CategoriasNota = n.CategoriasNota.ToList(),
                CategoriaSeleccionadaId = n.CategoriasNota
                    .Select(cn => cn.Id)
                    .ToList()
            }).ToList();

            return View(notaViewModel);
        }

        // GET: Notas/VerNota
        public async Task<IActionResult> VerNota(int? id, NotaViewModel notaViewModel)
        {
            if (id is null || context.Notas is null)
            {
                return NotFound();
            }

            var nota = await context.Notas
                .Include(c => c.CategoriasNota)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (nota is null)
            {
                return NotFound();
            }

            notaViewModel.Id = nota.Id;
            notaViewModel.Titulo = nota.Titulo;
            notaViewModel.CategoriasNota = nota.CategoriasNota.ToList();
            notaViewModel.CategoriaSeleccionadaId = nota.CategoriasNota
                    .Select(cn => cn.Id)
                    .ToList();
            return View(notaViewModel);
        }


        // GET: Notas/CrearNota
        public IActionResult CrearNota(NotaViewModel notaViewModel)
        {
            notaViewModel.CategoriasNota = context.CategoriasNota.ToList();
            return View(notaViewModel);
        }

        // POST: Notas/CrearNota
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CrearNota(CrearNotaViewModel crearNotaViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(crearNotaViewModel);
            }

            var nota = new Nota();
            nota.Titulo = crearNotaViewModel.Titulo;

            foreach (var id in crearNotaViewModel.CategoriaSeleccionadaId)
            {
                var categoria = await context.CategoriasNota.FindAsync(id);
                if (categoria != null)
                {
                    nota.CategoriasNota.Add(categoria);
                }
            }

            context.Add(nota);
            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Notas/EditarNota
        public async Task<IActionResult> EditarNota(int? id, NotaViewModel notaViewModel)
        {
            if (id == null || context.Notas == null)
            {
                return NotFound();
            }
            notaViewModel.CategoriasNota = context.CategoriasNota.ToList();
            
            var nota = await context.Notas
                .Include(c => c.CategoriasNota)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (nota == null)
            {
                return NotFound();
            }
            notaViewModel.Titulo = nota.Titulo.ToString();
            notaViewModel.CategoriasNota = context.CategoriasNota.ToList();
            notaViewModel.CategoriaSeleccionadaId = nota.CategoriasNota
                .Select(cn => cn.Id)
                .ToList();
            return View(notaViewModel);

        }

        // POST: Notas/EditarNota
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditarNota(NotaViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.CategoriasNota = context.CategoriasNota.ToList();  // Make sure to resend the list of CategoriasNota
                return View(viewModel);
            }

            var nota = await context.Notas.Include(c => c.CategoriasNota).FirstOrDefaultAsync(n => n.Id == viewModel.Id);
            if (nota == null)
            {
                return NotFound();
            }

            nota.Titulo = viewModel.Titulo;
            nota.CategoriasNota.Clear();  // Borra Categorias actuales

            // Para cada ID de CategoriaNota seleccionada, agrega la CategoriaNota relacionada a la Nota
            foreach (var id in viewModel.CategoriaSeleccionadaId)
            {
                var categoria = await context.CategoriasNota.FindAsync(id);
                if (categoria != null)
                {
                    nota.CategoriasNota.Add(categoria);
                }
            }

            try
            {
                context.Update(nota);
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NotaExists(nota.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: Notas/BorrarNota
        public async Task<IActionResult> BorrarNota(int? id, NotaViewModel notaViewModel)
        {
            if (id == null || context.Notas == null)
            {
                return NotFound();
            }

            var nota = await context.Notas
                .FirstOrDefaultAsync(m => m.Id == id);
            if (nota == null)
            {
                return NotFound();
            }

            notaViewModel.Id = nota.Id;
            notaViewModel.Titulo= nota.Titulo;
            notaViewModel.CategoriasNota = nota.CategoriasNota.ToList();
            notaViewModel.CategoriaSeleccionadaId = nota.CategoriasNota
                .Select(cn => cn.Id)
                .ToList();
            return View(notaViewModel);
        }

        // POST: Notas/BorrarNota
        [HttpPost, ActionName("BorrarNota")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (context.Notas == null)
            {
                return Problem("La Entidad 'ApplicationDbContext.Categoria'  es null.");
            }
            var nota = await context.Notas.FindAsync(id);
            if (nota != null)
            {
                context.Notas.Remove(nota);
            }
            
            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        // Se utiliza en EditarNota
        private bool NotaExists(int id)
        {
          return context.Notas.Any(e => e.Id == id);
        }

        /* ******************

          C A T E G O R I A S

        ********************** */


        // GET: Listado CategoriasNota
        // Muestra un listado de TODAS LAS CATEGORIA a no ser que enviemos
        // /Categorias?notaId=2 en cuyo caso mostrará solo las Categorías que estén relacionada con el Id de la Nota 2

        public async Task<IActionResult> Categorias(int? notaId = null)
        {
            IQueryable<CategoriaNota> query = context.CategoriasNota.Include(n => n.Notas);
            if (notaId != null)
            {
                query = query.Where(c => c.Notas.Any(n => n.Id == notaId));
            }
            var categoriasNotas = await query.AsNoTracking().ToListAsync();

            var categoriaNotaViewModel = categoriasNotas.Select(c => new CategoriaNotaViewModel
            {
                Id = c.Id,
                Nombre = c.Nombre,
                NotasDeCategoria = c.Notas.Select(n => n.Id).ToList()
            }).ToList();
            return View(categoriaNotaViewModel);
        }

        // GET: Notas/VerCategoria
        public async Task<IActionResult> VerCategoria(int? id)
        {
            if (id == null || context.CategoriasNota == null)
            {
                return NotFound();
            }

            var categoriaNota = await context.CategoriasNota
                .Include(c => c.Notas)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (categoriaNota == null)
            {
                return NotFound();
            }

            var categoriaNotaViewModel = new CategoriaNotaViewModel
            {
                Id = categoriaNota.Id,
                Nombre = categoriaNota.Nombre,
                Notas = categoriaNota.Notas.Select(n => new NotaViewModel
                {
                    Id = n.Id,
                    Titulo = n.Titulo,
                }).ToList()
            };

            return View(categoriaNotaViewModel);
        }


        // GET: Notas/CrearCategoria
        public IActionResult CrearCategoria()
        {
            return View();
        }

        // POST: Notas/CrearCategoria
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CrearCategoria([Bind("Id,Nombre")] CategoriaNotaViewModel categoriaNotaViewModel)
        {
            if (ModelState.IsValid)
            {
                var categoriaNota = new CategoriaNota
                {
                    Id = categoriaNotaViewModel.Id,
                    Nombre = categoriaNotaViewModel.Nombre
                };

                context.Add(categoriaNota);
                await context.SaveChangesAsync();
                return RedirectToAction(nameof(Categorias));
            }
            return View(categoriaNotaViewModel);
        }


        // GET: Notas/EditarCategoria
        public async Task<IActionResult> EditarCategoria(int? id, CategoriaNotaViewModel categoriaNotaViewModel)
        {
            if (id == null || context.CategoriasNota == null)
            {
                return NotFound();
            }

            var categoriaNota = await context.CategoriasNota.FindAsync(id);
            if (categoriaNota == null)
            {
                return NotFound();
            }

            categoriaNotaViewModel.Id = categoriaNota.Id;
            categoriaNotaViewModel.Nombre = categoriaNota.Nombre;
            return View(categoriaNotaViewModel);
        }

        // POST: Notas/EditarCategoriaNota
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditarCategoria(int id,  CategoriaNotaViewModel categoriaNotaViewModel)
        {
            if (id != categoriaNotaViewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var categoriaNota = new CategoriaNota
                    {
                        Id = categoriaNotaViewModel.Id,
                        Nombre = categoriaNotaViewModel.Nombre
                    };

                    context.Update(categoriaNota);
                    await context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoriaNotaExiste(categoriaNotaViewModel.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Categorias));
            }
            return View(categoriaNotaViewModel);
        }


        // GET: Notas/BorrarCategoria
        public async Task<IActionResult> BorrarCategoria(int? id, CategoriaNotaViewModel categoriaNotaViewModel)
        {
            if (id == null || context.CategoriasNota == null)
            {
                return NotFound();
            }

            var categoriaNota = await context.CategoriasNota
                .FirstOrDefaultAsync(m => m.Id == id);
            if (categoriaNota == null)
            {
                return NotFound();
            }

            categoriaNotaViewModel.Id= categoriaNota.Id;
            categoriaNotaViewModel.Nombre = categoriaNota.Nombre;

            return View(categoriaNotaViewModel);
        }

        // POST: Notas/BorrarNota
        [HttpPost, ActionName("BorrarCategoria")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConfirmarBorrado(int id)
        {
            if (context.CategoriasNota == null)
            {
                return Problem("La Entidad 'ApplicationDbContext.CategoriaNota'  es null.");
            }
            var categoriaNota = await context.CategoriasNota.FindAsync(id);
            if (categoriaNota != null)
            {
                context.CategoriasNota.Remove(categoriaNota);
            }

            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Categorias));
        }

        // Se utiliza en EditarCategoria
        private bool CategoriaNotaExiste(int id)
        {
            return context.CategoriasNota.Any(e => e.Id == id);
        }

    }
}
