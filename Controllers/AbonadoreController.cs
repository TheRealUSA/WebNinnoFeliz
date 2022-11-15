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
    public class AbonadoreController : Controller
    {
        private readonly WebNinnoFelizContext _context;

        public AbonadoreController(WebNinnoFelizContext context)
        {
            _context = context;
        }

        // GET: Abonadore
        public async Task<IActionResult> Index()
        {
            var webNinnoFelizContext = _context.Abonadores.Include(a => a.IdEncargadoNavigation);
            return View(await webNinnoFelizContext.ToListAsync());
        }


        public async Task<IActionResult> PDF()
        {
            return new ViewAsPdf(await _context.Abonadores.Include(a => a.IdEncargadoNavigation).ToListAsync())
            {
                FileName = "Abonadores.pdf"
            };
        }

        // GET: Abonadore/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var abonadore = await _context.Abonadores
                .Include(a => a.IdEncargadoNavigation)
                .FirstOrDefaultAsync(m => m.IdAbonador == id);
            if (abonadore == null)
            {
                return NotFound();
            }

            return View(abonadore);
        }

        // GET: Abonadore/Create
        public IActionResult Create()
        {
            ViewData["IdEncargado"] = new SelectList(_context.Encargados, "IdEncargado", "IdentificacionEncargado");
            return View();
        }

        // POST: Abonadore/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdAbonador,NumeroCuenta,IdEncargado")] Abonadore abonadore)
        {
            if (ModelState.IsValid)
            {
                SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
                SqlCommand cmd = conn.CreateCommand();
                conn.Open();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "sp_IngresarAbonadores";
                //cmd.Parameters.Add("@idAbonador", System.Data.SqlDbType.Int).Value = abonadore.IdAbonador;
                cmd.Parameters.Add("@numeroCuenta", System.Data.SqlDbType.Int).Value = abonadore.NumeroCuenta;
                cmd.Parameters.Add("@idEncargado", System.Data.SqlDbType.Int).Value = abonadore.IdEncargado;
                await cmd.ExecuteNonQueryAsync();
                conn.Close();
                //_context.Add(abonadore);
                //await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdEncargado"] = new SelectList(_context.Encargados, "IdEncargado", "IdentificacionEncargado", abonadore.IdEncargado);
            return View(abonadore);
        }

        // GET: Abonadore/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var abonadore = await _context.Abonadores.FindAsync(id);
            if (abonadore == null)
            {
                return NotFound();
            }
            ViewData["IdEncargado"] = new SelectList(_context.Encargados, "IdEncargado", "IdentificacionEncargado", abonadore.IdEncargado);
            return View(abonadore);
        }

        // POST: Abonadore/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdAbonador,NumeroCuenta,IdEncargado")] Abonadore abonadore)
        {
            if (id != abonadore.IdAbonador)
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
                    cmd.CommandText = "sp_ModificarAbonadores";
                    cmd.Parameters.Add("@idAbonador", System.Data.SqlDbType.Int).Value = abonadore.IdAbonador;
                    cmd.Parameters.Add("@numeroCuenta", System.Data.SqlDbType.Int).Value = abonadore.NumeroCuenta;
                    cmd.Parameters.Add("@idEncargado", System.Data.SqlDbType.Int).Value = abonadore.IdEncargado;
                    await cmd.ExecuteNonQueryAsync();
                    conn.Close();
                    //_context.Add(abonadore);
                    //await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AbonadoreExists(abonadore.IdAbonador))
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
            ViewData["IdEncargado"] = new SelectList(_context.Encargados, "IdEncargado", "IdentificacionEncargado", abonadore.IdEncargado);
            return View(abonadore);
        }

        // GET: Abonadore/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var abonadore = await _context.Abonadores
                .Include(a => a.IdEncargadoNavigation)
                .FirstOrDefaultAsync(m => m.IdAbonador == id);
            if (abonadore == null)
            {
                return NotFound();
            }

            return View(abonadore);
        }

        // POST: Abonadore/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
            SqlCommand cmd = conn.CreateCommand();
            conn.Open();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "sp_EliminarAbonadores";
            cmd.Parameters.Add("@idAbonador", System.Data.SqlDbType.Int).Value = id;
            await cmd.ExecuteNonQueryAsync();
            conn.Close();
            //var abonadore = await _context.Abonadores.FindAsync(id);
            //_context.Abonadores.Remove(abonadore);
            //await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AbonadoreExists(int id)
        {
            return _context.Abonadores.Any(e => e.IdAbonador == id);
        }
    }
}
