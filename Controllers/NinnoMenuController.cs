using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebNinnoFeliz.Data;
using WebNinnoFeliz.Models;
using WebNinnoFeliz.Models.ViewModels;
using System.Data;
using Microsoft.Data.SqlClient;
namespace WebNinnoFeliz.Controllers
{
    public class NinnoMenuController : Controller
    {
        List<ListarNinno_Menus> listaMenu = new List<ListarNinno_Menus>();
        SqlDataAdapter adapter;
        private readonly WebNinnoFelizContext _context;

        public NinnoMenuController(WebNinnoFelizContext context)
        {
            _context = context;
        }
        //Listar
        public List<ListarNinno_Menus> ListarListarNinnoMenus()
        {
            DataTable datatable = new DataTable();
            string error;
            try
            {
                SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
                adapter = new SqlDataAdapter("sp_listarNinno_Menus", conn);
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
                            ListarNinno_Menus ninno = new ListarNinno_Menus();
                            ninno.idNinnoMenu = Int32.Parse(datatable.Rows[i][0].ToString());
                            ninno.FechaConsumido = DateTime.Parse(datatable.Rows[i][1].ToString());
                            ninno.IdentificacionNinno = datatable.Rows[i][2].ToString();
                         
                            ninno.NombreNinno = datatable.Rows[i][3].ToString();
                            ninno.Apell1Ninno = datatable.Rows[i][4].ToString();
                            ninno.Apell2Ninno = datatable.Rows[i][5].ToString();
                            ninno.NombreMenu = datatable.Rows[i][6].ToString();
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

        // GET: Alergia

        public IActionResult Index()
        {
            //var webNinnoFelizContext = _context.Ninnos.Include(n => n.IdGeneroNavigation);
            return View(ListarListarNinnoMenus());
        }

        // GET: NinnoMenu/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ninnoMenu = await _context.NinnoMenus
                .Include(n => n.IdNinnoNavigation)
                .Include(n => n.IdNumeroMenuNavigation)
                .FirstOrDefaultAsync(m => m.IdNinnoMenu == id);
            if (ninnoMenu == null)
            {
                return NotFound();
            }

            return View(ninnoMenu);
        }

        // GET: NinnoMenu/Create
        public IActionResult Create()
        {
            ViewData["IdNinno"] = new SelectList(_context.Ninnos, "IdNinno", "IdentificacionNinno");
            ViewData["IdNumeroMenu"] = new SelectList(_context.Menus, "IdNumeroMenu", "NombreMenu");
            return View();
        }

        // POST: NinnoMenu/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdNinnoMenu,FechaConsumido,IdNinno,IdNumeroMenu")] NinnoMenu ninnoMenu)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ninnoMenu);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdNinno"] = new SelectList(_context.Ninnos, "IdNinno", "IdentificacionNinno", ninnoMenu.IdNinno);
            ViewData["IdNumeroMenu"] = new SelectList(_context.Menus, "IdNumeroMenu", "NombreMenu", ninnoMenu.IdNumeroMenu);
            return View(ninnoMenu);
        }

        // GET: NinnoMenu/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ninnoMenu = await _context.NinnoMenus.FindAsync(id);
            if (ninnoMenu == null)
            {
                return NotFound();
            }
            ViewData["IdNinno"] = new SelectList(_context.Ninnos, "IdNinno", "IdentificacionNinno", ninnoMenu.IdNinno);
            ViewData["IdNumeroMenu"] = new SelectList(_context.Menus, "IdNumeroMenu", "NombreMenu", ninnoMenu.IdNumeroMenu);
            return View(ninnoMenu);
        }

        // POST: NinnoMenu/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdNinnoMenu,FechaConsumido,IdNinno,IdNumeroMenu")] NinnoMenu ninnoMenu)
        {
            if (id != ninnoMenu.IdNinnoMenu)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ninnoMenu);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NinnoMenuExists(ninnoMenu.IdNinnoMenu))
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
            ViewData["IdNinno"] = new SelectList(_context.Ninnos, "IdNinno", "Apell1Ninno", ninnoMenu.IdNinno);
            ViewData["IdNumeroMenu"] = new SelectList(_context.Menus, "IdNumeroMenu", "NombreMenu", ninnoMenu.IdNumeroMenu);
            return View(ninnoMenu);
        }

        // GET: NinnoMenu/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ninnoMenu = await _context.NinnoMenus
                .Include(n => n.IdNinnoNavigation)
                .Include(n => n.IdNumeroMenuNavigation)
                .FirstOrDefaultAsync(m => m.IdNinnoMenu == id);
            if (ninnoMenu == null)
            {
                return NotFound();
            }

            return View(ninnoMenu);
        }

        // POST: NinnoMenu/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ninnoMenu = await _context.NinnoMenus.FindAsync(id);
            _context.NinnoMenus.Remove(ninnoMenu);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NinnoMenuExists(int id)
        {
            return _context.NinnoMenus.Any(e => e.IdNinnoMenu == id);
        }
    }
}
