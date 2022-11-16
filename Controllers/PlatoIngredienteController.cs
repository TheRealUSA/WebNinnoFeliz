using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using WebNinnoFeliz.Data;
using WebNinnoFeliz.Models;
using WebNinnoFeliz.Models.ViewModels;
using System.Data;
namespace WebNinnoFeliz.Controllers
{
    public class PlatoIngredienteController : Controller
    {
        List<ListarPlato_Ingredientes> listaPlatoIn = new List<ListarPlato_Ingredientes>();
        SqlDataAdapter adapter;
        private readonly WebNinnoFelizContext _context;

        public PlatoIngredienteController(WebNinnoFelizContext context)
        {
            _context = context;
        }

        //Listar
        public List<ListarPlato_Ingredientes> ListarPlatoIngredientes()
        {
            DataTable datatable = new DataTable();
            string error;
            try
            {
                SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
                adapter = new SqlDataAdapter("sp_listarPlato_Ingredientes", conn);
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
                            ListarPlato_Ingredientes ninno = new ListarPlato_Ingredientes();
                            ninno.IdPlatoIngrediente = Int32.Parse(datatable.Rows[i][0].ToString());
                            ninno.NombrePlato = datatable.Rows[i][1].ToString();
                            ninno.NombreIngrediente = datatable.Rows[i][2].ToString();
   
                            listaPlatoIn.Add(ninno);
                        }
                    }
                    conn.Close();
                }

            }
            catch (Exception e)
            {
                error = e.InnerException.Message;
            }

            return listaPlatoIn;
        }

        // GET: Alergia

        public IActionResult Index()
        {
            //var webNinnoFelizContext = _context.Ninnos.Include(n => n.IdGeneroNavigation);
            return View(ListarPlatoIngredientes());
        }


        // GET: PlatoIngrediente/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var platoIngrediente = await _context.PlatoIngredientes
                .Include(p => p.IdIngredienteNavigation)
                .Include(p => p.IdPlatoNavigation)
                .FirstOrDefaultAsync(m => m.IdPlatoIngrediente == id);
            if (platoIngrediente == null)
            {
                return NotFound();
            }

            return View(platoIngrediente);
        }

        // GET: PlatoIngrediente/Create
        public IActionResult Create()
        {
            ViewData["IdIngrediente"] = new SelectList(_context.Ingredientes, "IdIngrediente", "NombreIngrediente");
            ViewData["IdPlato"] = new SelectList(_context.Platos, "IdPlato", "NombrePlato");
            return View();
        }

        // POST: PlatoIngrediente/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdPlatoIngrediente,IdPlato,IdIngrediente")] PlatoIngrediente platoIngrediente)
        {
            if (ModelState.IsValid)
            {
                SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
                SqlCommand cmd = conn.CreateCommand();
                conn.Open();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "sp_IngresarPlato_Ingredientes";
                cmd.Parameters.Add("@idIngrediente", System.Data.SqlDbType.Int).Value = platoIngrediente.IdIngrediente;
                cmd.Parameters.Add("@idPlato", System.Data.SqlDbType.Int).Value = platoIngrediente.IdPlato;
                await cmd.ExecuteNonQueryAsync();
                conn.Close();
                //_context.Add(platoIngrediente);
                //await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdIngrediente"] = new SelectList(_context.Ingredientes, "IdIngrediente", "NombreIngrediente", platoIngrediente.IdIngrediente);
            ViewData["IdPlato"] = new SelectList(_context.Platos, "IdPlato", "NombrePlato", platoIngrediente.IdPlato);
            return View(platoIngrediente);
        }

        // GET: PlatoIngrediente/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var platoIngrediente = await _context.PlatoIngredientes.FindAsync(id);
            if (platoIngrediente == null)
            {
                return NotFound();
            }
            ViewData["IdIngrediente"] = new SelectList(_context.Ingredientes, "IdIngrediente", "NombreIngrediente", platoIngrediente.IdIngrediente);
            ViewData["IdPlato"] = new SelectList(_context.Platos, "IdPlato", "NombrePlato", platoIngrediente.IdPlato);
            return View(platoIngrediente);
        }

        // POST: PlatoIngrediente/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdPlatoIngrediente,IdPlato,IdIngrediente")] PlatoIngrediente platoIngrediente)
        {
            if (id != platoIngrediente.IdPlatoIngrediente)
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
                    cmd.CommandText = "sp_ModificarPlato_Ingredientes";
                    cmd.Parameters.Add("@idPlatoIngrediente", System.Data.SqlDbType.Int).Value = platoIngrediente.IdPlatoIngrediente;
                    cmd.Parameters.Add("@idIngrediente", System.Data.SqlDbType.Int).Value = platoIngrediente.IdIngrediente;
                    cmd.Parameters.Add("@idPlato", System.Data.SqlDbType.Int).Value = platoIngrediente.IdPlato;
                    await cmd.ExecuteNonQueryAsync();
                    conn.Close();
                    //_context.Update(platoIngrediente);
                    //await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PlatoIngredienteExists(platoIngrediente.IdPlatoIngrediente))
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
            ViewData["IdIngrediente"] = new SelectList(_context.Ingredientes, "IdIngrediente", "NombreIngrediente", platoIngrediente.IdIngrediente);
            ViewData["IdPlato"] = new SelectList(_context.Platos, "IdPlato", "NombrePlato", platoIngrediente.IdPlato);
            return View(platoIngrediente);
        }

        // GET: PlatoIngrediente/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var platoIngrediente = await _context.PlatoIngredientes
                .Include(p => p.IdIngredienteNavigation)
                .Include(p => p.IdPlatoNavigation)
                .FirstOrDefaultAsync(m => m.IdPlatoIngrediente == id);
            if (platoIngrediente == null)
            {
                return NotFound();
            }

            return View(platoIngrediente);
        }

        // POST: PlatoIngrediente/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
            SqlCommand cmd = conn.CreateCommand();
            conn.Open();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "sp_EliminarPlato_Ingredientes";
            cmd.Parameters.Add("@idPlatoIngrediente", System.Data.SqlDbType.Int).Value = id;
            await cmd.ExecuteNonQueryAsync();
            conn.Close();
            //var platoIngrediente = await _context.PlatoIngredientes.FindAsync(id);
            //_context.PlatoIngredientes.Remove(platoIngrediente);
            //await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PlatoIngredienteExists(int id)
        {
            return _context.PlatoIngredientes.Any(e => e.IdPlatoIngrediente == id);
        }
    }
}
