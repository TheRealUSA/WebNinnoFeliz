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
    public class NaiPlatoController : Controller
    {
        private readonly WebNinnoFelizContext _context;

        public NaiPlatoController(WebNinnoFelizContext context)
        {
            _context = context;
        }

        // GET: NaiPlato
        public async Task<IActionResult> Index()
        {
            var webNinnoFelizContext = _context.NaiPlatos.Include(n => n.IdNinnoAlergiaIngredienteNavigation).Include(n => n.IdPlatoNavigation);
            return View(await webNinnoFelizContext.ToListAsync());
        }

        // GET: NaiPlato/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var naiPlato = await _context.NaiPlatos
                .Include(n => n.IdNinnoAlergiaIngredienteNavigation)
                .Include(n => n.IdPlatoNavigation)
                .FirstOrDefaultAsync(m => m.IdNaiplato == id);
            if (naiPlato == null)
            {
                return NotFound();
            }

            return View(naiPlato);
        }

        // GET: NaiPlato/Create
        public IActionResult Create()
        {
            ViewData["IdNinnoAlergiaIngrediente"] = new SelectList(_context.NinnoAlergiaIngredientes, "IdNinnoAlergiaIngrediente", "IdNinnoAlergiaIngrediente");
            ViewData["IdPlato"] = new SelectList(_context.Platos, "IdPlato", "NombrePlato");
            return View();
        }

        // POST: NaiPlato/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdNaiplato,IdNinnoAlergiaIngrediente,IdPlato")] NaiPlato naiPlato)
        {
            if (ModelState.IsValid)
            {
                SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
                SqlCommand cmd = conn.CreateCommand();
                conn.Open();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "sp_IngresarNAI_Platos";
                cmd.Parameters.Add("@idNinnoAlergiaIngrediente", System.Data.SqlDbType.Int).Value = naiPlato.IdNinnoAlergiaIngrediente;
                cmd.Parameters.Add("@idPlato", System.Data.SqlDbType.Int).Value = naiPlato.IdPlato;
                await cmd.ExecuteNonQueryAsync();
                conn.Close();
                //_context.Add(naiPlato);
                //await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdNinnoAlergiaIngrediente"] = new SelectList(_context.NinnoAlergiaIngredientes, "IdNinnoAlergiaIngrediente", "IdNinnoAlergiaIngrediente", naiPlato.IdNinnoAlergiaIngrediente);
            ViewData["IdPlato"] = new SelectList(_context.Platos, "IdPlato", "NombrePlato", naiPlato.IdPlato);
            return View(naiPlato);
        }

        // GET: NaiPlato/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var naiPlato = await _context.NaiPlatos.FindAsync(id);
            if (naiPlato == null)
            {
                return NotFound();
            }
            ViewData["IdNinnoAlergiaIngrediente"] = new SelectList(_context.NinnoAlergiaIngredientes, "IdNinnoAlergiaIngrediente", "IdNinnoAlergiaIngrediente", naiPlato.IdNinnoAlergiaIngrediente);
            ViewData["IdPlato"] = new SelectList(_context.Platos, "IdPlato", "NombrePlato", naiPlato.IdPlato);
            return View(naiPlato);
        }

        // POST: NaiPlato/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdNaiplato,IdNinnoAlergiaIngrediente,IdPlato")] NaiPlato naiPlato)
        {
            if (id != naiPlato.IdNaiplato)
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
                    cmd.CommandText = "sp_ModificarNAI_Platos";
                    cmd.Parameters.Add("@idNAIPlato", System.Data.SqlDbType.Int).Value = naiPlato.IdNaiplato;
                    cmd.Parameters.Add("@idNinnoAlergiaIngrediente", System.Data.SqlDbType.Int).Value = naiPlato.IdNinnoAlergiaIngrediente;
                    cmd.Parameters.Add("@idPlato", System.Data.SqlDbType.Int).Value = naiPlato.IdPlato;
                    await cmd.ExecuteNonQueryAsync();
                    conn.Close();
                    //_context.Update(naiPlato);
                    //await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NaiPlatoExists(naiPlato.IdNaiplato))
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
            ViewData["IdNinnoAlergiaIngrediente"] = new SelectList(_context.NinnoAlergiaIngredientes, "IdNinnoAlergiaIngrediente", "IdNinnoAlergiaIngrediente", naiPlato.IdNinnoAlergiaIngrediente);
            ViewData["IdPlato"] = new SelectList(_context.Platos, "IdPlato", "NombrePlato", naiPlato.IdPlato);
            return View(naiPlato);
        }

        // GET: NaiPlato/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var naiPlato = await _context.NaiPlatos
                .Include(n => n.IdNinnoAlergiaIngredienteNavigation)
                .Include(n => n.IdPlatoNavigation)
                .FirstOrDefaultAsync(m => m.IdNaiplato == id);
            if (naiPlato == null)
            {
                return NotFound();
            }

            return View(naiPlato);
        }

        // POST: NaiPlato/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
            SqlCommand cmd = conn.CreateCommand();
            conn.Open();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "sp_EliminarNAI_Platos";
            cmd.Parameters.Add("@idNAIPlato", System.Data.SqlDbType.Int).Value = id;
            await cmd.ExecuteNonQueryAsync();
            conn.Close();
            //var naiPlato = await _context.NaiPlatos.FindAsync(id);
            //_context.NaiPlatos.Remove(naiPlato);
            //await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NaiPlatoExists(int id)
        {
            return _context.NaiPlatos.Any(e => e.IdNaiplato == id);
        }
    }
}
