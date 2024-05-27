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
    public class CategoryController : Controller
    {
        private readonly ApplicationDataContext _context;

        public CategoryController(ApplicationDataContext context)
        {
            _context = context;
        }

        // GET: Category
        public async Task<IActionResult> CategoryList()
        {
            return View(await _context.Category.ToListAsync());
        }

        // GET: Category/Details/5
        public async Task<IActionResult> DetailsCategory(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _context.Category
                .FirstOrDefaultAsync(m => m.Id == id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // GET: Category/Create
        public IActionResult AddCategory()
        {
            return View();
        }

        // POST: Category/Create
        [HttpPost]
        public async Task<IActionResult> AddCategory( Category category)
        {
            
                category.Id = Guid.NewGuid();
                _context.Add(category);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(CategoryList));
         
        }

        // GET: Category/Edit/5
        public async Task<IActionResult> EditCategory(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _context.Category.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        // POST: Category/Edit/5
        [HttpPost]
        public async Task<IActionResult> EditCategory(Guid id, Category category)
        {
            if (id != category.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(category);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoryExists(category.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(CategoryList));
            }
            return View(category);
        }

        // GET: Category/Delete/5
        public async Task<IActionResult> DeleteCategory(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _context.Category
                .FirstOrDefaultAsync(m => m.Id == id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // POST: Category/Delete/5
        [HttpPost, ActionName("DeleteCategory")]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var category = await _context.Category.FindAsync(id);
            if (category != null)
            {
                _context.Category.Remove(category);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(CategoryList));
        }

        private bool CategoryExists(Guid id)
        {
            return _context.Category.Any(e => e.Id == id);
        }
    }
}
