using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using WebNinnoFeliz.Data;
using System.Data;
using WebNinnoFeliz.Models;
using WebNinnoFeliz.Models.ViewModels;

namespace WebNinnoFeliz.Controllers
{
    public class GeneroController : Controller
    {
        private readonly WebNinnoFelizContext _context;
        List<Historico> listaHistorico = new List<Historico>();
        SqlDataAdapter adapter;
        public GeneroController(WebNinnoFelizContext context)
        {
            _context = context;
        }

        // Consulta

        public List<Historico> ListarHistorico()
        {
            DataTable datatable = new DataTable();
            string error;
            try
            {
                SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
                adapter = new SqlDataAdapter("sp_HISTORICO", conn);
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
                            Historico NiAl = new Historico();
                            NiAl.id = Int32.Parse(datatable.Rows[i][0].ToString());
                            NiAl.Fecha = datatable.Rows[i][1].ToString();
                            NiAl.descripcion = datatable.Rows[i][2].ToString();
                            listaHistorico.Add(NiAl);
                        }

                    }
                    conn.Close();
                }

            }
            catch (Exception e)
            {
                error = e.InnerException.Message;
            }

            return listaHistorico;
        }

        public IActionResult ConsHistorico()
        {
            return View(ListarHistorico());
        }

        //GET: Genero
        public async Task<IActionResult> Index()
        {
            return View(await _context.Generos.ToListAsync());
        }

        //public IActionResult Index()
        //{
        //    return View();
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Index([Bind("IdGenero,DetalleGen")] Genero genero)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
        //        SqlCommand cmd = conn.CreateCommand();
        //        conn.Open();
        //        cmd.CommandType = System.Data.CommandType.StoredProcedure;
        //        cmd.CommandText = "sp_BuscarGenero";
        //        cmd.Parameters.Add("@detalleGen", System.Data.SqlDbType.VarChar, 15).Value = genero.DetalleGen;
        //        await cmd.ExecuteNonQueryAsync();
        //        conn.Close();
        //        // _context.Add(genero);
        //        //await _context.SaveChangesAsync();
        //        //return RedirectToAction(nameof(Index));
        //    }
        //    return View(genero);
        //}


        // GET: Genero/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var genero = await _context.Generos
                .FirstOrDefaultAsync(m => m.IdGenero == id);
            if (genero == null)
            {
                return NotFound();
            }

            return View(genero);
        }

        // GET: Genero/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Genero/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdGenero,DetalleGen")] Genero genero)
        {
            if (ModelState.IsValid)
            {
                SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
                SqlCommand cmd = conn.CreateCommand();
                conn.Open();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "sp_IngresarGenero";
                cmd.Parameters.Add("@detalleGen", System.Data.SqlDbType.VarChar, 15).Value = genero.DetalleGen;
                await cmd.ExecuteNonQueryAsync();
                conn.Close();
                // _context.Add(genero);
                //await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(genero);
        }

        // GET: Genero/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var genero = await _context.Generos.FindAsync(id);
            if (genero == null)
            {
                return NotFound();
            }
            return View(genero);
        }

        // POST: Genero/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdGenero,DetalleGen")] Genero genero)
        {
            if (id != genero.IdGenero)
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
                    cmd.CommandText = "sp_ModificarGenero";
                    cmd.Parameters.Add("@idGenero", System.Data.SqlDbType.Int).Value = genero.IdGenero;
                    cmd.Parameters.Add("@detalleGen", System.Data.SqlDbType.VarChar, 15).Value = genero.DetalleGen;
                    await cmd.ExecuteNonQueryAsync();
                    conn.Close();
                    //_context.Update(genero);
                    //await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GeneroExists(genero.IdGenero))
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
            return View(genero);
        }

        // GET: Genero/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var genero = await _context.Generos
                .FirstOrDefaultAsync(m => m.IdGenero == id);
            if (genero == null)
            {
                return NotFound();
            }

            return View(genero);
        }

        // POST: Genero/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
            SqlCommand cmd = conn.CreateCommand();
            conn.Open();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "sp_EliminarGenero";
            cmd.Parameters.Add("@idGenero", System.Data.SqlDbType.Int).Value = id;
            await cmd.ExecuteNonQueryAsync();
            conn.Close();
            //var genero = await _context.Generos.FindAsync(id);
            //_context.Generos.Remove(genero);
            //await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GeneroExists(int id)
        {
            return _context.Generos.Any(e => e.IdGenero == id);
        }
    }
}
