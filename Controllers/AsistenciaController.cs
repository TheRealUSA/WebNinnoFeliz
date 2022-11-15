using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Rotativa.AspNetCore;
using WebNinnoFeliz.Data;
using WebNinnoFeliz.Models;

namespace WebNinnoFeliz.Controllers
{
    public class AsistenciaController : Controller
    {
        private readonly WebNinnoFelizContext _context;

        public AsistenciaController(WebNinnoFelizContext context)
        {
            _context = context;
        }

        // GET: Asistencia
        public async Task<IActionResult> Index()
        {
            var webNinnoFelizContext = _context.Asistencias.Include(a => a.IdNinnoNavigation);
            return View(await webNinnoFelizContext.ToListAsync());
        }

        public async Task<IActionResult> PDF()
        {
            return new ViewAsPdf(await _context.Asistencias.Include(a => a.IdNinnoNavigation).ToListAsync())
            {
                FileName = "Asistencia.pdf"
            };
        }

        // GET: Asistencia/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var asistencia = await _context.Asistencias
                .Include(a => a.IdNinnoNavigation)
                .FirstOrDefaultAsync(m => m.IdAsistencia == id);
            if (asistencia == null)
            {
                return NotFound();
            }

            return View(asistencia);
        }

        // GET: Asistencia/Create
        public IActionResult Create()
        {
            ViewData["IdNinno"] = new SelectList(_context.Ninnos, "IdNinno", "IdentificacionNinno");
            return View();
        }

        // POST: Asistencia/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdAsistencia,FechaAsist,EstadoAsist,IdNinno")] Asistencia asistencia)
        {
            if (ModelState.IsValid)
            {
                SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
                SqlCommand cmd = conn.CreateCommand();
                conn.Open();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "sp_IngresarAsistencias";
                cmd.Parameters.Add("@fechaAsist", System.Data.SqlDbType.Date).Value = asistencia.FechaAsist;
                cmd.Parameters.Add("@estadoAsist", System.Data.SqlDbType.Bit).Value = asistencia.EstadoAsist;
                cmd.Parameters.Add("@idNinno", System.Data.SqlDbType.Int).Value = asistencia.IdNinno;
                await cmd.ExecuteNonQueryAsync();
                conn.Close();
                //_context.Add(asistencia);
                //await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdNinno"] = new SelectList(_context.Ninnos, "IdNinno", "IdentificacionNinno", asistencia.IdNinno);
            return View(asistencia);
        }

        // GET: Asistencia/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var asistencia = await _context.Asistencias.FindAsync(id);
            if (asistencia == null)
            {
                return NotFound();
            }
            ViewData["IdNinno"] = new SelectList(_context.Ninnos, "IdNinno", "IdentificacionNinno", asistencia.IdNinno);
            return View(asistencia);
        }

        // POST: Asistencia/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdAsistencia,FechaAsist,EstadoAsist,IdNinno")] Asistencia asistencia)
        {
            if (id != asistencia.IdAsistencia)
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
                    cmd.CommandText = "sp_ModificarAsistencias";
                    cmd.Parameters.Add("@idAsistencia", System.Data.SqlDbType.Int).Value = asistencia.IdAsistencia;
                    cmd.Parameters.Add("@fechaAsist", System.Data.SqlDbType.Date).Value = asistencia.FechaAsist;
                    cmd.Parameters.Add("@estadoAsist", System.Data.SqlDbType.Bit).Value = asistencia.EstadoAsist;
                    cmd.Parameters.Add("@idNinno", System.Data.SqlDbType.Int).Value = asistencia.IdNinno;
                    await cmd.ExecuteNonQueryAsync();
                    conn.Close();
                    //_context.Update(asistencia);
                    //await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AsistenciaExists(asistencia.IdAsistencia))
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
            ViewData["IdNinno"] = new SelectList(_context.Ninnos, "IdNinno", "IdentificacionNinno", asistencia.IdNinno);
            return View(asistencia);
        }

        // GET: Asistencia/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var asistencia = await _context.Asistencias
                .Include(a => a.IdNinnoNavigation)
                .FirstOrDefaultAsync(m => m.IdAsistencia == id);
            if (asistencia == null)
            {
                return NotFound();
            }

            return View(asistencia);
        }

        // POST: Asistencia/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
            SqlCommand cmd = conn.CreateCommand();
            conn.Open();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "sp_EliminarAsistencias";
            cmd.Parameters.Add("@idAsistencia", System.Data.SqlDbType.Int).Value = id;
            await cmd.ExecuteNonQueryAsync();
            conn.Close();
            //var asistencia = await _context.Asistencias.FindAsync(id);
            //_context.Asistencias.Remove(asistencia);
            //await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AsistenciaExists(int id)
        {
            return _context.Asistencias.Any(e => e.IdAsistencia == id);
        }
    }
}
