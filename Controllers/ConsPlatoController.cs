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
    public class ConsPlatoController : Controller
    {
        List<Plato> listaPlato = new List<Plato>();
        SqlDataAdapter adapter;

        private readonly WebNinnoFelizContext _context;

        public ConsPlatoController(WebNinnoFelizContext context)
        {
            _context = context;
        }
        //Listar
        public List<Plato> ListarPlato()
        {
            DataTable datatable = new DataTable();
            string error;
            try
            {
                SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
                adapter = new SqlDataAdapter("sp_listarPlatos", conn);
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
                            Plato ninno = new Plato();
                            ninno.IdPlato = Int32.Parse(datatable.Rows[i][0].ToString());
                            ninno.NombrePlato = datatable.Rows[i][1].ToString();
                            ninno.PrecioPlato = datatable.Rows[i][2].ToString();
                            listaPlato.Add(ninno);
                        }
                    }
                    conn.Close();
                }

            }
            catch (Exception e)
            {
                error = e.InnerException.Message;
            }

            return listaPlato;
        }

        // GET: TipoAlergia

        public IActionResult Index()
        {
            //var webNinnoFelizContext = _context.Ninnos.Include(n => n.IdGeneroNavigation);
            return View(ListarPlato());
        }

        // GET: Plato/Details/5
        

        // GET: Plato/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Plato/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdPlato,NombrePlato,PrecioPlato")] Plato plato)
        {
            if (ModelState.IsValid)
            {
                SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
                SqlCommand cmd = conn.CreateCommand();
                conn.Open();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "sp_IngresarPlatos";
                cmd.Parameters.Add("@nombrePlato", System.Data.SqlDbType.VarChar, 20).Value = plato.NombrePlato;
                cmd.Parameters.Add("@precioPlato", System.Data.SqlDbType.VarChar, 10).Value = plato.PrecioPlato;
                await cmd.ExecuteNonQueryAsync();
                conn.Close();
                //_context.Add(plato);
                //await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(plato);
        }

        // GET: Plato/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var plato = await _context.Platos.FindAsync(id);
            if (plato == null)
            {
                return NotFound();
            }
            return View(plato);
        }

        // POST: Plato/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdPlato,NombrePlato,PrecioPlato")] Plato plato)
        {
            if (id != plato.IdPlato)
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
                    cmd.CommandText = "sp_ModificarPlatos";
                    cmd.Parameters.Add("@idPlato", System.Data.SqlDbType.Int).Value = plato.IdPlato;
                    cmd.Parameters.Add("@nombrePlato", System.Data.SqlDbType.VarChar, 20).Value = plato.NombrePlato;
                    cmd.Parameters.Add("@precioPlato", System.Data.SqlDbType.VarChar, 10).Value = plato.PrecioPlato;
                    await cmd.ExecuteNonQueryAsync();
                    conn.Close();
                    //_context.Update(plato);
                    //await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PlatoExists(plato.IdPlato))
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
            return View(plato);
        }

        private bool PlatoExists(int id)
        {
            return _context.Platos.Any(e => e.IdPlato == id);
        }
    }
}
