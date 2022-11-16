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

namespace WebNinnoFeliz.Controllers
{
    public class ConsIngredienteController : Controller
    {
        List<Ingrediente> listaIngrediente = new List<Ingrediente>();
        SqlDataAdapter adapter;
        private readonly WebNinnoFelizContext _context;

        public ConsIngredienteController(WebNinnoFelizContext context)
        {
            _context = context;
        }

        //Listar
        public List<Ingrediente> ListarIngrediente()
        {
            DataTable datatable = new DataTable();
            string error;
            try
            {
                SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
                adapter = new SqlDataAdapter("sp_listarIngredientes", conn); 
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
                            Ingrediente ninno = new Ingrediente();
                            ninno.IdIngrediente = Int32.Parse(datatable.Rows[i][0].ToString());
                            ninno.NombreIngrediente = datatable.Rows[i][1].ToString();
                            listaIngrediente.Add(ninno);
                        }
                    }
                    conn.Close();
                }

            }
            catch (Exception e)
            {
                error = e.InnerException.Message;
            }

            return listaIngrediente;
        }

        // GET: TipoAlergia

        public IActionResult Index()
        {
            //var webNinnoFelizContext = _context.Ninnos.Include(n => n.IdGeneroNavigation);
            return View(ListarIngrediente());
        }


      

        // GET: Ingrediente/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Ingrediente/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdIngrediente,NombreIngrediente")] Ingrediente ingrediente)
        {
            if (ModelState.IsValid)
            {
                SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
                SqlCommand cmd = conn.CreateCommand();
                conn.Open();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "sp_IngresarIngredientes";
                cmd.Parameters.Add("@nombreIngrediente", System.Data.SqlDbType.VarChar, 20).Value = ingrediente.NombreIngrediente;
                await cmd.ExecuteNonQueryAsync();
                conn.Close();
                //_context.Add(ingrediente);
                //await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(ingrediente);
        }

        // GET: Ingrediente/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ingrediente = await _context.Ingredientes.FindAsync(id);
            if (ingrediente == null)
            {
                return NotFound();
            }
            return View(ingrediente);
        }

        // POST: Ingrediente/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdIngrediente,NombreIngrediente")] Ingrediente ingrediente)
        {
            if (id != ingrediente.IdIngrediente)
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
                    cmd.CommandText = "sp_ModificarIngredientes"; 
                    cmd.Parameters.Add("@idIngrediente", System.Data.SqlDbType.Int).Value = ingrediente.IdIngrediente;
                    cmd.Parameters.Add("@nombreIngrediente ", System.Data.SqlDbType.VarChar, 20).Value = ingrediente.NombreIngrediente;
                    await cmd.ExecuteNonQueryAsync();
                    conn.Close();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!IngredienteExists(ingrediente.IdIngrediente))
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
            return View(ingrediente);
        }


        private bool IngredienteExists(int id)
        {
            return _context.Ingredientes.Any(e => e.IdIngrediente == id);
        }
    }
}
