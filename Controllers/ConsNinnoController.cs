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
    public class ConsNinnoController : Controller
    {
        private readonly WebNinnoFelizContext _context;

        public ConsNinnoController(WebNinnoFelizContext context)
        {
            _context = context;
        }

        // GET: ConsNinno
        public async Task<IActionResult> Index()
        {
            var webNinnoFelizContext = _context.Ninnos.Include(n => n.IdGeneroNavigation);
            return View(await webNinnoFelizContext.ToListAsync());
        }

        // GET: ConsNinno/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ninno = await _context.Ninnos
                .Include(n => n.IdGeneroNavigation)
                .FirstOrDefaultAsync(m => m.IdNinno == id);
            if (ninno == null)
            {
                return NotFound();
            }

            return View(ninno);
        }

        // GET: ConsNinno/Create
        public IActionResult Create()
        {
            ViewData["IdGenero"] = new SelectList(_context.Generos, "IdGenero", "DetalleGen");
            return View();
        }

        // POST: ConsNinno/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdNinno,IdentificacionNinno,NombreNinno,Apell1Ninno,Apell2Ninno,FechaNacimiento,DireccionNinno,IdGenero")] Ninno ninno)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ninno);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdGenero"] = new SelectList(_context.Generos, "IdGenero", "DetalleGen", ninno.IdGenero);
            return View(ninno);
        }

        // GET: ConsNinno/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ninno = await _context.Ninnos.FindAsync(id);
            if (ninno == null)
            {
                return NotFound();
            }
            ViewData["IdGenero"] = new SelectList(_context.Generos, "IdGenero", "DetalleGen", ninno.IdGenero);
            return View(ninno);
        }

        // POST: ConsNinno/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdNinno,IdentificacionNinno,NombreNinno,Apell1Ninno,Apell2Ninno,FechaNacimiento,DireccionNinno,IdGenero")] Ninno ninno)
        {
            if (id != ninno.IdNinno)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ninno);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NinnoExists(ninno.IdNinno))
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
            ViewData["IdGenero"] = new SelectList(_context.Generos, "IdGenero", "DetalleGen", ninno.IdGenero);
            return View(ninno);
        }
        private bool NinnoExists(int id)
        {
            return _context.Ninnos.Any(e => e.IdNinno == id);
        }
    }
}
