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
    public class ParentezcoController : Controller
    {
        private readonly WebNinnoFelizContext _context;

        public ParentezcoController(WebNinnoFelizContext context)
        {
            _context = context;
        }

        // GET: Parentezco
        public async Task<IActionResult> Index()
        {
            return View(await _context.Parentezcos.ToListAsync());
        }

        //PDF

        public async Task<IActionResult> PDF()
        {
            //return View(await _context.Parentezcos.ToListAsync());
            return new ViewAsPdf(await _context.Parentezcos.ToListAsync())
            {
                FileName = "Parentezco.pdf"
            };
        }

        // GET: Parentezco/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var parentezco = await _context.Parentezcos
                .FirstOrDefaultAsync(m => m.IdParentezco == id);
            if (parentezco == null)
            {
                return NotFound();
            }

            return View(parentezco);
        }

        // GET: Parentezco/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Parentezco/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdParentezco,DetallePar")] Parentezco parentezco)
        {
            if (ModelState.IsValid)
            {
                SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
                SqlCommand cmd = conn.CreateCommand();
                conn.Open();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "sp_IngresarParentezco";
                cmd.Parameters.Add("@detallePar", System.Data.SqlDbType.VarChar, 15).Value = parentezco.DetallePar;
                await cmd.ExecuteNonQueryAsync();
                conn.Close();
                //_context.Add(parentezco);
                //await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(parentezco);
        }

        // GET: Parentezco/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var parentezco = await _context.Parentezcos.FindAsync(id);
            if (parentezco == null)
            {
                return NotFound();
            }
            return View(parentezco);
        }

        // POST: Parentezco/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdParentezco,DetallePar")] Parentezco parentezco)
        {
            if (id != parentezco.IdParentezco)
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
                    cmd.CommandText = "sp_ModificarParentezcos";
                    cmd.Parameters.Add("@idParentezco", System.Data.SqlDbType.Int).Value = parentezco.IdParentezco;
                    cmd.Parameters.Add("@detallePar", System.Data.SqlDbType.VarChar, 15).Value = parentezco.DetallePar;
                    await cmd.ExecuteNonQueryAsync();
                    conn.Close();
                    //_context.Update(parentezco);
                    //await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ParentezcoExists(parentezco.IdParentezco))
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
            return View(parentezco);
        }

        // GET: Parentezco/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var parentezco = await _context.Parentezcos
                .FirstOrDefaultAsync(m => m.IdParentezco == id);
            if (parentezco == null)
            {
                return NotFound();
            }

            return View(parentezco);
        }

        // POST: Parentezco/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
            SqlCommand cmd = conn.CreateCommand();
            conn.Open();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "sp_EliminarParentezcos";
            cmd.Parameters.Add("@idParentezco", System.Data.SqlDbType.Int).Value = id;
            await cmd.ExecuteNonQueryAsync();
            conn.Close();
            //var parentezco = await _context.Parentezcos.FindAsync(id);
            //_context.Parentezcos.Remove(parentezco);
            //await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ParentezcoExists(int id)
        {
            return _context.Parentezcos.Any(e => e.IdParentezco == id);
        }
    }
}
