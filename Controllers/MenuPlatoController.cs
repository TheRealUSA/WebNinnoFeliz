﻿using System;
using System.Collections.Generic;
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
    public class MenuPlatoController : Controller
    {
        private readonly WebNinnoFelizContext _context;

        public MenuPlatoController(WebNinnoFelizContext context)
        {
            _context = context;
        }

        // GET: MenuPlato
        public async Task<IActionResult> Index()
        {
            var webNinnoFelizContext = _context.MenuPlatos.Include(m => m.IdNumeroMenuNavigation).Include(m => m.IdPlatoNavigation);
            return View(await webNinnoFelizContext.ToListAsync());
        }

        // GET: MenuPlato/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var menuPlato = await _context.MenuPlatos
                .Include(m => m.IdNumeroMenuNavigation)
                .Include(m => m.IdPlatoNavigation)
                .FirstOrDefaultAsync(m => m.IdnumeroMenuPlato == id);
            if (menuPlato == null)
            {
                return NotFound();
            }

            return View(menuPlato);
        }

        // GET: MenuPlato/Create
        public IActionResult Create()
        {
            ViewData["IdNumeroMenu"] = new SelectList(_context.Menus, "IdNumeroMenu", "NombreMenu");
            ViewData["IdPlato"] = new SelectList(_context.Platos, "IdPlato", "NombrePlato");
            return View();
        }

        // POST: MenuPlato/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdnumeroMenuPlato,IdNumeroMenu,IdPlato")] MenuPlato menuPlato)
        {
            if (ModelState.IsValid)
            {
                SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
                SqlCommand cmd = conn.CreateCommand();
                conn.Open();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "sp_IngresarMenu_Platos";
                cmd.Parameters.Add("@idNumeroMenu", System.Data.SqlDbType.Int).Value = menuPlato.IdNumeroMenu;
                cmd.Parameters.Add("@idPlato", System.Data.SqlDbType.Int).Value = menuPlato.IdPlato;
                await cmd.ExecuteNonQueryAsync();
                conn.Close();
                //_context.Add(menuPlato);
                //await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdNumeroMenu"] = new SelectList(_context.Menus, "IdNumeroMenu", "NombreMenu", menuPlato.IdNumeroMenu);
            ViewData["IdPlato"] = new SelectList(_context.Platos, "IdPlato", "NombrePlato", menuPlato.IdPlato);
            return View(menuPlato);
        }

        // GET: MenuPlato/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var menuPlato = await _context.MenuPlatos.FindAsync(id);
            if (menuPlato == null)
            {
                return NotFound();
            }
            ViewData["IdNumeroMenu"] = new SelectList(_context.Menus, "IdNumeroMenu", "NombreMenu", menuPlato.IdNumeroMenu);
            ViewData["IdPlato"] = new SelectList(_context.Platos, "IdPlato", "NombrePlato", menuPlato.IdPlato);
            return View(menuPlato);
        }

        // POST: MenuPlato/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdnumeroMenuPlato,IdNumeroMenu,IdPlato")] MenuPlato menuPlato)
        {
            if (id != menuPlato.IdnumeroMenuPlato)
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
                    cmd.CommandText = "sp_ModificarMenu_Platos";
                    cmd.Parameters.Add("@idnumeroMenuPlato", System.Data.SqlDbType.Int).Value = menuPlato.IdnumeroMenuPlato;
                    cmd.Parameters.Add("@idNumeroMenu", System.Data.SqlDbType.Int).Value = menuPlato.IdNumeroMenu;
                    cmd.Parameters.Add("@idPlato", System.Data.SqlDbType.Int).Value = menuPlato.IdPlato;
                    await cmd.ExecuteNonQueryAsync();
                    conn.Close();
                    //_context.Update(menuPlato);
                    //await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MenuPlatoExists(menuPlato.IdnumeroMenuPlato))
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
            ViewData["IdNumeroMenu"] = new SelectList(_context.Menus, "IdNumeroMenu", "NombreMenu", menuPlato.IdNumeroMenu);
            ViewData["IdPlato"] = new SelectList(_context.Platos, "IdPlato", "NombrePlato", menuPlato.IdPlato);
            return View(menuPlato);
        }

        // GET: MenuPlato/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var menuPlato = await _context.MenuPlatos
                .Include(m => m.IdNumeroMenuNavigation)
                .Include(m => m.IdPlatoNavigation)
                .FirstOrDefaultAsync(m => m.IdnumeroMenuPlato == id);
            if (menuPlato == null)
            {
                return NotFound();
            }

            return View(menuPlato);
        }

        // POST: MenuPlato/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
            SqlCommand cmd = conn.CreateCommand();
            conn.Open();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "sp_EliminarMenu_Platos";
            cmd.Parameters.Add("@idnumeroMenuPlato", System.Data.SqlDbType.Int).Value = id;
            await cmd.ExecuteNonQueryAsync();
            conn.Close();
            //var menuPlato = await _context.MenuPlatos.FindAsync(id);
            //_context.MenuPlatos.Remove(menuPlato);
            //await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MenuPlatoExists(int id)
        {
            return _context.MenuPlatos.Any(e => e.IdnumeroMenuPlato == id);
        }
    }
}
