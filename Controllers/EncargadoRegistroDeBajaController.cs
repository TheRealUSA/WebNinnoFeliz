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
    public class EncargadoRegistroDeBajaController : Controller
    {

        List<EncargadoRegistroBajas> listaencreg = new List<EncargadoRegistroBajas>();
        SqlDataAdapter adapter;

        private readonly WebNinnoFelizContext _context;

        public EncargadoRegistroDeBajaController(WebNinnoFelizContext context)
        {
            _context = context;
        }

        public List<EncargadoRegistroBajas> ListaEncargadoRegistro()
        {
            DataTable datatable = new DataTable();
            string error;
            try
            {
                SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
                adapter = new SqlDataAdapter("sp_listarEncargado_RegistroBajas", conn);
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
                            EncargadoRegistroBajas encargado = new EncargadoRegistroBajas();
                            encargado.IdEncargadoRegistroBaja = Int32.Parse(datatable.Rows[i][0].ToString());
                            encargado.IdRegistroBaja = Int32.Parse(datatable.Rows[i][1].ToString());
                            encargado.IdentificacionEncargado = datatable.Rows[i][2].ToString();
                            encargado.NombreEncargado = datatable.Rows[i][3].ToString();
                            encargado.Apell1Encargado = datatable.Rows[i][4].ToString();
                            encargado.Apell2Encargado = datatable.Rows[i][5].ToString();
                            encargado.DetallePar = datatable.Rows[i][6].ToString();
                            encargado.FechaIngreso = DateTime.Parse(datatable.Rows[i][7].ToString());
                            encargado.NombreNinno = datatable.Rows[i][8].ToString();
                            encargado.Apell1Ninno = datatable.Rows[i][9].ToString();
                            encargado.Apell2Ninno = datatable.Rows[i][10].ToString();
                            listaencreg.Add(encargado);
                        }
                    }
                    conn.Close();
                }

            }
            catch (Exception e)
            {
                error = e.InnerException.Message;
            }

            return listaencreg;
        }


        public IActionResult Index()
        {
            //var webNinnoFelizContext = _context.Encargados.Include(e => e.IdParentezcoNavigation);
            return View(ListaEncargadoRegistro());
        }

        // GET: EncargadoRegistroDeBaja/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var encargadoRegistroDeBaja = await _context.EncargadoRegistroDeBajas
                .Include(e => e.IdEncargadoNavigation)
                .Include(e => e.IdRegistroBajaNavigation)
                .FirstOrDefaultAsync(m => m.IdEncargadoRegistroBaja == id);
            if (encargadoRegistroDeBaja == null)
            {
                return NotFound();
            }

            return View(encargadoRegistroDeBaja);
        }

        // GET: EncargadoRegistroDeBaja/Create
        public IActionResult Create()
        {
            ViewData["IdEncargado"] = new SelectList(_context.Encargados, "IdEncargado", "IdentificacionEncargado");
            ViewData["IdRegistroBaja"] = new SelectList(_context.RegistroBajas, "IdRegistroBaja", "IdRegistroBaja");
            return View();
        }

        // POST: EncargadoRegistroDeBaja/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdEncargadoRegistroBaja,IdRegistroBaja,IdEncargado")] EncargadoRegistroDeBaja encargadoRegistroDeBaja)
        {
            if (ModelState.IsValid)
            {
                SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
                SqlCommand cmd = conn.CreateCommand();
                conn.Open();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "sp_IngresarEncargado_RegistroDeBajas";
                cmd.Parameters.Add("@idRegistroBaja", System.Data.SqlDbType.Int).Value = encargadoRegistroDeBaja.IdRegistroBaja;
                cmd.Parameters.Add("@idEncargado", System.Data.SqlDbType.Int).Value = encargadoRegistroDeBaja.IdEncargado;
                await cmd.ExecuteNonQueryAsync();
                conn.Close();
                //_context.Add(encargadoRegistroDeBaja);
                //await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdEncargado"] = new SelectList(_context.Encargados, "IdEncargado", "IdentificacionEncargado", encargadoRegistroDeBaja.IdEncargado);
            ViewData["IdRegistroBaja"] = new SelectList(_context.RegistroBajas, "IdRegistroBaja", "IdRegistroBaja", encargadoRegistroDeBaja.IdRegistroBaja);
            return View(encargadoRegistroDeBaja);
        }

        // GET: EncargadoRegistroDeBaja/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var encargadoRegistroDeBaja = await _context.EncargadoRegistroDeBajas.FindAsync(id);
            if (encargadoRegistroDeBaja == null)
            {
                return NotFound();
            }
            ViewData["IdEncargado"] = new SelectList(_context.Encargados, "IdEncargado", "IdentificacionEncargado", encargadoRegistroDeBaja.IdEncargado);
            ViewData["IdRegistroBaja"] = new SelectList(_context.RegistroBajas, "IdRegistroBaja", "IdRegistroBaja", encargadoRegistroDeBaja.IdRegistroBaja);
            return View(encargadoRegistroDeBaja);
        }

        // POST: EncargadoRegistroDeBaja/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdEncargadoRegistroBaja,IdRegistroBaja,IdEncargado")] EncargadoRegistroDeBaja encargadoRegistroDeBaja)
        {
            if (id != encargadoRegistroDeBaja.IdEncargadoRegistroBaja)
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
                    cmd.CommandText = "sp_ModificarEncargado_RegistroDeBajas";
                    cmd.Parameters.Add("@idEncargadoRegistroBaja", System.Data.SqlDbType.Int).Value = encargadoRegistroDeBaja.IdEncargadoRegistroBaja;
                    cmd.Parameters.Add("@idRegistroBaja", System.Data.SqlDbType.Int).Value = encargadoRegistroDeBaja.IdRegistroBaja;
                    cmd.Parameters.Add("@idEncargado", System.Data.SqlDbType.Int).Value = encargadoRegistroDeBaja.IdEncargado;
                    await cmd.ExecuteNonQueryAsync();
                    conn.Close();
                    //_context.Update(encargadoRegistroDeBaja);
                    //await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EncargadoRegistroDeBajaExists(encargadoRegistroDeBaja.IdEncargadoRegistroBaja))
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
            ViewData["IdEncargado"] = new SelectList(_context.Encargados, "IdEncargado", "IdentificacionEncargado", encargadoRegistroDeBaja.IdEncargado);
            ViewData["IdRegistroBaja"] = new SelectList(_context.RegistroBajas, "IdRegistroBaja", "IdRegistroBaja", encargadoRegistroDeBaja.IdRegistroBaja);
            return View(encargadoRegistroDeBaja);
        }

        // GET: EncargadoRegistroDeBaja/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var encargadoRegistroDeBaja = await _context.EncargadoRegistroDeBajas
                .Include(e => e.IdEncargadoNavigation)
                .Include(e => e.IdRegistroBajaNavigation)
                .FirstOrDefaultAsync(m => m.IdEncargadoRegistroBaja == id);
            if (encargadoRegistroDeBaja == null)
            {
                return NotFound();
            }

            return View(encargadoRegistroDeBaja);
        }

        // POST: EncargadoRegistroDeBaja/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
            SqlCommand cmd = conn.CreateCommand();
            conn.Open();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "sp_EliminarEncargado_RegistroDeBajas";
            cmd.Parameters.Add("@idEncargadoRegistroBaja", System.Data.SqlDbType.Int).Value = id;
            await cmd.ExecuteNonQueryAsync();
            conn.Close();
            //var encargadoRegistroDeBaja = await _context.EncargadoRegistroDeBajas.FindAsync(id);
            //_context.EncargadoRegistroDeBajas.Remove(encargadoRegistroDeBaja);
            //await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EncargadoRegistroDeBajaExists(int id)
        {
            return _context.EncargadoRegistroDeBajas.Any(e => e.IdEncargadoRegistroBaja == id);
        }
    }
}
