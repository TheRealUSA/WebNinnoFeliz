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
    public class ConsNinnoAlergiaIngredienteController : Controller
    {
        private readonly WebNinnoFelizContext _context;

        public ConsNinnoAlergiaIngredienteController(WebNinnoFelizContext context)
        {
            _context = context;
        }

        // GET: ConsNinnoAlergiaIngrediente
        public async Task<IActionResult> Index()
        {
            var webNinnoFelizContext = _context.NinnoAlergiaIngredientes.Include(n => n.IdAlergiaNavigation).Include(n => n.IdIngredienteNavigation).Include(n => n.IdNinnoNavigation);
            return View(await webNinnoFelizContext.ToListAsync());
        }

        // GET: ConsNinnoAlergiaIngrediente/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ninnoAlergiaIngrediente = await _context.NinnoAlergiaIngredientes
                .Include(n => n.IdAlergiaNavigation)
                .Include(n => n.IdIngredienteNavigation)
                .Include(n => n.IdNinnoNavigation)
                .FirstOrDefaultAsync(m => m.IdNinnoAlergiaIngrediente == id);
            if (ninnoAlergiaIngrediente == null)
            {
                return NotFound();
            }

            return View(ninnoAlergiaIngrediente);
        }

        // GET: ConsNinnoAlergiaIngrediente/Create
        public IActionResult Create()
        {
            ViewData["IdAlergia"] = new SelectList(_context.Alergias, "IdAlergia", "NombreAlergia");
            ViewData["IdIngrediente"] = new SelectList(_context.Ingredientes, "IdIngrediente", "NombreIngrediente");
            ViewData["IdNinno"] = new SelectList(_context.Ninnos, "IdNinno", "IdentificacionNinno");
            return View();
        }

        // POST: ConsNinnoAlergiaIngrediente/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdNinnoAlergiaIngrediente,IdAlergia,IdIngrediente,IdNinno")] NinnoAlergiaIngrediente ninnoAlergiaIngrediente)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ninnoAlergiaIngrediente);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdAlergia"] = new SelectList(_context.Alergias, "IdAlergia", "NombreAlergia", ninnoAlergiaIngrediente.IdAlergia);
            ViewData["IdIngrediente"] = new SelectList(_context.Ingredientes, "IdIngrediente", "NombreIngrediente", ninnoAlergiaIngrediente.IdIngrediente);
            ViewData["IdNinno"] = new SelectList(_context.Ninnos, "IdNinno", "IdentificacionNinno", ninnoAlergiaIngrediente.IdNinno);
            return View(ninnoAlergiaIngrediente);
        }

        // GET: ConsNinnoAlergiaIngrediente/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ninnoAlergiaIngrediente = await _context.NinnoAlergiaIngredientes.FindAsync(id);
            if (ninnoAlergiaIngrediente == null)
            {
                return NotFound();
            }
            ViewData["IdAlergia"] = new SelectList(_context.Alergias, "IdAlergia", "NombreAlergia", ninnoAlergiaIngrediente.IdAlergia);
            ViewData["IdIngrediente"] = new SelectList(_context.Ingredientes, "IdIngrediente", "NombreIngrediente", ninnoAlergiaIngrediente.IdIngrediente);
            ViewData["IdNinno"] = new SelectList(_context.Ninnos, "IdNinno", "IdentificacionNinno", ninnoAlergiaIngrediente.IdNinno);
            return View(ninnoAlergiaIngrediente);
        }

        // POST: ConsNinnoAlergiaIngrediente/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdNinnoAlergiaIngrediente,IdAlergia,IdIngrediente,IdNinno")] NinnoAlergiaIngrediente ninnoAlergiaIngrediente)
        {
            if (id != ninnoAlergiaIngrediente.IdNinnoAlergiaIngrediente)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ninnoAlergiaIngrediente);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NinnoAlergiaIngredienteExists(ninnoAlergiaIngrediente.IdNinnoAlergiaIngrediente))
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
            ViewData["IdAlergia"] = new SelectList(_context.Alergias, "IdAlergia", "NombreAlergia", ninnoAlergiaIngrediente.IdAlergia);
            ViewData["IdIngrediente"] = new SelectList(_context.Ingredientes, "IdIngrediente", "NombreIngrediente", ninnoAlergiaIngrediente.IdIngrediente);
            ViewData["IdNinno"] = new SelectList(_context.Ninnos, "IdNinno", "IdentificacionNinno", ninnoAlergiaIngrediente.IdNinno);
            return View(ninnoAlergiaIngrediente);
        }

        private bool NinnoAlergiaIngredienteExists(int id)
        {
            return _context.NinnoAlergiaIngredientes.Any(e => e.IdNinnoAlergiaIngrediente == id);
        }
    }
}
