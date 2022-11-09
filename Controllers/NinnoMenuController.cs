using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebNinnoFeliz.Data;
using WebNinnoFeliz.Models;

namespace WebNinnoFeliz.Controllers
{
    public class NinnoMenuController : Controller
    {
        private readonly WebNinnoFelizContext _context;

        public NinnoMenuController(WebNinnoFelizContext context)
        {
            _context = context;
        }

        // GET: NinnoMenu
        public async Task<IActionResult> Index()
        {
            var webNinnoFelizContext = _context.NinnoMenus.Include(n => n.IdNinnoNavigation).Include(n => n.IdNumeroMenuNavigation);
            return View(await webNinnoFelizContext.ToListAsync());
        }

        // GET: NinnoMenu/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ninnoMenu = await _context.NinnoMenus
                .Include(n => n.IdNinnoNavigation)
                .Include(n => n.IdNumeroMenuNavigation)
                .FirstOrDefaultAsync(m => m.IdNinnoMenu == id);
            if (ninnoMenu == null)
            {
                return NotFound();
            }

            return View(ninnoMenu);
        }

        // GET: NinnoMenu/Create
        public IActionResult Create()
        {
            ViewData["IdNinno"] = new SelectList(_context.Ninnos, "IdNinno", "Apell1Ninno");
            ViewData["IdNumeroMenu"] = new SelectList(_context.Menus, "IdNumeroMenu", "NombreMenu");
            return View();
        }

        // POST: NinnoMenu/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdNinnoMenu,FechaConsumido,IdNinno,IdNumeroMenu")] NinnoMenu ninnoMenu)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ninnoMenu);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdNinno"] = new SelectList(_context.Ninnos, "IdNinno", "Apell1Ninno", ninnoMenu.IdNinno);
            ViewData["IdNumeroMenu"] = new SelectList(_context.Menus, "IdNumeroMenu", "NombreMenu", ninnoMenu.IdNumeroMenu);
            return View(ninnoMenu);
        }

        // GET: NinnoMenu/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ninnoMenu = await _context.NinnoMenus.FindAsync(id);
            if (ninnoMenu == null)
            {
                return NotFound();
            }
            ViewData["IdNinno"] = new SelectList(_context.Ninnos, "IdNinno", "Apell1Ninno", ninnoMenu.IdNinno);
            ViewData["IdNumeroMenu"] = new SelectList(_context.Menus, "IdNumeroMenu", "NombreMenu", ninnoMenu.IdNumeroMenu);
            return View(ninnoMenu);
        }

        // POST: NinnoMenu/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdNinnoMenu,FechaConsumido,IdNinno,IdNumeroMenu")] NinnoMenu ninnoMenu)
        {
            if (id != ninnoMenu.IdNinnoMenu)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ninnoMenu);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NinnoMenuExists(ninnoMenu.IdNinnoMenu))
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
            ViewData["IdNinno"] = new SelectList(_context.Ninnos, "IdNinno", "Apell1Ninno", ninnoMenu.IdNinno);
            ViewData["IdNumeroMenu"] = new SelectList(_context.Menus, "IdNumeroMenu", "NombreMenu", ninnoMenu.IdNumeroMenu);
            return View(ninnoMenu);
        }

        // GET: NinnoMenu/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ninnoMenu = await _context.NinnoMenus
                .Include(n => n.IdNinnoNavigation)
                .Include(n => n.IdNumeroMenuNavigation)
                .FirstOrDefaultAsync(m => m.IdNinnoMenu == id);
            if (ninnoMenu == null)
            {
                return NotFound();
            }

            return View(ninnoMenu);
        }

        // POST: NinnoMenu/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ninnoMenu = await _context.NinnoMenus.FindAsync(id);
            _context.NinnoMenus.Remove(ninnoMenu);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NinnoMenuExists(int id)
        {
            return _context.NinnoMenus.Any(e => e.IdNinnoMenu == id);
        }
    }
}
