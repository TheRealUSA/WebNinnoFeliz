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
    public class EncargadoMatriculaController : Controller
    {

        List<EncargadoMatriculas> listaencmatr = new List<EncargadoMatriculas>();
        SqlDataAdapter adapter;

        private readonly WebNinnoFelizContext _context;

        public EncargadoMatriculaController(WebNinnoFelizContext context)
        {
            _context = context;
        }

        public List<EncargadoMatriculas> ListaEncargadoMatr()
        {
            DataTable datatable = new DataTable();
            string error;
            try
            {
                SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
                adapter = new SqlDataAdapter("sp_listarEncargado_Matriculas", conn);
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
                            EncargadoMatriculas encargado = new EncargadoMatriculas();
                            encargado.IdEncargadoMatricula = Int32.Parse(datatable.Rows[i][0].ToString());
                            encargado.NumeroMatricula = Int32.Parse(datatable.Rows[i][1].ToString());
                            encargado.IdentificacionEncargado = datatable.Rows[i][2].ToString();
                            encargado.NombreEncargado = datatable.Rows[i][3].ToString();
                            encargado.Apell1Encargado = datatable.Rows[i][4].ToString();
                            encargado.Apell2Encargado = datatable.Rows[i][5].ToString();
                            encargado.DetallePar = datatable.Rows[i][6].ToString();
                            encargado.FechaIngreso = DateTime.Parse(datatable.Rows[i][7].ToString());
                            encargado.NombreNinno = datatable.Rows[i][8].ToString();
                            encargado.Apell1Ninno = datatable.Rows[i][9].ToString();
                            encargado.Apell2Ninno = datatable.Rows[i][10].ToString();
                            listaencmatr.Add(encargado);
                        }
                    }
                    conn.Close();
                }

            }
            catch (Exception e)
            {
                error = e.InnerException.Message;
            }

            return listaencmatr;
        }


        public IActionResult Index()
        {
            //var webNinnoFelizContext = _context.Encargados.Include(e => e.IdParentezcoNavigation);
            return View(ListaEncargadoMatr());
        }

        // GET: EncargadoMatricula/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var encargadoMatricula = await _context.EncargadoMatriculas
                .Include(e => e.IdEncargadoNavigation)
                .Include(e => e.NumeroMatriculaNavigation)
                .FirstOrDefaultAsync(m => m.IdEncargadoMatricula == id);
            if (encargadoMatricula == null)
            {
                return NotFound();
            }

            return View(encargadoMatricula);
        }

        // GET: EncargadoMatricula/Create
        public IActionResult Create()
        {
            ViewData["IdEncargado"] = new SelectList(_context.Encargados, "IdEncargado", "IdentificacionEncargado");
            ViewData["NumeroMatricula"] = new SelectList(_context.Matriculas, "NumeroMatricula", "NumeroMatricula");
            return View();
        }

        // POST: EncargadoMatricula/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdEncargadoMatricula,NumeroMatricula,IdEncargado")] EncargadoMatricula encargadoMatricula)
        {
            if (ModelState.IsValid)
            {
                SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
                SqlCommand cmd = conn.CreateCommand();
                conn.Open();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "sp_IngresarEncargado_Matriculas";
                cmd.Parameters.Add("@numeroMatricula", System.Data.SqlDbType.Int).Value = encargadoMatricula.NumeroMatricula;
                cmd.Parameters.Add("@idEncargado", System.Data.SqlDbType.Int).Value = encargadoMatricula.IdEncargado;
                await cmd.ExecuteNonQueryAsync();
                conn.Close();
                //_context.Add(encargadoMatricula);
                //await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdEncargado"] = new SelectList(_context.Encargados, "IdEncargado", "IdentificacionEncargado", encargadoMatricula.IdEncargado);
            ViewData["NumeroMatricula"] = new SelectList(_context.Matriculas, "NumeroMatricula", "NumeroMatricula", encargadoMatricula.NumeroMatricula);
            return View(encargadoMatricula);
        }

        // GET: EncargadoMatricula/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var encargadoMatricula = await _context.EncargadoMatriculas.FindAsync(id);
            if (encargadoMatricula == null)
            {
                return NotFound();
            }
            ViewData["IdEncargado"] = new SelectList(_context.Encargados, "IdEncargado", "IdentificacionEncargado", encargadoMatricula.IdEncargado);
            ViewData["NumeroMatricula"] = new SelectList(_context.Matriculas, "NumeroMatricula", "NumeroMatricula", encargadoMatricula.NumeroMatricula);
            return View(encargadoMatricula);
        }

        // POST: EncargadoMatricula/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdEncargadoMatricula,NumeroMatricula,IdEncargado")] EncargadoMatricula encargadoMatricula)
        {
            if (id != encargadoMatricula.IdEncargadoMatricula)
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
                    cmd.CommandText = "sp_ModificarEncargado_Matriculas";
                    cmd.Parameters.Add("@idEncargadoMatricula", System.Data.SqlDbType.Int).Value = encargadoMatricula.IdEncargadoMatricula;
                    cmd.Parameters.Add("@numeroMatricula", System.Data.SqlDbType.Int).Value = encargadoMatricula.NumeroMatricula;
                    cmd.Parameters.Add("@idEncargado", System.Data.SqlDbType.Int).Value = encargadoMatricula.IdEncargado;
                    await cmd.ExecuteNonQueryAsync();
                    conn.Close();
                    //_context.Update(encargadoMatricula);
                    //await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EncargadoMatriculaExists(encargadoMatricula.IdEncargadoMatricula))
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
            ViewData["IdEncargado"] = new SelectList(_context.Encargados, "IdEncargado", "IdentificacionEncargado", encargadoMatricula.IdEncargado);
            ViewData["NumeroMatricula"] = new SelectList(_context.Matriculas, "NumeroMatricula", "NumeroMatricula", encargadoMatricula.NumeroMatricula);
            return View(encargadoMatricula);
        }

        // GET: EncargadoMatricula/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var encargadoMatricula = await _context.EncargadoMatriculas
                .Include(e => e.IdEncargadoNavigation)
                .Include(e => e.NumeroMatriculaNavigation)
                .FirstOrDefaultAsync(m => m.IdEncargadoMatricula == id);
            if (encargadoMatricula == null)
            {
                return NotFound();
            }

            return View(encargadoMatricula);
        }

        // POST: EncargadoMatricula/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
            SqlCommand cmd = conn.CreateCommand();
            conn.Open();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "sp_EliminarEncargado_Matriculas";
            cmd.Parameters.Add("@idEncargadoMatricula", System.Data.SqlDbType.Int).Value = id;
            await cmd.ExecuteNonQueryAsync();
            conn.Close();
            //var encargadoMatricula = await _context.EncargadoMatriculas.FindAsync(id);
            //_context.EncargadoMatriculas.Remove(encargadoMatricula);
            //await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EncargadoMatriculaExists(int id)
        {
            return _context.EncargadoMatriculas.Any(e => e.IdEncargadoMatricula == id);
        }
    }
}
