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
        private readonly ApplicationDbContext _context;

        public NotasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Listado Notas
        public async Task<IActionResult> Index()
        {
            return View(await _context.Notas.AsNoTracking().ToListAsync()); ;
        }

        // GET: Notas/VerNota
        public async Task<IActionResult> VerNota(int? id)
        {
            if (id is null || _context.Notas is null)
            {
                return NotFound();
            }

            var nota = await _context.Notas
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
            viewModel.CategoriasNota = _context.CategoriasNota.ToList();
            return View(viewModel);
        }


        // POST: Notas/CrearNota
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CrearNota([Bind("Id,Titulo")] Nota nota)
        {
            if (ModelState.IsValid)
            {
                _context.Add(nota);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(nota);
        }



        // GET: Notas/EditarNota
        public async Task<IActionResult> EditarNota(int? id)
        {
            if (id == null || _context.Notas == null)
            {
                return NotFound();
            }

            var nota = await _context.Notas.FindAsync(id);
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
                    _context.Update(nota);
                    await _context.SaveChangesAsync();
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
            if (id == null || _context.Notas == null)
            {
                return NotFound();
            }

            var nota = await _context.Notas
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
            if (_context.Notas == null)
            {
                return Problem("La Entidad 'ApplicationDbContext.Categoria'  es null.");
            }
            var nota = await _context.Notas.FindAsync(id);
            if (nota != null)
            {
                _context.Notas.Remove(nota);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NotaExists(int id)
        {
          return _context.Notas.Any(e => e.Id == id);
        }

        /* ******************

          C A T E G O R I A S

        ********************** */

        // GET: Listado CategoriasNota
        public async Task<IActionResult> Categorias()
        {
            return View(await _context.CategoriasNota.ToListAsync()); 
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
                _context.Add(categoriaNota);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Categorias));
            }
            return View(categoriaNota);
        }

        // GET: Notas/VerCategoria
        public async Task<IActionResult> VerCategoria(int? id)
        {
            if (id == null || _context.CategoriasNota == null)
            {
                return NotFound();
            }

            var categoriaNota = await _context.CategoriasNota
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
            if (id == null || _context.CategoriasNota == null)
            {
                return NotFound();
            }

            var categoriaNota = await _context.CategoriasNota.FindAsync(id);
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
                    _context.Update(categoriaNota);
                    await _context.SaveChangesAsync();
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
            if (id == null || _context.CategoriasNota == null)
            {
                return NotFound();
            }

            var categoriaNota = await _context.CategoriasNota
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
            if (_context.CategoriasNota == null)
            {
                return Problem("La Entidad 'ApplicationDbContext.CategoriaNota'  es null.");
            }
            var categoriaNota = await _context.CategoriasNota.FindAsync(id);
            if (categoriaNota != null)
            {
                _context.CategoriasNota.Remove(categoriaNota);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Categorias));
        }

        private bool CategoriaNotaExiste(int id)
        {
            return _context.CategoriasNota.Any(e => e.Id == id);
        }

    }
}
