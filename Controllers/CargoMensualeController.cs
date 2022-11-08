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
    public class CargoMensualeController : Controller
    {
        private readonly WebNinnoFelizContext _context;

        public CargoMensualeController(WebNinnoFelizContext context)
        {
            _context = context;
        }

        // GET: CargoMensuale
        public async Task<IActionResult> Index()
        {
            var webNinnoFelizContext = _context.CargoMensuales.Include(c => c.IdUsoComedorNavigation);
            return View(await webNinnoFelizContext.ToListAsync());
        }

        // GET: CargoMensuale/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cargoMensuale = await _context.CargoMensuales
                .Include(c => c.IdUsoComedorNavigation)
                .FirstOrDefaultAsync(m => m.IdCargo == id);
            if (cargoMensuale == null)
            {
                return NotFound();
            }

            return View(cargoMensuale);
        }

        // GET: CargoMensuale/Create
        public IActionResult Create()
        {
            ViewData["IdUsoComedor"] = new SelectList(_context.UsoComedores, "IdUsoComedor", "IdUsoComedor");
            return View();
        }

        // POST: CargoMensuale/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdCargo,CargoMensual,IdUsoComedor")] CargoMensuale cargoMensuale)
        {
            if (ModelState.IsValid)
            {
                SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
                SqlCommand cmd = conn.CreateCommand();
                conn.Open();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "sp_IngresarCargoMensuales";
                //cmd.Parameters.Add("@idCargo", System.Data.SqlDbType.Int).Value = cargoMensuale.IdCargo;
                cmd.Parameters.Add("@cargoMensual", System.Data.SqlDbType.VarChar, 10).Value = cargoMensuale.CargoMensual;
                cmd.Parameters.Add("@idUsoComedor", System.Data.SqlDbType.Int).Value = cargoMensuale.IdUsoComedor;
                await cmd.ExecuteNonQueryAsync();
                conn.Close();
                //_context.Add(cargoMensuale);
                //await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdUsoComedor"] = new SelectList(_context.UsoComedores, "IdUsoComedor", "IdUsoComedor", cargoMensuale.IdUsoComedor);
            return View(cargoMensuale);
        }

        // GET: CargoMensuale/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cargoMensuale = await _context.CargoMensuales.FindAsync(id);
            if (cargoMensuale == null)
            {
                return NotFound();
            }
            ViewData["IdUsoComedor"] = new SelectList(_context.UsoComedores, "IdUsoComedor", "IdUsoComedor", cargoMensuale.IdUsoComedor);
            return View(cargoMensuale);
        }

        // POST: CargoMensuale/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdCargo,CargoMensual,IdUsoComedor")] CargoMensuale cargoMensuale)
        {
            if (id != cargoMensuale.IdCargo)
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
                    cmd.CommandText = "sp_ModificarCargoMensuales";
                    cmd.Parameters.Add("@idCargo", System.Data.SqlDbType.Int).Value = cargoMensuale.IdCargo;
                    cmd.Parameters.Add("@cargoMensual", System.Data.SqlDbType.VarChar, 10).Value = cargoMensuale.CargoMensual;
                    cmd.Parameters.Add("@idUsoComedor", System.Data.SqlDbType.Int).Value = cargoMensuale.IdUsoComedor;
                    await cmd.ExecuteNonQueryAsync();
                    conn.Close();
                    //_context.Update(cargoMensuale);
                    //await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CargoMensualeExists(cargoMensuale.IdCargo))
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
            ViewData["IdUsoComedor"] = new SelectList(_context.UsoComedores, "IdUsoComedor", "IdUsoComedor", cargoMensuale.IdUsoComedor);
            return View(cargoMensuale);
        }

        // GET: CargoMensuale/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cargoMensuale = await _context.CargoMensuales
                .Include(c => c.IdUsoComedorNavigation)
                .FirstOrDefaultAsync(m => m.IdCargo == id);
            if (cargoMensuale == null)
            {
                return NotFound();
            }

            return View(cargoMensuale);
        }

        // POST: CargoMensuale/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
            SqlCommand cmd = conn.CreateCommand();
            conn.Open();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "sp_EliminarCargoMensuales";
            cmd.Parameters.Add("@idCargo", System.Data.SqlDbType.Int).Value = id;
            await cmd.ExecuteNonQueryAsync();
            conn.Close();
            //var cargoMensuale = await _context.CargoMensuales.FindAsync(id);
            //_context.CargoMensuales.Remove(cargoMensuale);
            //await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CargoMensualeExists(int id)
        {
            return _context.CargoMensuales.Any(e => e.IdCargo == id);
        }
    }
}
