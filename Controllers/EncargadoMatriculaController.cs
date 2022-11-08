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
    public class EncargadoMatriculaController : Controller
    {
        private readonly WebNinnoFelizContext _context;

        public EncargadoMatriculaController(WebNinnoFelizContext context)
        {
            _context = context;
        }

        // GET: EncargadoMatricula
        public async Task<IActionResult> Index()
        {
            var webNinnoFelizContext = _context.EncargadoMatriculas.Include(e => e.IdEncargadoNavigation).Include(e => e.NumeroMatriculaNavigation);
            return View(await webNinnoFelizContext.ToListAsync());
        }

        // GET: EncargadoMatricula/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var encargadoMatricula = await _context.EncargadoMatriculas
                .Include(e => e.IdEncargadoNavigation)
                .Include(e => e.NumeroMatriculaNavigation)
                .FirstOrDefaultAsync(m => m.IdEncargadoMatricula == id);
            if (encargadoMatricula == null)
            {
                return NotFound();
            }

            return View(encargadoMatricula);
        }

        // GET: EncargadoMatricula/Create
        public IActionResult Create()
        {
            ViewData["IdEncargado"] = new SelectList(_context.Encargados, "IdEncargado", "IdentificacionEncargado");
            ViewData["NumeroMatricula"] = new SelectList(_context.Matriculas, "NumeroMatricula", "NumeroMatricula");
            return View();
        }

        // POST: EncargadoMatricula/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdEncargadoMatricula,NumeroMatricula,IdEncargado")] EncargadoMatricula encargadoMatricula)
        {
            if (ModelState.IsValid)
            {
                SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
                SqlCommand cmd = conn.CreateCommand();
                conn.Open();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "sp_IngresarEncargado_Matriculas";
                cmd.Parameters.Add("@numeroMatricula", System.Data.SqlDbType.Int).Value = encargadoMatricula.NumeroMatricula;
                cmd.Parameters.Add("@idEncargado", System.Data.SqlDbType.Int).Value = encargadoMatricula.IdEncargado;
                await cmd.ExecuteNonQueryAsync();
                conn.Close();
                //_context.Add(encargadoMatricula);
                //await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdEncargado"] = new SelectList(_context.Encargados, "IdEncargado", "IdentificacionEncargado", encargadoMatricula.IdEncargado);
            ViewData["NumeroMatricula"] = new SelectList(_context.Matriculas, "NumeroMatricula", "NumeroMatricula", encargadoMatricula.NumeroMatricula);
            return View(encargadoMatricula);
        }

        // GET: EncargadoMatricula/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var encargadoMatricula = await _context.EncargadoMatriculas.FindAsync(id);
            if (encargadoMatricula == null)
            {
                return NotFound();
            }
            ViewData["IdEncargado"] = new SelectList(_context.Encargados, "IdEncargado", "IdentificacionEncargado", encargadoMatricula.IdEncargado);
            ViewData["NumeroMatricula"] = new SelectList(_context.Matriculas, "NumeroMatricula", "NumeroMatricula", encargadoMatricula.NumeroMatricula);
            return View(encargadoMatricula);
        }

        // POST: EncargadoMatricula/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdEncargadoMatricula,NumeroMatricula,IdEncargado")] EncargadoMatricula encargadoMatricula)
        {
            if (id != encargadoMatricula.IdEncargadoMatricula)
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
                    cmd.CommandText = "sp_ModificarEncargado_Matriculas";
                    cmd.Parameters.Add("@idEncargadoMatricula", System.Data.SqlDbType.Int).Value = encargadoMatricula.IdEncargadoMatricula;
                    cmd.Parameters.Add("@numeroMatricula", System.Data.SqlDbType.Int).Value = encargadoMatricula.NumeroMatricula;
                    cmd.Parameters.Add("@idEncargado", System.Data.SqlDbType.Int).Value = encargadoMatricula.IdEncargado;
                    await cmd.ExecuteNonQueryAsync();
                    conn.Close();
                    //_context.Update(encargadoMatricula);
                    //await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EncargadoMatriculaExists(encargadoMatricula.IdEncargadoMatricula))
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
            ViewData["IdEncargado"] = new SelectList(_context.Encargados, "IdEncargado", "IdentificacionEncargado", encargadoMatricula.IdEncargado);
            ViewData["NumeroMatricula"] = new SelectList(_context.Matriculas, "NumeroMatricula", "NumeroMatricula", encargadoMatricula.NumeroMatricula);
            return View(encargadoMatricula);
        }

        // GET: EncargadoMatricula/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var encargadoMatricula = await _context.EncargadoMatriculas
                .Include(e => e.IdEncargadoNavigation)
                .Include(e => e.NumeroMatriculaNavigation)
                .FirstOrDefaultAsync(m => m.IdEncargadoMatricula == id);
            if (encargadoMatricula == null)
            {
                return NotFound();
            }

            return View(encargadoMatricula);
        }

        // POST: EncargadoMatricula/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
            SqlCommand cmd = conn.CreateCommand();
            conn.Open();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "sp_EliminarEncargado_Matriculas";
            cmd.Parameters.Add("@idEncargadoMatricula", System.Data.SqlDbType.Int).Value = id;
            await cmd.ExecuteNonQueryAsync();
            conn.Close();
            //var encargadoMatricula = await _context.EncargadoMatriculas.FindAsync(id);
            //_context.EncargadoMatriculas.Remove(encargadoMatricula);
            //await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EncargadoMatriculaExists(int id)
        {
            return _context.EncargadoMatriculas.Any(e => e.IdEncargadoMatricula == id);
        }
    }
}
