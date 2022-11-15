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
    public class EncargadoController : Controller
    {
        List<EncargadoParentezco> listaencargado = new List<EncargadoParentezco>();
        SqlDataAdapter adapter;

        private readonly WebNinnoFelizContext _context;

        public EncargadoController(WebNinnoFelizContext context)
        {
            _context = context;
        }

        public List<EncargadoParentezco> ListaEncargado()
        {
            DataTable datatable = new DataTable();
            string error;
            try
            {
                SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
                adapter = new SqlDataAdapter("sp_listarEncargados", conn);
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
                            EncargadoParentezco encargado = new EncargadoParentezco();
                            encargado.IdEncargado = Int32.Parse(datatable.Rows[i][0].ToString());
                            encargado.IdentificacionEncargado = datatable.Rows[i][1].ToString();
                            encargado.NombreEncargado = datatable.Rows[i][2].ToString();
                            encargado.Apell1Encargado = datatable.Rows[i][3].ToString();
                            encargado.Apell2Encargado = datatable.Rows[i][4].ToString();
                            encargado.TelefonoEncargado = datatable.Rows[i][5].ToString();
                            encargado.DirecciónEncargado = datatable.Rows[i][6].ToString();
                            encargado.DetallePar = datatable.Rows[i][7].ToString();
                            listaencargado.Add(encargado);
                        }
                    }
                    conn.Close();
                }

            }
            catch (Exception e)
            {
                error = e.InnerException.Message;
            }

            return listaencargado;
        }


        public IActionResult Index()
        {
            //var webNinnoFelizContext = _context.Encargados.Include(e => e.IdParentezcoNavigation);
            return View(ListaEncargado());
        }


        // GET: Encargado
        //public async Task<IActionResult> Index()
        //{
        //    var webNinnoFelizContext = _context.Encargados.Include(e => e.IdParentezcoNavigation);
        //    return View(await webNinnoFelizContext.ToListAsync());
        //}

        // GET: Encargado/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var encargado = await _context.Encargados
                .Include(e => e.IdParentezcoNavigation)
                .FirstOrDefaultAsync(m => m.IdEncargado == id);
            if (encargado == null)
            {
                return NotFound();
            }

            return View(encargado);
        }

        // GET: Encargado/Create
        public IActionResult Create()
        {
            ViewData["IdParentezco"] = new SelectList(_context.Parentezcos, "IdParentezco", "DetallePar");
            return View();
        }

        // POST: Encargado/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdEncargado,IdentificacionEncargado,NombreEncargado,Apell1Encargado,Apell2Encargado,TelefonoEncargado,DirecciónEncargado,IdParentezco")] Encargado encargado)
        {
            if (ModelState.IsValid)
            {
                SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
                SqlCommand cmd = conn.CreateCommand();
                conn.Open();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "sp_IngresarEncargados";
                cmd.Parameters.Add("@identificacionEncargado", System.Data.SqlDbType.VarChar, 15).Value = encargado.IdentificacionEncargado;
                cmd.Parameters.Add("@nombreEncargado", System.Data.SqlDbType.VarChar, 15).Value = encargado.NombreEncargado;
                cmd.Parameters.Add("@apell1Encargado", System.Data.SqlDbType.VarChar, 15).Value = encargado.Apell1Encargado;
                cmd.Parameters.Add("@apell2Encargado", System.Data.SqlDbType.VarChar, 15).Value = encargado.Apell2Encargado;
                cmd.Parameters.Add("@telefonoEncargado", System.Data.SqlDbType.VarChar, 15).Value = encargado.TelefonoEncargado;
                cmd.Parameters.Add("@direcciónEncargado", System.Data.SqlDbType.VarChar, 50).Value = encargado.DirecciónEncargado;
                cmd.Parameters.Add("@idParentezco", System.Data.SqlDbType.Int).Value = encargado.IdParentezco;
                await cmd.ExecuteNonQueryAsync();
                conn.Close();
                //_context.Add(encargado);
                //await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdParentezco"] = new SelectList(_context.Parentezcos, "IdParentezco", "DetallePar", encargado.IdParentezco);
            return View(encargado);
        }

        // GET: Encargado/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var encargado = await _context.Encargados.FindAsync(id);
            if (encargado == null)
            {
                return NotFound();
            }
            ViewData["IdParentezco"] = new SelectList(_context.Parentezcos, "IdParentezco", "DetallePar", encargado.IdParentezco);
            return View(encargado);
        }

        // POST: Encargado/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdEncargado,IdentificacionEncargado,NombreEncargado,Apell1Encargado,Apell2Encargado,TelefonoEncargado,DirecciónEncargado,IdParentezco")] Encargado encargado)
        {
            if (id != encargado.IdEncargado)
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
                    cmd.CommandText = "sp_ModificarEncargados";
                    cmd.Parameters.Add("@idEncargado", System.Data.SqlDbType.Int).Value = encargado.IdEncargado;
                    cmd.Parameters.Add("@identificacionEncargado", System.Data.SqlDbType.VarChar, 15).Value = encargado.IdentificacionEncargado;
                    cmd.Parameters.Add("@nombreEncargado", System.Data.SqlDbType.VarChar, 15).Value = encargado.NombreEncargado;
                    cmd.Parameters.Add("@apell1Encargado", System.Data.SqlDbType.VarChar, 15).Value = encargado.Apell1Encargado;
                    cmd.Parameters.Add("@apell2Encargado", System.Data.SqlDbType.VarChar, 15).Value = encargado.Apell2Encargado;
                    cmd.Parameters.Add("@telefonoEncargado", System.Data.SqlDbType.VarChar, 15).Value = encargado.TelefonoEncargado;
                    cmd.Parameters.Add("@direcciónEncargado", System.Data.SqlDbType.VarChar, 50).Value = encargado.DirecciónEncargado;
                    cmd.Parameters.Add("@idParentezco", System.Data.SqlDbType.Int).Value = encargado.IdParentezco;
                    await cmd.ExecuteNonQueryAsync();
                    conn.Close();
                    //_context.Update(encargado);
                    //await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EncargadoExists(encargado.IdEncargado))
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
            ViewData["IdParentezco"] = new SelectList(_context.Parentezcos, "IdParentezco", "DetallePar", encargado.IdParentezco);
            return View(encargado);
        }

        // GET: Encargado/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var encargado = await _context.Encargados
                .Include(e => e.IdParentezcoNavigation)
                .FirstOrDefaultAsync(m => m.IdEncargado == id);
            if (encargado == null)
            {
                return NotFound();
            }

            return View(encargado);
        }

        // POST: Encargado/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
            SqlCommand cmd = conn.CreateCommand();
            conn.Open();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "sp_EliminarEncargados";
            cmd.Parameters.Add("@idEncargado", System.Data.SqlDbType.Int).Value = id;
            await cmd.ExecuteNonQueryAsync();
            conn.Close();
            //var encargado = await _context.Encargados.FindAsync(id);
            //_context.Encargados.Remove(encargado);
            //await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EncargadoExists(int id)
        {
            return _context.Encargados.Any(e => e.IdEncargado == id);
        }
    }
}
