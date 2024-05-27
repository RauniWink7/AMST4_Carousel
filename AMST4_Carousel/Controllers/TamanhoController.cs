using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AMST4_Carousel.Context;
using AMST4_Carousel.Models;

namespace AMST4_Carousel.Controllers
{
    public class TamanhoController : Controller
    {
        private readonly ApplicationDataContext _context;

        public TamanhoController(ApplicationDataContext context)
        {
            _context = context;
        }

        // GET: Tamanho
        public async Task<IActionResult> ListTamanho()
        {
            return View(await _context.Tamanho.ToListAsync());
        }

       
        // GET: Tamanho/Create
        public IActionResult AddTamanho()
        {
            return View();
        }

        // POST: Tamanho/Create
        [HttpPost]
        public async Task<IActionResult> AddTamanho(Tamanho tamanho)
        {
            if (ModelState.IsValid)
            {
                tamanho.Id = Guid.NewGuid();
                _context.Add(tamanho);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(ListTamanho));
            }
            return View(tamanho);
        }

        // GET: Tamanho/Edit/5
        public async Task<IActionResult> EditTamanho(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tamanho = await _context.Tamanho.FindAsync(id);
            if (tamanho == null)
            {
                return NotFound();
            }
            return View(tamanho);
        }

        // POST: Tamanho/Edit/5
        [HttpPost]
        public async Task<IActionResult> EditTamanho(Guid id, Tamanho tamanho)
        {
            if (id != tamanho.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tamanho);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TamanhoExists(tamanho.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(ListTamanho));
            }
            return View(tamanho);
        }

        // GET: Tamanho/Delete/5
        public async Task<IActionResult> DeleteTamanho(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tamanho = await _context.Tamanho
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tamanho == null)
            {
                return NotFound();
            }

            return View(tamanho);
        }

        // POST: Tamanho/Delete/5
        [HttpPost, ActionName("DeleteTamanho")]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var tamanho = await _context.Tamanho.FindAsync(id);
            if (tamanho != null)
            {
                _context.Tamanho.Remove(tamanho);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(ListTamanho));
        }

        private bool TamanhoExists(Guid id)
        {
            return _context.Tamanho.Any(e => e.Id == id);
        }
    }
}
