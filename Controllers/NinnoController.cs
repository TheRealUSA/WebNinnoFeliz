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
    public class NinnoController : Controller
    {
        private readonly WebNinnoFelizContext _context;

        public NinnoController(WebNinnoFelizContext context)
        {
            _context = context;
        }

        // GET: Ninno
        public async Task<IActionResult> Index()
        {
            var webNinnoFelizContext = _context.Ninnos.Include(n => n.IdGeneroNavigation);
            return View(await webNinnoFelizContext.ToListAsync());
        }

        //PDF

        public async Task<IActionResult> PDF()
        {
            //return View(await _context.Parentezcos.ToListAsync());
            return new ViewAsPdf(await _context.Ninnos.ToListAsync())
            {
                FileName = "Ninnos.pdf"
            }; 
        }

        // GET: Ninno/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ninno = await _context.Ninnos
                .Include(n => n.IdGeneroNavigation)
                .FirstOrDefaultAsync(m => m.IdNinno == id);
            if (ninno == null)
            {
                return NotFound();
            }

            return View(ninno);
        }

        // GET: Ninno/Create
        public IActionResult Create()
        {
            ViewData["IdGenero"] = new SelectList(_context.Generos, "IdGenero", "DetalleGen");
            return View();
        }

        // POST: Ninno/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdNinno,IdentificacionNinno,NombreNinno,Apell1Ninno,Apell2Ninno,FechaNacimiento,DireccionNinno,IdGenero")] Ninno ninno)
        {
            if (ModelState.IsValid)
            {
                SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
                SqlCommand cmd = conn.CreateCommand();
                conn.Open();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "sp_IngresarNinnos";
                cmd.Parameters.Add("@IdentificacionNinno", System.Data.SqlDbType.VarChar, 15).Value = ninno.IdentificacionNinno;
                cmd.Parameters.Add("@nombreNinno", System.Data.SqlDbType.VarChar, 15).Value = ninno.NombreNinno;
                cmd.Parameters.Add("@apell1Ninno", System.Data.SqlDbType.VarChar, 15).Value = ninno.Apell1Ninno;
                cmd.Parameters.Add("@apell2Ninno", System.Data.SqlDbType.VarChar, 15).Value = ninno.Apell2Ninno;
                cmd.Parameters.Add("@fechaNacimiento", System.Data.SqlDbType.Date).Value = ninno.FechaNacimiento;
                cmd.Parameters.Add("@direccionNinno", System.Data.SqlDbType.VarChar, 50).Value = ninno.DireccionNinno;
                cmd.Parameters.Add("@idGenero", System.Data.SqlDbType.Int).Value = ninno.IdGenero;
                await cmd.ExecuteNonQueryAsync();
                conn.Close();
                //_context.Add(ninno);
                //await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdGenero"] = new SelectList(_context.Generos, "IdGenero", "DetalleGen", ninno.IdGenero);
            return View(ninno);
        }

        // GET: Ninno/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ninno = await _context.Ninnos.FindAsync(id);
            if (ninno == null)
            {
                return NotFound();
            }
            ViewData["IdGenero"] = new SelectList(_context.Generos, "IdGenero", "DetalleGen", ninno.IdGenero);
            return View(ninno);
        }

        // POST: Ninno/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdNinno,IdentificacionNinno,NombreNinno,Apell1Ninno,Apell2Ninno,FechaNacimiento,DireccionNinno,IdGenero")] Ninno ninno)
        {
            if (id != ninno.IdNinno)
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
                    cmd.CommandText = "sp_ModificarNinnos";
                    cmd.Parameters.Add("@idNinno", System.Data.SqlDbType.Int).Value = ninno.IdNinno;
                    cmd.Parameters.Add("@IdentificacionNinno", System.Data.SqlDbType.VarChar, 15).Value = ninno.IdentificacionNinno;
                    cmd.Parameters.Add("@nombreNinno", System.Data.SqlDbType.VarChar, 15).Value = ninno.NombreNinno;
                    cmd.Parameters.Add("@apell1Ninno", System.Data.SqlDbType.VarChar, 15).Value = ninno.Apell1Ninno;
                    cmd.Parameters.Add("@apell2Ninno", System.Data.SqlDbType.VarChar, 15).Value = ninno.Apell2Ninno;
                    cmd.Parameters.Add("@fechaNacimiento", System.Data.SqlDbType.Date).Value = ninno.FechaNacimiento;
                    cmd.Parameters.Add("@direccionNinno", System.Data.SqlDbType.VarChar, 50).Value = ninno.DireccionNinno;
                    cmd.Parameters.Add("@idGenero", System.Data.SqlDbType.Int).Value = ninno.IdGenero;
                    await cmd.ExecuteNonQueryAsync();
                    conn.Close();
                    //_context.Update(ninno);
                    //await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NinnoExists(ninno.IdNinno))
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
            ViewData["IdGenero"] = new SelectList(_context.Generos, "IdGenero", "DetalleGen", ninno.IdGenero);
            return View(ninno);
        }

        // GET: Ninno/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ninno = await _context.Ninnos
                .Include(n => n.IdGeneroNavigation)
                .FirstOrDefaultAsync(m => m.IdNinno == id);
            if (ninno == null)
            {
                return NotFound();
            }

            return View(ninno);
        }

        // POST: Ninno/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
            SqlCommand cmd = conn.CreateCommand();
            conn.Open();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "sp_EliminarNinnos";
            cmd.Parameters.Add("@idNinno", System.Data.SqlDbType.Int).Value = id;
            await cmd.ExecuteNonQueryAsync();
            conn.Close();
            //var ninno = await _context.Ninnos.FindAsync(id);
            //_context.Ninnos.Remove(ninno);
            //await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NinnoExists(int id)
        {
            return _context.Ninnos.Any(e => e.IdNinno == id);
        }
    }
}
