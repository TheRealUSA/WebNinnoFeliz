using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using WebNinnoFeliz.Data;
using WebNinnoFeliz.Models;
using WebNinnoFeliz.Models.ViewModels;

namespace WebNinnoFeliz.Controllers
{
    public class UsoComedoreController : Controller
    {
        List<UsoComedorNinnoMes> listausocomedor = new List<UsoComedorNinnoMes>();
        SqlDataAdapter adapter;
        private readonly WebNinnoFelizContext _context;

        public UsoComedoreController(WebNinnoFelizContext context)
        {
            _context = context;
        }

        // GET: UsoComedore
        public List<UsoComedorNinnoMes> ListarUsoComedor()
        {
            DataTable datatable = new DataTable();
            string error;
            try
            {
                SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
                adapter = new SqlDataAdapter("sp_listarUsoComedores", conn);
                using (adapter)
                {
                    conn.Open();
                    adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                    adapter.Fill(datatable);
                    int tamanno = datatable.Rows.Count;
                    if (tamanno > 0)
                    {
                        for (int i = 0; i < tamanno; i++)
                        {
                            UsoComedorNinnoMes ninno = new UsoComedorNinnoMes();
                            ninno.IdUsoComedor = Int32.Parse(datatable.Rows[i][0].ToString());
                            ninno.CantidadDias = Int32.Parse(datatable.Rows[i][1].ToString());
                            ninno.NombreMes = datatable.Rows[i][2].ToString();
                            ninno.NombreNinno = datatable.Rows[i][3].ToString();
                            ninno.Apell1Ninno = datatable.Rows[i][4].ToString();
                            ninno.Apell2Ninno = datatable.Rows[i][5].ToString();
                            listausocomedor.Add(ninno);
                        }
                    }
                    conn.Close();
                }

            }
            catch (Exception e)
            {
                error = e.InnerException.Message;
            }

            return listausocomedor;
        }


        public IActionResult Index()
        {
            //var webNinnoFelizContext = _context.Ninnos.Include(n => n.IdGeneroNavigation);
            return View(ListarUsoComedor());
        }

        // GET: UsoComedore/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usoComedore = await _context.UsoComedores
                .Include(u => u.IdMesNavigation)
                .Include(u => u.IdNinnoNavigation)
                .FirstOrDefaultAsync(m => m.IdUsoComedor == id);
            if (usoComedore == null)
            {
                return NotFound();
            }

            return View(usoComedore);
        }

        // GET: UsoComedore/Create
        public IActionResult Create()
        {
            ViewData["IdMes"] = new SelectList(_context.Meses, "IdMes", "NombreMes");
            ViewData["IdNinno"] = new SelectList(_context.Ninnos, "IdNinno", "IdentificacionNinno");
            return View();
        }

        // POST: UsoComedore/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdUsoComedor,CantidadDias,IdMes,IdNinno")] UsoComedore usoComedore)
        {
            if (ModelState.IsValid)
            {
                SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
                SqlCommand cmd = conn.CreateCommand();
                conn.Open();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "sp_IngresarUsoComedores";
                //cmd.Parameters.Add("@idUsoComedor", System.Data.SqlDbType.Int).Value = usoComedore.IdUsoComedor;
                cmd.Parameters.Add("@cantidadDias", System.Data.SqlDbType.Int).Value = usoComedore.CantidadDias;
                cmd.Parameters.Add("@idMes", System.Data.SqlDbType.Int).Value = usoComedore.IdMes;
                cmd.Parameters.Add("@idNinno", System.Data.SqlDbType.Int).Value = usoComedore.IdNinno;
                await cmd.ExecuteNonQueryAsync();
                conn.Close();
                //_context.Add(usoComedore);
                //await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdMes"] = new SelectList(_context.Meses, "IdMes", "NombreMes", usoComedore.IdMes);
            ViewData["IdNinno"] = new SelectList(_context.Ninnos, "IdNinno", "IdentificacionNinno", usoComedore.IdNinno);
            return View(usoComedore);
        }

        // GET: UsoComedore/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usoComedore = await _context.UsoComedores.FindAsync(id);
            if (usoComedore == null)
            {
                return NotFound();
            }
            ViewData["IdMes"] = new SelectList(_context.Meses, "IdMes", "NombreMes", usoComedore.IdMes);
            ViewData["IdNinno"] = new SelectList(_context.Ninnos, "IdNinno", "IdentificacionNinno", usoComedore.IdNinno);
            return View(usoComedore);
        }

        // POST: UsoComedore/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdUsoComedor,CantidadDias,IdMes,IdNinno")] UsoComedore usoComedore)
        {
            if (id != usoComedore.IdUsoComedor)
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
                    cmd.CommandText = "sp_ModificarUsoComedores";
                    cmd.Parameters.Add("@idUsoComedor", System.Data.SqlDbType.Int).Value = usoComedore.IdUsoComedor;
                    cmd.Parameters.Add("@cantidadDias", System.Data.SqlDbType.Int).Value = usoComedore.CantidadDias;
                    cmd.Parameters.Add("@idMes", System.Data.SqlDbType.Int).Value = usoComedore.IdMes;
                    cmd.Parameters.Add("@idNinno", System.Data.SqlDbType.Int).Value = usoComedore.IdNinno;
                    await cmd.ExecuteNonQueryAsync();
                    conn.Close();
                    //_context.Update(usoComedore);
                    //await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UsoComedoreExists(usoComedore.IdUsoComedor))
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
            ViewData["IdMes"] = new SelectList(_context.Meses, "IdMes", "NombreMes", usoComedore.IdMes);
            ViewData["IdNinno"] = new SelectList(_context.Ninnos, "IdNinno", "IdentificacionNinno", usoComedore.IdNinno);
            return View(usoComedore);
        }

        // GET: UsoComedore/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usoComedore = await _context.UsoComedores
                .Include(u => u.IdMesNavigation)
                .Include(u => u.IdNinnoNavigation)
                .FirstOrDefaultAsync(m => m.IdUsoComedor == id);
            if (usoComedore == null)
            {
                return NotFound();
            }

            return View(usoComedore);
        }

        // POST: UsoComedore/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
            SqlCommand cmd = conn.CreateCommand();
            conn.Open();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "sp_EliminarUsoComedores";
            cmd.Parameters.Add("@idUsoComedor", System.Data.SqlDbType.Int).Value = id;
            await cmd.ExecuteNonQueryAsync();
            conn.Close();
            //var usoComedore = await _context.UsoComedores.FindAsync(id);
            //_context.UsoComedores.Remove(usoComedore);
            //await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UsoComedoreExists(int id)
        {
            return _context.UsoComedores.Any(e => e.IdUsoComedor == id);
        }
    }
}
