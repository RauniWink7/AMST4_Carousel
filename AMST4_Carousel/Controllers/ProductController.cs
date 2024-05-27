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
    public class ProductController : Controller
    {
        private readonly ApplicationDataContext _context;

        public ProductController(ApplicationDataContext context)
        {
            _context = context;
        }

        // GET: Product
        public async Task<IActionResult> ProductList()
        {
            var applicationDataContext = _context.Product.Include(p => p.Category);
            return View(await applicationDataContext.ToListAsync());
        }

        // GET: Product/Details/5
        public async Task<IActionResult> DetailsProduct(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Product
                .Include(p => p.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Product/Create
        public IActionResult AddProduct()
        {
            ViewBag.CategoryList = new SelectList(_context.Category, "Id", "Description");
            ViewData["CategoryId"] = new SelectList(_context.Category, "Id", "Id");
            return View();
        }

        // POST: Product/Create
        [HttpPost]
        public async Task<IActionResult> AddProduct(Product product)
        {
                product.Id = Guid.NewGuid();
                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(ProductList));
            
         
        }

        // GET: Product/Edit/5
        public async Task<IActionResult> EditProduct(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Product.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.Category, "Id", "Id", product.CategoryId);
            return View(product);
        }

        // POST: Product/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public async Task<IActionResult> EditProduct(Guid id, Product product)
        {
            if (id != product.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(ProductList));
            }
            ViewData["CategoryId"] = new SelectList(_context.Category, "Id", "Id", product.CategoryId);
            return View(product);
        }

        // GET: Product/Delete/5
        public async Task<IActionResult> DeleteProduct(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Product
                .Include(p => p.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Product/Delete/5
        [HttpPost, ActionName("DeleteProduct")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var product = await _context.Product.FindAsync(id);
            if (product != null)
            {
                _context.Product.Remove(product);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(ProductList));
        }

        private bool ProductExists(Guid id)
        {
            return _context.Product.Any(e => e.Id == id);
        }
    }
}
