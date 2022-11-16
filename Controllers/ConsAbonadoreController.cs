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
    public class ConsAbonadoreController : Controller
    {
        private readonly WebNinnoFelizContext _context;

        public ConsAbonadoreController(WebNinnoFelizContext context)
        {
            _context = context;
        }

        // GET: ConsAbonadore
        public async Task<IActionResult> Index()
        {
            var webNinnoFelizContext = _context.Abonadores.Include(a => a.IdEncargadoNavigation);
            return View(await webNinnoFelizContext.ToListAsync());
        }

        // GET: ConsAbonadore/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var abonadore = await _context.Abonadores
                .Include(a => a.IdEncargadoNavigation)
                .FirstOrDefaultAsync(m => m.IdAbonador == id);
            if (abonadore == null)
            {
                return NotFound();
            }

            return View(abonadore);
        }

        // GET: ConsAbonadore/Create
        public IActionResult Create()
        {
            ViewData["IdEncargado"] = new SelectList(_context.Encargados, "IdEncargado", "IdentificacionEncargado");
            return View();
        }

        // POST: ConsAbonadore/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdAbonador,NumeroCuenta,IdEncargado")] Abonadore abonadore)
        {
            if (ModelState.IsValid)
            {
                _context.Add(abonadore);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdEncargado"] = new SelectList(_context.Encargados, "IdEncargado", "IdentificacionEncargado", abonadore.IdEncargado);
            return View(abonadore);
        }

        // GET: ConsAbonadore/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var abonadore = await _context.Abonadores.FindAsync(id);
            if (abonadore == null)
            {
                return NotFound();
            }
            ViewData["IdEncargado"] = new SelectList(_context.Encargados, "IdEncargado", "IdentificacionEncargado", abonadore.IdEncargado);
            return View(abonadore);
        }

        // POST: ConsAbonadore/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdAbonador,NumeroCuenta,IdEncargado")] Abonadore abonadore)
        {
            if (id != abonadore.IdAbonador)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(abonadore);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AbonadoreExists(abonadore.IdAbonador))
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
            ViewData["IdEncargado"] = new SelectList(_context.Encargados, "IdEncargado", "IdentificacionEncargado", abonadore.IdEncargado);
            return View(abonadore);
        }


        private bool AbonadoreExists(int id)
        {
            return _context.Abonadores.Any(e => e.IdAbonador == id);
        }
    }
}
