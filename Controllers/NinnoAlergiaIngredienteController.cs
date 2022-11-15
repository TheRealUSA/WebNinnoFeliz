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
using WebNinnoFeliz.Models.ViewModels;
using System.Data;
namespace WebNinnoFeliz.Controllers
{
    public class NinnoAlergiaIngredienteController : Controller
    {

        List<ListarNinno_Alergia_Ingredientes> listaalergiaIn = new List<ListarNinno_Alergia_Ingredientes>();
        SqlDataAdapter adapter;
        private readonly WebNinnoFelizContext _context;

        public NinnoAlergiaIngredienteController(WebNinnoFelizContext context)
        {
            _context = context;
        }
        //Listar
        public List<ListarNinno_Alergia_Ingredientes> ListarAlergiasIn()
        {
            DataTable datatable = new DataTable();
            string error;
            try
            {
                SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
                adapter = new SqlDataAdapter("sp_listarNinno_Alergia_Ingredientes", conn);
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
                            ListarNinno_Alergia_Ingredientes ninno = new ListarNinno_Alergia_Ingredientes();
                            ninno.idNinnoAlergiaIngrediente = Int32.Parse(datatable.Rows[i][0].ToString());
                            ninno.nombreAlergia = datatable.Rows[i][1].ToString();
                            ninno.NombreIngrediente = datatable.Rows[i][2].ToString();
                            ninno.IdentificacionNinno = datatable.Rows[i][3].ToString();
                            ninno.NombreNinno = datatable.Rows[i][4].ToString();
                            ninno.Apell1Ninno = datatable.Rows[i][5].ToString();
                            ninno.Apell2Ninno = datatable.Rows[i][6].ToString();
                            listaalergiaIn.Add(ninno);
                        }
                    }
                    conn.Close();
                }

            }
            catch (Exception e)
            {
                error = e.InnerException.Message;
            }

            return listaalergiaIn;
        }

        // GET: Alergia

        public IActionResult Index()
        {
            //var webNinnoFelizContext = _context.Ninnos.Include(n => n.IdGeneroNavigation);
            return View(ListarAlergiasIn());
        }

        public async Task<IActionResult> PDF()
        {
            return new ViewAsPdf(await _context.NinnoAlergiaIngredientes.Include(n => n.IdAlergiaNavigation).Include(n => n.IdIngredienteNavigation).Include(n => n.IdNinnoNavigation).ToListAsync())
            {
                FileName = "NiñoAlergiaIngrediente.pdf"
            };
        }

        // GET: NinnoAlergiaIngrediente/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ninnoAlergiaIngrediente = await _context.NinnoAlergiaIngredientes
                .Include(n => n.IdAlergiaNavigation)
                .Include(n => n.IdIngredienteNavigation)
                .Include(n => n.IdNinnoNavigation)
                .FirstOrDefaultAsync(m => m.IdNinnoAlergiaIngrediente == id);
            if (ninnoAlergiaIngrediente == null)
            {
                return NotFound();
            }

            return View(ninnoAlergiaIngrediente);
        }

        // GET: NinnoAlergiaIngrediente/Create
        public IActionResult Create()
        {
            ViewData["IdAlergia"] = new SelectList(_context.Alergias, "IdAlergia", "NombreAlergia");
            ViewData["IdIngrediente"] = new SelectList(_context.Ingredientes, "IdIngrediente", "NombreIngrediente");
            ViewData["IdNinno"] = new SelectList(_context.Ninnos, "IdNinno", "IdentificacionNinno");
            return View();
        }

        // POST: NinnoAlergiaIngrediente/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdNinnoAlergiaIngrediente,IdAlergia,IdIngrediente,IdNinno")] NinnoAlergiaIngrediente ninnoAlergiaIngrediente)
        {
            if (ModelState.IsValid)
            {
                SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
                SqlCommand cmd = conn.CreateCommand();
                conn.Open();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "sp_IngresarNinno_Alergia_Ingredientes";
                cmd.Parameters.Add("@idNinno ", System.Data.SqlDbType.Int).Value = ninnoAlergiaIngrediente.IdNinno;
                cmd.Parameters.Add("@idAlergia", System.Data.SqlDbType.Int).Value = ninnoAlergiaIngrediente.IdNinno;
                cmd.Parameters.Add("@idIngrediente", System.Data.SqlDbType.Int).Value = ninnoAlergiaIngrediente.IdIngrediente;
                await cmd.ExecuteNonQueryAsync();
                conn.Close();
                //_context.Add(ninnoAlergiaIngrediente);
                //await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdAlergia"] = new SelectList(_context.Alergias, "IdAlergia", "NombreAlergia", ninnoAlergiaIngrediente.IdAlergia);
            ViewData["IdIngrediente"] = new SelectList(_context.Ingredientes, "IdIngrediente", "NombreIngrediente", ninnoAlergiaIngrediente.IdIngrediente);
            ViewData["IdNinno"] = new SelectList(_context.Ninnos, "IdNinno", "IdentificacionNinno", ninnoAlergiaIngrediente.IdNinno);
            return View(ninnoAlergiaIngrediente);
        }

        // GET: NinnoAlergiaIngrediente/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ninnoAlergiaIngrediente = await _context.NinnoAlergiaIngredientes.FindAsync(id);
            if (ninnoAlergiaIngrediente == null)
            {
                return NotFound();
            }
            ViewData["IdAlergia"] = new SelectList(_context.Alergias, "IdAlergia", "NombreAlergia", ninnoAlergiaIngrediente.IdAlergia);
            ViewData["IdIngrediente"] = new SelectList(_context.Ingredientes, "IdIngrediente", "NombreIngrediente", ninnoAlergiaIngrediente.IdIngrediente);
            ViewData["IdNinno"] = new SelectList(_context.Ninnos, "IdNinno", "IdentificacionNinno", ninnoAlergiaIngrediente.IdNinno);
            return View(ninnoAlergiaIngrediente);
        }

        // POST: NinnoAlergiaIngrediente/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdNinnoAlergiaIngrediente,IdAlergia,IdIngrediente,IdNinno")] NinnoAlergiaIngrediente ninnoAlergiaIngrediente)
        {
            if (id != ninnoAlergiaIngrediente.IdNinnoAlergiaIngrediente)
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
                    cmd.CommandText = "sp_ModificarNinno_Alergia_Ingredientes";
                    cmd.Parameters.Add("@idNinnoAlergiaIngrediente", System.Data.SqlDbType.Int).Value = ninnoAlergiaIngrediente.IdNinnoAlergiaIngrediente;
                    cmd.Parameters.Add("@idNinno ", System.Data.SqlDbType.Int).Value = ninnoAlergiaIngrediente.IdNinno;
                    cmd.Parameters.Add("@idAlergia", System.Data.SqlDbType.Int).Value = ninnoAlergiaIngrediente.IdNinno;
                    cmd.Parameters.Add("@idIngrediente", System.Data.SqlDbType.Int).Value = ninnoAlergiaIngrediente.IdIngrediente;
                    await cmd.ExecuteNonQueryAsync();
                    conn.Close();
                    //_context.Update(ninnoAlergiaIngrediente);
                    //await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NinnoAlergiaIngredienteExists(ninnoAlergiaIngrediente.IdNinnoAlergiaIngrediente))
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
            ViewData["IdAlergia"] = new SelectList(_context.Alergias, "IdAlergia", "NombreAlergia", ninnoAlergiaIngrediente.IdAlergia);
            ViewData["IdIngrediente"] = new SelectList(_context.Ingredientes, "IdIngrediente", "NombreIngrediente", ninnoAlergiaIngrediente.IdIngrediente);
            ViewData["IdNinno"] = new SelectList(_context.Ninnos, "IdNinno", "IdentificacionNinno", ninnoAlergiaIngrediente.IdNinno);
            return View(ninnoAlergiaIngrediente);
        }

        // GET: NinnoAlergiaIngrediente/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ninnoAlergiaIngrediente = await _context.NinnoAlergiaIngredientes
                .Include(n => n.IdAlergiaNavigation)
                .Include(n => n.IdIngredienteNavigation)
                .Include(n => n.IdNinnoNavigation)
                .FirstOrDefaultAsync(m => m.IdNinnoAlergiaIngrediente == id);
            if (ninnoAlergiaIngrediente == null)
            {
                return NotFound();
            }

            return View(ninnoAlergiaIngrediente);
        }

        // POST: NinnoAlergiaIngrediente/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
            SqlCommand cmd = conn.CreateCommand();
            conn.Open();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "sp_EliminarNinno_Alergia_Ingredientes";
            cmd.Parameters.Add("@idNinnoAlergiaIngrediente", System.Data.SqlDbType.Int).Value = id;
            await cmd.ExecuteNonQueryAsync();
            conn.Close();
            //var ninnoAlergiaIngrediente = await _context.NinnoAlergiaIngredientes.FindAsync(id);
            //_context.NinnoAlergiaIngredientes.Remove(ninnoAlergiaIngrediente);
            //await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NinnoAlergiaIngredienteExists(int id)
        {
            return _context.NinnoAlergiaIngredientes.Any(e => e.IdNinnoAlergiaIngrediente == id);
        }
    }
}
