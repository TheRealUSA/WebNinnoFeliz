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
    public class ConsMenuController : Controller
    {
        List<Menu> listaMenu = new List<Menu>();
        SqlDataAdapter adapter;
        private readonly WebNinnoFelizContext _context;

        public ConsMenuController(WebNinnoFelizContext context)
        {
            _context = context;
        }
        //Listar
        public List<Menu> ListarMenu()
        {
            DataTable datatable = new DataTable();
            string error;
            try
            {
                SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
                adapter = new SqlDataAdapter("sp_listarMenus", conn);
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
                            Menu ninno = new Menu();
                            ninno.IdNumeroMenu = Int32.Parse(datatable.Rows[i][0].ToString());
                            ninno.NombreMenu = datatable.Rows[i][1].ToString();
                            listaMenu.Add(ninno);
                        }
                    }
                    conn.Close();
                }

            }
            catch (Exception e)
            {
                error = e.InnerException.Message;
            }

            return listaMenu;
        }

        // GET: TipoAlergia

        public IActionResult Index()
        {
            //var webNinnoFelizContext = _context.Ninnos.Include(n => n.IdGeneroNavigation);
            return View(ListarMenu());
        }
        

        // POST: Menu/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdNumeroMenu,NombreMenu")] Menu menu)
        {
            if (ModelState.IsValid)
            {
                SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
                SqlCommand cmd = conn.CreateCommand();
                conn.Open();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "sp_IngresarMenus";
                cmd.Parameters.Add("@nombreMenu", System.Data.SqlDbType.VarChar, 20).Value = menu.NombreMenu;
                await cmd.ExecuteNonQueryAsync();
                conn.Close();
                //_context.Add(menu);
                //await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(menu);
        }

        // GET: Menu/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var menu = await _context.Menus.FindAsync(id);
            if (menu == null)
            {
                return NotFound();
            }
            return View(menu);
        }

        // POST: Menu/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdNumeroMenu,NombreMenu")] Menu menu)
        {
            if (id != menu.IdNumeroMenu)
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
                    cmd.CommandText = "sp_ModificarMenus";
                    cmd.Parameters.Add("@idNumeroMenu", System.Data.SqlDbType.Int).Value = menu.IdNumeroMenu;
                    cmd.Parameters.Add("@nombreMenu", System.Data.SqlDbType.VarChar, 20).Value = menu.NombreMenu;
                    await cmd.ExecuteNonQueryAsync();
                    conn.Close();
                    //_context.Update(menu);
                    //await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MenuExists(menu.IdNumeroMenu))
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
            return View(menu);
        }
        private bool MenuExists(int id)
        {
            return _context.Menus.Any(e => e.IdNumeroMenu == id);
        }
    }
}
