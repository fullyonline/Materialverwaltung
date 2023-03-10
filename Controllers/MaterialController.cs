using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Materialverwaltung.Data;
using Materialverwaltung.Models;

namespace Materialverwaltung.Controllers
{
    public class MaterialController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MaterialController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Material
        public async Task<IActionResult> Index()
        {
            return _context.Materials != null ? 
                          View(await _context.Materials.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Materials'  is null.");
        }

        // GET: Material/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Materials == null)
            {
                return NotFound();
            }

            var material = await _context.Materials
                .FirstOrDefaultAsync(m => m.Id == id);
            if (material == null)
            {
                return NotFound();
            }

            return View(material);
        }

        // GET: Material/Create
        public IActionResult Create()
        {
            if (_context == null || !_context.Materialgruppe.Any())
            {
                return CustomerWarning();
            }
            ViewBag.MaterialgruppeNameList = new SelectList(_context.Materialgruppe, "Id", "Name", _context.Materialgruppe.FirstOrDefault().Name);
            return View();
        }

        // POST: Material/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Material material)
        {
            material.Materialgruppe = _context.Materialgruppe.FirstOrDefault(x => x.Id == material.Materialgruppe.Id);
            if (material.Materialgruppe != null)
            {
                material.MaterialgruppeId = material.Materialgruppe.Id;
            }
            // after we added the Materialgruppe it has to be revalidated
            ModelState.Clear();
            TryValidateModel(material);
            if (ModelState.IsValid)
            {
                _context.Add(material);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(material);
        }

        public IActionResult CustomerWarning()
        {
            ViewData["Warning"] = "Es muss mindestens eine Materialgruppe angelegt werden.";
            return View("_CustomerWarning");
        }

        // GET: Material/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Materials == null)
            {
                return NotFound();
            }

            ViewBag.MaterialgruppeNameList = new SelectList(_context.Materialgruppe, "Id", "Name", _context.Materialgruppe.FirstOrDefault().Name);

            var material = await _context.Materials.FindAsync(id);
            if (material == null)
            {
                return NotFound();
            }
            material.Materialgruppe = _context.Materialgruppe.Where(x => x.Id == material.MaterialgruppeId).FirstOrDefault();
            return View(material);
        }

        // POST: Material/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Material material)
        {
            if (id != material.Id)
            {
                return NotFound();
            }

            material.Materialgruppe = _context.Materialgruppe.FirstOrDefault(x => x.Id == material.Materialgruppe.Id);
            if (material.Materialgruppe != null)
            {
                material.MaterialgruppeId = material.Materialgruppe.Id;
            }
            // after we added the Materialgruppe it has to be revalidated
            ModelState.Clear();
            TryValidateModel(material);

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(material);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MaterialExists(material.Id))
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
            return View(material);
        }

        // GET: Material/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Materials == null)
            {
                return NotFound();
            }

            var material = await _context.Materials
                .FirstOrDefaultAsync(m => m.Id == id);
            if (material == null)
            {
                return NotFound();
            }

            return View(material);
        }

        // POST: Material/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Materials == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Materials'  is null.");
            }
            var material = await _context.Materials.FindAsync(id);
            if (material != null)
            {
                _context.Materials.Remove(material);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MaterialExists(int id)
        {
          return (_context.Materials?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
