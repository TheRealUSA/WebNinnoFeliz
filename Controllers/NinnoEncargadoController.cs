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
    public class NinnoEncargadoController : Controller
    {
        private readonly WebNinnoFelizContext _context;

        public NinnoEncargadoController(WebNinnoFelizContext context)
        {
            _context = context;
        }

        // GET: NinnoEncargado
        public async Task<IActionResult> Index()
        {
            var webNinnoFelizContext = _context.NinnoEncargados.Include(n => n.IdEncargadoNavigation).Include(n => n.IdNinnoNavigation);
            return View(await webNinnoFelizContext.ToListAsync());
        }

        // GET: NinnoEncargado/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ninnoEncargado = await _context.NinnoEncargados
                .Include(n => n.IdEncargadoNavigation)
                .Include(n => n.IdNinnoNavigation)
                .FirstOrDefaultAsync(m => m.IdNinnoEncargado == id);
            if (ninnoEncargado == null)
            {
                return NotFound();
            }

            return View(ninnoEncargado);
        }

        // GET: NinnoEncargado/Create
        public IActionResult Create()
        {
            ViewData["IdEncargado"] = new SelectList(_context.Encargados, "IdEncargado", "Apell1Encargado");
            ViewData["IdNinno"] = new SelectList(_context.Ninnos, "IdNinno", "NombreNinno");
            return View();
        }

        // POST: NinnoEncargado/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdNinnoEncargado,IdEncargado,IdNinno")] NinnoEncargado ninnoEncargado)
        {
            if (ModelState.IsValid)
            {
                SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
                SqlCommand cmd = conn.CreateCommand();
                conn.Open();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "sp_IngresarNinno_Encargados";
                cmd.Parameters.Add("@idNinno", System.Data.SqlDbType.Int).Value = ninnoEncargado.IdNinno;
                cmd.Parameters.Add("@idEncargado", System.Data.SqlDbType.Int).Value = ninnoEncargado.IdEncargado;
                await cmd.ExecuteNonQueryAsync();
                conn.Close();
                //_context.Add(ninnoEncargado);
                //await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdEncargado"] = new SelectList(_context.Encargados, "IdEncargado", "Apell1Encargado", ninnoEncargado.IdEncargado);
            ViewData["IdNinno"] = new SelectList(_context.Ninnos, "IdNinno", "NombreNinno", ninnoEncargado.IdNinno);
            return View(ninnoEncargado);
        }

        // GET: NinnoEncargado/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ninnoEncargado = await _context.NinnoEncargados.FindAsync(id);
            if (ninnoEncargado == null)
            {
                return NotFound();
            }
            ViewData["IdEncargado"] = new SelectList(_context.Encargados, "IdEncargado", "Apell1Encargado", ninnoEncargado.IdEncargado);
            ViewData["IdNinno"] = new SelectList(_context.Ninnos, "IdNinno", "Apell1Ninno", ninnoEncargado.IdNinno);
            return View(ninnoEncargado);
        }

        // POST: NinnoEncargado/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdNinnoEncargado,IdEncargado,IdNinno")] NinnoEncargado ninnoEncargado)
        {
            if (id != ninnoEncargado.IdNinnoEncargado)
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
                    cmd.CommandText = "sp_ModificarNinno_Encargados";
                    cmd.Parameters.Add("@idNinnoEncargado", System.Data.SqlDbType.Int).Value = ninnoEncargado.IdNinnoEncargado;
                    cmd.Parameters.Add("@idNinno", System.Data.SqlDbType.Int).Value = ninnoEncargado.IdNinno;
                    cmd.Parameters.Add("@idEncargado", System.Data.SqlDbType.Int).Value = ninnoEncargado.IdEncargado;
                    await cmd.ExecuteNonQueryAsync();
                    conn.Close();
                    //_context.Update(ninnoEncargado);
                    //await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NinnoEncargadoExists(ninnoEncargado.IdNinnoEncargado))
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
            ViewData["IdEncargado"] = new SelectList(_context.Encargados, "IdEncargado", "Apell1Encargado", ninnoEncargado.IdEncargado);
            ViewData["IdNinno"] = new SelectList(_context.Ninnos, "IdNinno", "Apell1Ninno", ninnoEncargado.IdNinno);
            return View(ninnoEncargado);
        }

        // GET: NinnoEncargado/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ninnoEncargado = await _context.NinnoEncargados
                .Include(n => n.IdEncargadoNavigation)
                .Include(n => n.IdNinnoNavigation)
                .FirstOrDefaultAsync(m => m.IdNinnoEncargado == id);
            if (ninnoEncargado == null)
            {
                return NotFound();
            }

            return View(ninnoEncargado);
        }

        // POST: NinnoEncargado/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
            SqlCommand cmd = conn.CreateCommand();
            conn.Open();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "sp_EliminarNinno_Encargados";
            cmd.Parameters.Add("@idNinnoEncargado", System.Data.SqlDbType.Int).Value = id;
            await cmd.ExecuteNonQueryAsync();
            conn.Close();
            //var ninnoEncargado = await _context.NinnoEncargados.FindAsync(id);
            //_context.NinnoEncargados.Remove(ninnoEncargado);
            //await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NinnoEncargadoExists(int id)
        {
            return _context.NinnoEncargados.Any(e => e.IdNinnoEncargado == id);
        }
    }
}
