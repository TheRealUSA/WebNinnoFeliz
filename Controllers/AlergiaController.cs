using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using WebNinnoFeliz.Data;
using WebNinnoFeliz.Models;

namespace WebNinnoFeliz.Controllers
{
    public class AlergiaController : Controller
    {
        private readonly WebNinnoFelizContext _context;

        public AlergiaController(WebNinnoFelizContext context)
        {
            _context = context;
        }

        // GET: Alergia
        public async Task<IActionResult> Index()
        {
            var webNinnoFelizContext = _context.Alergias.Include(a => a.IdTipoAlergiaNavigation);
            return View(await webNinnoFelizContext.ToListAsync());
        }

        // GET: Alergia/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var alergia = await _context.Alergias
                .Include(a => a.IdTipoAlergiaNavigation)
                .FirstOrDefaultAsync(m => m.IdAlergia == id);
            if (alergia == null)
            {
                return NotFound();
            }

            return View(alergia);
        }

        // GET: Alergia/Create
        public IActionResult Create()
        {
            ViewData["IdTipoAlergia"] = new SelectList(_context.TipoAlergias, "IdTipoAlergia", "NombreTipoAlergia");
            return View();
        }

        // POST: Alergia/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdAlergia,NombreAlergia,IdTipoAlergia")] Alergia alergia)
        {
            if (ModelState.IsValid)
            {
                SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
                SqlCommand cmd = conn.CreateCommand();
                conn.Open();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "sp_IngresarAlergias";
                cmd.Parameters.Add("@nombreAlergia", System.Data.SqlDbType.VarChar, 20).Value = alergia.NombreAlergia;
                cmd.Parameters.Add("@idTipoAlergia", System.Data.SqlDbType.Int).Value = alergia.IdTipoAlergia;
                await cmd.ExecuteNonQueryAsync();
                conn.Close();
                //_context.Add(alergia);
                //await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdTipoAlergia"] = new SelectList(_context.TipoAlergias, "IdTipoAlergia", "NombreTipoAlergia", alergia.IdTipoAlergia);
            return View(alergia);
        }

        // GET: Alergia/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var alergia = await _context.Alergias.FindAsync(id);
            if (alergia == null)
            {
                return NotFound();
            }
            ViewData["IdTipoAlergia"] = new SelectList(_context.TipoAlergias, "IdTipoAlergia", "NombreTipoAlergia", alergia.IdTipoAlergia);
            return View(alergia);
        }

        // POST: Alergia/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdAlergia,NombreAlergia,IdTipoAlergia")] Alergia alergia)
        {
            if (id != alergia.IdAlergia)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
                    SqlCommand cmd = conn.CreateCommand();
                    conn.Open();
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.CommandText = "sp_ModificarAlergias";
                    cmd.Parameters.Add("@idAlergia", System.Data.SqlDbType.Int).Value = alergia.IdAlergia;
                    cmd.Parameters.Add("@nombreAlergia", System.Data.SqlDbType.VarChar, 20).Value = alergia.NombreAlergia;
                    cmd.Parameters.Add("@idTipoAlergia", System.Data.SqlDbType.Int).Value = alergia.IdTipoAlergia;
                    await cmd.ExecuteNonQueryAsync();
                    conn.Close();
                    //_context.Update(alergia);
                    //await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AlergiaExists(alergia.IdAlergia))
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
            ViewData["IdTipoAlergia"] = new SelectList(_context.TipoAlergias, "IdTipoAlergia", "NombreTipoAlergia", alergia.IdTipoAlergia);
            return View(alergia);
        }

        // GET: Alergia/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var alergia = await _context.Alergias
                .Include(a => a.IdTipoAlergiaNavigation)
                .FirstOrDefaultAsync(m => m.IdAlergia == id);
            if (alergia == null)
            {
                return NotFound();
            }

            return View(alergia);
        }

        // POST: Alergia/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
            SqlCommand cmd = conn.CreateCommand();
            conn.Open();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "sp_EliminarAlergias";
            cmd.Parameters.Add("@idAlergia", System.Data.SqlDbType.Int).Value = id;
            await cmd.ExecuteNonQueryAsync();
            conn.Close();
            //var alergia = await _context.Alergias.FindAsync(id);
            //_context.Alergias.Remove(alergia);
            //await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AlergiaExists(int id)
        {
            return _context.Alergias.Any(e => e.IdAlergia == id);
        }
    }
}
