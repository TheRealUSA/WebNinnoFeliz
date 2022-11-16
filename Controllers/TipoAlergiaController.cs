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
    public class TipoAlergiaController : Controller
    {
        List<TipoAlergia> listaTipoAlergia = new List<TipoAlergia>();
        List<NinnosAlergicos> listaninnosalergicos = new List<NinnosAlergicos>();
        SqlDataAdapter adapter;

        private readonly WebNinnoFelizContext _context;

        public TipoAlergiaController(WebNinnoFelizContext context)
        {
            _context = context;
        }
           

        //Listar
        public List<TipoAlergia> ListarTipoAlergia()
        {
            DataTable datatable = new DataTable();
            string error;
            try
            {
                SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
                adapter = new SqlDataAdapter("sp_listartipoAlergias", conn);
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
                            TipoAlergia ninno = new TipoAlergia();
                            ninno.IdTipoAlergia = Int32.Parse(datatable.Rows[i][0].ToString());
                            ninno.NombreTipoAlergia = datatable.Rows[i][1].ToString();
                            listaTipoAlergia.Add(ninno);
                        }
                    }
                    conn.Close();
                }

            }
            catch (Exception e)
            {
                error = e.InnerException.Message;
            }

            return listaTipoAlergia;
        }

         // GET: TipoAlergia
      
        public IActionResult Index()
        {
            //var webNinnoFelizContext = _context.Ninnos.Include(n => n.IdGeneroNavigation);
            return View(ListarTipoAlergia());
        }

        // Consulta

        public List<NinnosAlergicos> ListarNinnosAlergicos()
        {
            DataTable datatable = new DataTable();
            string error;
            try
            {
                SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
                adapter = new SqlDataAdapter("sp_NinnosAlergicos", conn);
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
                            NinnosAlergicos NiAl = new NinnosAlergicos();
                            NiAl.nombreTipoAlergia = datatable.Rows[i][0].ToString();
                            NiAl.CantidadNinnos = Int32.Parse(datatable.Rows[i][1].ToString());
                            listaninnosalergicos.Add(NiAl);
                        }
                        
                    }
                    conn.Close();
                }

            }
            catch (Exception e)
            {
                error = e.InnerException.Message;
            }

            return listaninnosalergicos;
        }

        public IActionResult ConsNinnosAlergicos()
        {
            return View(ListarNinnosAlergicos());
        }

      

        // GET: TipoAlergia/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipoAlergia = await _context.TipoAlergias
                .FirstOrDefaultAsync(m => m.IdTipoAlergia == id);
            if (tipoAlergia == null)
            {
                return NotFound();
            }

            return View(tipoAlergia);
        }

        // GET: TipoAlergia/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TipoAlergia/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdTipoAlergia,NombreTipoAlergia")] TipoAlergia tipoAlergia)
        {
            if (ModelState.IsValid)
            {
                SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
                SqlCommand cmd = conn.CreateCommand();
                conn.Open();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "sp_IngresartipoAlergias";
                cmd.Parameters.Add("@nombreTipoAlergia", System.Data.SqlDbType.VarChar, 20).Value = tipoAlergia.NombreTipoAlergia;
                await cmd.ExecuteNonQueryAsync();
                conn.Close();
                //_context.Add(tipoAlergia);
                //await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tipoAlergia);
        }

        // GET: TipoAlergia/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipoAlergia = await _context.TipoAlergias.FindAsync(id);
            if (tipoAlergia == null)
            {
                return NotFound();
            }
            return View(tipoAlergia);
        }

        // POST: TipoAlergia/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdTipoAlergia,NombreTipoAlergia")] TipoAlergia tipoAlergia)
        {
            if (id != tipoAlergia.IdTipoAlergia)
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
                    cmd.CommandText = "sp_ModificartipoAlergias";
                    cmd.Parameters.Add("@idTipoAlergia", System.Data.SqlDbType.Int).Value = tipoAlergia.IdTipoAlergia;
                    cmd.Parameters.Add("@nombreTipoAlergia", System.Data.SqlDbType.VarChar, 20).Value = tipoAlergia.NombreTipoAlergia;
                    await cmd.ExecuteNonQueryAsync();
                    conn.Close();
                    //_context.Update(tipoAlergia);
                    //await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TipoAlergiaExists(tipoAlergia.IdTipoAlergia))
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
            return View(tipoAlergia);
        }

        // GET: TipoAlergia/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipoAlergia = await _context.TipoAlergias
                .FirstOrDefaultAsync(m => m.IdTipoAlergia == id);
            if (tipoAlergia == null)
            {
                return NotFound();
            }

            return View(tipoAlergia);
        }

        // POST: TipoAlergia/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
            SqlCommand cmd = conn.CreateCommand();
            conn.Open();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "sp_EliminartipoAlergias";
            cmd.Parameters.Add("@IdTipoAlergia", System.Data.SqlDbType.Int).Value = id;
            await cmd.ExecuteNonQueryAsync();
            conn.Close();
            //var tipoAlergia = await _context.TipoAlergias.FindAsync(id);
            //_context.TipoAlergias.Remove(tipoAlergia);
            //await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TipoAlergiaExists(int id)
        {
            return _context.TipoAlergias.Any(e => e.IdTipoAlergia == id);
        }
    }
}
