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
    public class MeseController : Controller
    {
        private readonly WebNinnoFelizContext _context;

        public MeseController(WebNinnoFelizContext context)
        {
            _context = context;
        }

        // GET: Mese
        public async Task<IActionResult> Index()
        {
            return View(await _context.Meses.ToListAsync());
        }

        // GET: Mese/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mese = await _context.Meses
                .FirstOrDefaultAsync(m => m.IdMes == id);
            if (mese == null)
            {
                return NotFound();
            }

            return View(mese);
        }

        // GET: Mese/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Mese/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdMes,NombreMes")] Mese mese)
        {
            if (ModelState.IsValid)
            {
                SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
                SqlCommand cmd = conn.CreateCommand();
                conn.Open();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "sp_IngresarMeses";
                //cmd.Parameters.Add("@idMes", System.Data.SqlDbType.Int).Value = mese.IdMes;
                cmd.Parameters.Add("@nombreMes", System.Data.SqlDbType.VarChar, 10).Value = mese.NombreMes;
                await cmd.ExecuteNonQueryAsync();
                conn.Close();
                //_context.Add(mese);
                //await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(mese);
        }

        // GET: Mese/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mese = await _context.Meses.FindAsync(id);
            if (mese == null)
            {
                return NotFound();
            }
            return View(mese);
        }

        // POST: Mese/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdMes,NombreMes")] Mese mese)
        {
            if (id != mese.IdMes)
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
                    cmd.CommandText = "sp_ModificarMeses";
                    cmd.Parameters.Add("@idMes", System.Data.SqlDbType.Int).Value = mese.IdMes;
                    cmd.Parameters.Add("@nombreMes", System.Data.SqlDbType.VarChar, 10).Value = mese.NombreMes;
                    await cmd.ExecuteNonQueryAsync();
                    conn.Close();
                    //_context.Update(mese);
                    //await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MeseExists(mese.IdMes))
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
            return View(mese);
        }

        // GET: Mese/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mese = await _context.Meses
                .FirstOrDefaultAsync(m => m.IdMes == id);
            if (mese == null)
            {
                return NotFound();
            }

            return View(mese);
        }

        // POST: Mese/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
            SqlCommand cmd = conn.CreateCommand();
            conn.Open();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "sp_EliminarMeses";
            cmd.Parameters.Add("@idMes", System.Data.SqlDbType.Int).Value = id;
            await cmd.ExecuteNonQueryAsync();
            conn.Close();
            //var mese = await _context.Meses.FindAsync(id);
            //_context.Meses.Remove(mese);
            //await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MeseExists(int id)
        {
            return _context.Meses.Any(e => e.IdMes == id);
        }
    }
}
