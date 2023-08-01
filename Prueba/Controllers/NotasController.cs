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
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await context.Notas.AsNoTracking().ToListAsync()); ;
        }

        // GET: Notas/VerNota
        public async Task<IActionResult> VerNota(int? id)
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

            return View(nota);
        }


        // GET: Notas/CrearNota
        public IActionResult CrearNota()
        {
            var viewModel = new NotaViewModel ();
            viewModel.CategoriasNota = context.CategoriasNota.ToList();
            return View(viewModel);
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
        public async Task<IActionResult> EditarNota(int? id)
        {
            if (id == null || context.Notas == null)
            {
                return NotFound();
            }

            var nota = await context.Notas.FindAsync(id);
            if (nota == null)
            {
                return NotFound();
            }
            return View(nota);
        }

        // POST: Notas/EditarNota
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditarNota(int id, [Bind("Id,Titulo")] Nota nota)
        {
            if (id != nota.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
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
            return View(nota);
        }

        // GET: Notas/BorrarNota
        public async Task<IActionResult> BorrarNota(int? id)
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

            return View(nota);
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

        private bool NotaExists(int id)
        {
          return context.Notas.Any(e => e.Id == id);
        }

        /* ******************

          C A T E G O R I A S

        ********************** */

        // GET: Listado CategoriasNota
        public async Task<IActionResult> Categorias()
        {
            return View(await context.CategoriasNota.ToListAsync()); 
        }

        // GET: Notas/CrearCategoria
        public IActionResult CrearCategoria()
        {
            return View();
        }

        // POST: Notas/CrearCategoria
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CrearCategoria([Bind("Id,Nombre")] CategoriaNota categoriaNota)
        {
            if (ModelState.IsValid)
            {
                context.Add(categoriaNota);
                await context.SaveChangesAsync();
                return RedirectToAction(nameof(Categorias));
            }
            return View(categoriaNota);
        }

        // GET: Notas/VerCategoria
        public async Task<IActionResult> VerCategoria(int? id)
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

            return View(categoriaNota);
        }


        // GET: Notas/EditarCategoria
        public async Task<IActionResult> EditarCategoria(int? id)
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
            return View(categoriaNota);
        }

        // POST: Notas/EditarCategoriaNota
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditarCategoria(int id, [Bind("Id,Nombre")] CategoriaNota categoriaNota)
        {
            if (id != categoriaNota.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    context.Update(categoriaNota);
                    await context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoriaNotaExiste(categoriaNota.Id))
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
            return View(categoriaNota);
        }


        // GET: Notas/BorrarCategoria
        public async Task<IActionResult> BorrarCategoria(int? id)
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

            return View(categoriaNota);
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

        private bool CategoriaNotaExiste(int id)
        {
            return context.CategoriasNota.Any(e => e.Id == id);
        }

    }
}
