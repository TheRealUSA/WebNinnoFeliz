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
    public class PlatoIngredienteController : Controller
    {
        private readonly WebNinnoFelizContext _context;

        public PlatoIngredienteController(WebNinnoFelizContext context)
        {
            _context = context;
        }

        // GET: PlatoIngrediente
        public async Task<IActionResult> Index()
        {
            var webNinnoFelizContext = _context.PlatoIngredientes.Include(p => p.IdIngredienteNavigation).Include(p => p.IdPlatoNavigation);
            return View(await webNinnoFelizContext.ToListAsync());
        }

        // GET: PlatoIngrediente/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var platoIngrediente = await _context.PlatoIngredientes
                .Include(p => p.IdIngredienteNavigation)
                .Include(p => p.IdPlatoNavigation)
                .FirstOrDefaultAsync(m => m.IdPlatoIngrediente == id);
            if (platoIngrediente == null)
            {
                return NotFound();
            }

            return View(platoIngrediente);
        }

        // GET: PlatoIngrediente/Create
        public IActionResult Create()
        {
            ViewData["IdIngrediente"] = new SelectList(_context.Ingredientes, "IdIngrediente", "NombreIngrediente");
            ViewData["IdPlato"] = new SelectList(_context.Platos, "IdPlato", "NombrePlato");
            return View();
        }

        // POST: PlatoIngrediente/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdPlatoIngrediente,IdPlato,IdIngrediente")] PlatoIngrediente platoIngrediente)
        {
            if (ModelState.IsValid)
            {
                SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
                SqlCommand cmd = conn.CreateCommand();
                conn.Open();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "sp_IngresarPlato_Ingredientes";
                cmd.Parameters.Add("@idIngrediente", System.Data.SqlDbType.Int).Value = platoIngrediente.IdIngrediente;
                cmd.Parameters.Add("@idPlato", System.Data.SqlDbType.Int).Value = platoIngrediente.IdPlato;
                await cmd.ExecuteNonQueryAsync();
                conn.Close();
                //_context.Add(platoIngrediente);
                //await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdIngrediente"] = new SelectList(_context.Ingredientes, "IdIngrediente", "NombreIngrediente", platoIngrediente.IdIngrediente);
            ViewData["IdPlato"] = new SelectList(_context.Platos, "IdPlato", "NombrePlato", platoIngrediente.IdPlato);
            return View(platoIngrediente);
        }

        // GET: PlatoIngrediente/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var platoIngrediente = await _context.PlatoIngredientes.FindAsync(id);
            if (platoIngrediente == null)
            {
                return NotFound();
            }
            ViewData["IdIngrediente"] = new SelectList(_context.Ingredientes, "IdIngrediente", "NombreIngrediente", platoIngrediente.IdIngrediente);
            ViewData["IdPlato"] = new SelectList(_context.Platos, "IdPlato", "NombrePlato", platoIngrediente.IdPlato);
            return View(platoIngrediente);
        }

        // POST: PlatoIngrediente/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdPlatoIngrediente,IdPlato,IdIngrediente")] PlatoIngrediente platoIngrediente)
        {
            if (id != platoIngrediente.IdPlatoIngrediente)
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
                    cmd.CommandText = "sp_ModificarPlato_Ingredientes";
                    cmd.Parameters.Add("@idPlatoIngrediente", System.Data.SqlDbType.Int).Value = platoIngrediente.IdPlatoIngrediente;
                    cmd.Parameters.Add("@idIngrediente", System.Data.SqlDbType.Int).Value = platoIngrediente.IdIngrediente;
                    cmd.Parameters.Add("@idPlato", System.Data.SqlDbType.Int).Value = platoIngrediente.IdPlato;
                    await cmd.ExecuteNonQueryAsync();
                    conn.Close();
                    //_context.Update(platoIngrediente);
                    //await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PlatoIngredienteExists(platoIngrediente.IdPlatoIngrediente))
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
            ViewData["IdIngrediente"] = new SelectList(_context.Ingredientes, "IdIngrediente", "NombreIngrediente", platoIngrediente.IdIngrediente);
            ViewData["IdPlato"] = new SelectList(_context.Platos, "IdPlato", "NombrePlato", platoIngrediente.IdPlato);
            return View(platoIngrediente);
        }

        // GET: PlatoIngrediente/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var platoIngrediente = await _context.PlatoIngredientes
                .Include(p => p.IdIngredienteNavigation)
                .Include(p => p.IdPlatoNavigation)
                .FirstOrDefaultAsync(m => m.IdPlatoIngrediente == id);
            if (platoIngrediente == null)
            {
                return NotFound();
            }

            return View(platoIngrediente);
        }

        // POST: PlatoIngrediente/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
            SqlCommand cmd = conn.CreateCommand();
            conn.Open();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "sp_EliminarPlato_Ingredientes";
            cmd.Parameters.Add("@idPlatoIngrediente", System.Data.SqlDbType.Int).Value = id;
            await cmd.ExecuteNonQueryAsync();
            conn.Close();
            //var platoIngrediente = await _context.PlatoIngredientes.FindAsync(id);
            //_context.PlatoIngredientes.Remove(platoIngrediente);
            //await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PlatoIngredienteExists(int id)
        {
            return _context.PlatoIngredientes.Any(e => e.IdPlatoIngrediente == id);
        }
    }
}
