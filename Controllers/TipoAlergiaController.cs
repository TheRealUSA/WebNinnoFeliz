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
    public class TipoAlergiaController : Controller
    {
        private readonly WebNinnoFelizContext _context;

        public TipoAlergiaController(WebNinnoFelizContext context)
        {
            _context = context;
        }

        // GET: TipoAlergia
        public async Task<IActionResult> Index()
        {
            return View(await _context.TipoAlergias.ToListAsync());
        }

        // GET: TipoAlergia/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipoAlergia = await _context.TipoAlergias
                .FirstOrDefaultAsync(m => m.IdTipoAlergia == id);
            if (tipoAlergia == null)
            {
                return NotFound();
            }

            return View(tipoAlergia);
        }

        // GET: TipoAlergia/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TipoAlergia/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdTipoAlergia,NombreTipoAlergia")] TipoAlergia tipoAlergia)
        {
            if (ModelState.IsValid)
            {
                SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
                SqlCommand cmd = conn.CreateCommand();
                conn.Open();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "sp_IngresartipoAlergias";
                cmd.Parameters.Add("@nombreTipoAlergia", System.Data.SqlDbType.VarChar, 20).Value = tipoAlergia.NombreTipoAlergia;
                await cmd.ExecuteNonQueryAsync();
                conn.Close();
                //_context.Add(tipoAlergia);
                //await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tipoAlergia);
        }

        // GET: TipoAlergia/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipoAlergia = await _context.TipoAlergias.FindAsync(id);
            if (tipoAlergia == null)
            {
                return NotFound();
            }
            return View(tipoAlergia);
        }

        // POST: TipoAlergia/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdTipoAlergia,NombreTipoAlergia")] TipoAlergia tipoAlergia)
        {
            if (id != tipoAlergia.IdTipoAlergia)
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
                    cmd.CommandText = "sp_ModificartipoAlergias";
                    cmd.Parameters.Add("@idTipoAlergia", System.Data.SqlDbType.Int).Value = tipoAlergia.IdTipoAlergia;
                    cmd.Parameters.Add("@nombreTipoAlergia", System.Data.SqlDbType.VarChar, 20).Value = tipoAlergia.NombreTipoAlergia;
                    await cmd.ExecuteNonQueryAsync();
                    conn.Close();
                    //_context.Update(tipoAlergia);
                    //await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TipoAlergiaExists(tipoAlergia.IdTipoAlergia))
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
            return View(tipoAlergia);
        }

        // GET: TipoAlergia/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipoAlergia = await _context.TipoAlergias
                .FirstOrDefaultAsync(m => m.IdTipoAlergia == id);
            if (tipoAlergia == null)
            {
                return NotFound();
            }

            return View(tipoAlergia);
        }

        // POST: TipoAlergia/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
            SqlCommand cmd = conn.CreateCommand();
            conn.Open();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "sp_EliminartipoAlergias";
            cmd.Parameters.Add("@IdTipoAlergia", System.Data.SqlDbType.Int).Value = id;
            await cmd.ExecuteNonQueryAsync();
            conn.Close();
            //var tipoAlergia = await _context.TipoAlergias.FindAsync(id);
            //_context.TipoAlergias.Remove(tipoAlergia);
            //await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TipoAlergiaExists(int id)
        {
            return _context.TipoAlergias.Any(e => e.IdTipoAlergia == id);
        }
    }
}
