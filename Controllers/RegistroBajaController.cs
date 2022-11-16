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
    public class RegistroBajaController : Controller
    {

        List<RegistroBajaNinno> listaregistroninno = new List<RegistroBajaNinno>();
        SqlDataAdapter adapter;

        private readonly WebNinnoFelizContext _context;

        public RegistroBajaController(WebNinnoFelizContext context)
        {
            _context = context;
        }

        public List<RegistroBajaNinno> ListarNinnoRegistro()
        {
            DataTable datatable = new DataTable();
            string error;
            try
            {
                SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
                adapter = new SqlDataAdapter("sp_listarRegistroBajas", conn);
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
                            RegistroBajaNinno ninno = new RegistroBajaNinno();
                            ninno.IdRegistroBaja = Int32.Parse(datatable.Rows[i][0].ToString());
                            ninno.FechaBaja = DateTime.Parse(datatable.Rows[i][1].ToString());
                            ninno.IdentificacionNinno = datatable.Rows[i][2].ToString();
                            ninno.NombreNinno = datatable.Rows[i][3].ToString();
                            ninno.Apell1Ninno = datatable.Rows[i][4].ToString();
                            ninno.Apell2Ninno = datatable.Rows[i][5].ToString();

                            listaregistroninno.Add(ninno);
                        }
                    }
                    conn.Close();
                }

            }
            catch (Exception e)
            {
                error = e.InnerException.Message;
            }

            return listaregistroninno;
        }


        public IActionResult Index()
        {
            //var webNinnoFelizContext = _context.Ninnos.Include(n => n.IdGeneroNavigation);
            return View(ListarNinnoRegistro());
        }


        // GET: RegistroBaja/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var registroBaja = await _context.RegistroBajas
                .Include(r => r.IdNinnoNavigation)
                .FirstOrDefaultAsync(m => m.IdRegistroBaja == id);
            if (registroBaja == null)
            {
                return NotFound();
            }

            return View(registroBaja);
        }

        // GET: RegistroBaja/Create
        public IActionResult Create()
        {
            ViewData["IdNinno"] = new SelectList(_context.Ninnos, "IdNinno", "IdentificacionNinno");
            return View();
        }

        // POST: RegistroBaja/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdRegistroBaja,FechaBaja,IdNinno")] RegistroBaja registroBaja)
        {
            if (ModelState.IsValid)
            {
                SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
                SqlCommand cmd = conn.CreateCommand();
                conn.Open();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "sp_IngresarRegistroBajas";
                cmd.Parameters.Add("@fechaBaja", System.Data.SqlDbType.Date).Value = registroBaja.FechaBaja;
                cmd.Parameters.Add("@idNinno", System.Data.SqlDbType.Int).Value = registroBaja.IdNinno;
                await cmd.ExecuteNonQueryAsync();
                conn.Close();
                //_context.Add(registroBaja);
                //await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdNinno"] = new SelectList(_context.Ninnos, "IdNinno", "IdentificacionNinno", registroBaja.IdNinno);
            return View(registroBaja);
        }

        // GET: RegistroBaja/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var registroBaja = await _context.RegistroBajas.FindAsync(id);
            if (registroBaja == null)
            {
                return NotFound();
            }
            ViewData["IdNinno"] = new SelectList(_context.Ninnos, "IdNinno", "Apell1Ninno", registroBaja.IdNinno);
            return View(registroBaja);
        }

        // POST: RegistroBaja/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdRegistroBaja,FechaBaja,IdNinno")] RegistroBaja registroBaja)
        {
            if (id != registroBaja.IdRegistroBaja)
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
                    cmd.CommandText = "sp_ModificarRegistroBajas";
                    cmd.Parameters.Add("@idRegistroBaja", System.Data.SqlDbType.Int).Value = registroBaja.IdRegistroBaja;
                    cmd.Parameters.Add("@fechaBaja", System.Data.SqlDbType.Date).Value = registroBaja.FechaBaja;
                    cmd.Parameters.Add("@idNinno", System.Data.SqlDbType.Int).Value = registroBaja.IdNinno;
                    await cmd.ExecuteNonQueryAsync();
                    conn.Close();
                    //_context.Update(registroBaja);
                    //await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RegistroBajaExists(registroBaja.IdRegistroBaja))
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
            ViewData["IdNinno"] = new SelectList(_context.Ninnos, "IdNinno", "IdentificacionNinno", registroBaja.IdNinno);
            return View(registroBaja);
        }

        // GET: RegistroBaja/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var registroBaja = await _context.RegistroBajas
                .Include(r => r.IdNinnoNavigation)
                .FirstOrDefaultAsync(m => m.IdRegistroBaja == id);
            if (registroBaja == null)
            {
                return NotFound();
            }

            return View(registroBaja);
        }

        // POST: RegistroBaja/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
            SqlCommand cmd = conn.CreateCommand();
            conn.Open();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "sp_EliminarRegistroBajas";
            cmd.Parameters.Add("@idRegistroBaja", System.Data.SqlDbType.Int).Value = id;
            await cmd.ExecuteNonQueryAsync();
            conn.Close();
            //var registroBaja = await _context.RegistroBajas.FindAsync(id);
            //_context.RegistroBajas.Remove(registroBaja);
            //await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RegistroBajaExists(int id)
        {
            return _context.RegistroBajas.Any(e => e.IdRegistroBaja == id);
        }
    }
}
