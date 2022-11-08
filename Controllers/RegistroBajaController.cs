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
    public class RegistroBajaController : Controller
    {
        private readonly WebNinnoFelizContext _context;

        public RegistroBajaController(WebNinnoFelizContext context)
        {
            _context = context;
        }

        // GET: RegistroBaja
        public async Task<IActionResult> Index()
        {
            var webNinnoFelizContext = _context.RegistroBajas.Include(r => r.IdNinnoNavigation);
            return View(await webNinnoFelizContext.ToListAsync());
        }

        // GET: RegistroBaja/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var registroBaja = await _context.RegistroBajas
                .Include(r => r.IdNinnoNavigation)
                .FirstOrDefaultAsync(m => m.IdRegistroBaja == id);
            if (registroBaja == null)
            {
                return NotFound();
            }

            return View(registroBaja);
        }

        // GET: RegistroBaja/Create
        public IActionResult Create()
        {
            ViewData["IdNinno"] = new SelectList(_context.Ninnos, "IdNinno", "IdentificacionNinno");
            return View();
        }

        // POST: RegistroBaja/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdRegistroBaja,FechaBaja,IdNinno")] RegistroBaja registroBaja)
        {
            if (ModelState.IsValid)
            {
                SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
                SqlCommand cmd = conn.CreateCommand();
                conn.Open();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "sp_IngresarRegistroBajas";
                cmd.Parameters.Add("@fechaBaja", System.Data.SqlDbType.Date).Value = registroBaja.FechaBaja;
                cmd.Parameters.Add("@idNinno", System.Data.SqlDbType.Int).Value = registroBaja.IdNinno;
                await cmd.ExecuteNonQueryAsync();
                conn.Close();
                //_context.Add(registroBaja);
                //await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdNinno"] = new SelectList(_context.Ninnos, "IdNinno", "IdentificacionNinno", registroBaja.IdNinno);
            return View(registroBaja);
        }

        // GET: RegistroBaja/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var registroBaja = await _context.RegistroBajas.FindAsync(id);
            if (registroBaja == null)
            {
                return NotFound();
            }
            ViewData["IdNinno"] = new SelectList(_context.Ninnos, "IdNinno", "Apell1Ninno", registroBaja.IdNinno);
            return View(registroBaja);
        }

        // POST: RegistroBaja/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdRegistroBaja,FechaBaja,IdNinno")] RegistroBaja registroBaja)
        {
            if (id != registroBaja.IdRegistroBaja)
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
                    cmd.CommandText = "sp_ModificarRegistroBajas";
                    cmd.Parameters.Add("@idRegistroBaja", System.Data.SqlDbType.Int).Value = registroBaja.IdRegistroBaja;
                    cmd.Parameters.Add("@fechaBaja", System.Data.SqlDbType.Date).Value = registroBaja.FechaBaja;
                    cmd.Parameters.Add("@idNinno", System.Data.SqlDbType.Int).Value = registroBaja.IdNinno;
                    await cmd.ExecuteNonQueryAsync();
                    conn.Close();
                    //_context.Update(registroBaja);
                    //await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RegistroBajaExists(registroBaja.IdRegistroBaja))
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
            ViewData["IdNinno"] = new SelectList(_context.Ninnos, "IdNinno", "IdentificacionNinno", registroBaja.IdNinno);
            return View(registroBaja);
        }

        // GET: RegistroBaja/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var registroBaja = await _context.RegistroBajas
                .Include(r => r.IdNinnoNavigation)
                .FirstOrDefaultAsync(m => m.IdRegistroBaja == id);
            if (registroBaja == null)
            {
                return NotFound();
            }

            return View(registroBaja);
        }

        // POST: RegistroBaja/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
            SqlCommand cmd = conn.CreateCommand();
            conn.Open();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "sp_EliminarRegistroBajas";
            cmd.Parameters.Add("@idRegistroBaja", System.Data.SqlDbType.Int).Value = id;
            await cmd.ExecuteNonQueryAsync();
            conn.Close();
            //var registroBaja = await _context.RegistroBajas.FindAsync(id);
            //_context.RegistroBajas.Remove(registroBaja);
            //await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RegistroBajaExists(int id)
        {
            return _context.RegistroBajas.Any(e => e.IdRegistroBaja == id);
        }
    }
}
