﻿using System;
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
    public class ConsAbonadorCargoMensualController : Controller
    {
        List<AbonadorCargoMensual> listaAbonaCargoMen = new List<AbonadorCargoMensual>();
        SqlDataAdapter adapter;

        private readonly WebNinnoFelizContext _context;

        public ConsAbonadorCargoMensualController(WebNinnoFelizContext context)
        {
            _context = context;
        }

        public List<AbonadorCargoMensual> ListarAbonaCargoMen()
        {
            DataTable datatable = new DataTable();
            string error;
            try
            {
                SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
                adapter = new SqlDataAdapter("sp_listarAbonador_CargoMensuales", conn);
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
                            AbonadorCargoMensual ninno = new AbonadorCargoMensual();
                            ninno.IdAbonadorCargoMensual = Int32.Parse(datatable.Rows[i][0].ToString());
                            ninno.NombreEncargado = datatable.Rows[i][1].ToString();
                            ninno.Apell1Encargado = datatable.Rows[i][2].ToString();
                            ninno.Apell2Encargado = datatable.Rows[i][3].ToString();
                            ninno.NumeroCuenta = Int32.Parse(datatable.Rows[i][4].ToString());
                            ninno.CargoMensual = datatable.Rows[i][5].ToString();
                            ninno.NombreNinno = datatable.Rows[i][6].ToString();
                            ninno.Apell1Ninno = datatable.Rows[i][7].ToString();
                            ninno.Apell2Ninno = datatable.Rows[i][8].ToString();
                            listaAbonaCargoMen.Add(ninno);
                        }
                    }
                    conn.Close();
                }

            }
            catch (Exception e)
            {
                error = e.InnerException.Message;
            }

            return listaAbonaCargoMen;
        }


        public IActionResult Index()
        {
            //var webNinnoFelizContext = _context.Ninnos.Include(n => n.IdGeneroNavigation);
            return View(ListarAbonaCargoMen());
        }

    

        // GET: AbonadorCargoMensuale/Create
        public IActionResult Create()
        {
            ViewData["IdAbonador"] = new SelectList(_context.Abonadores, "IdAbonador", "NumeroCuenta");
            ViewData["IdCargo"] = new SelectList(_context.CargoMensuales, "IdCargo", "IdCargo");
            return View();
        }

        // POST: AbonadorCargoMensuale/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdAbonadorCargoMensual,IdAbonador,IdCargo")] AbonadorCargoMensuale abonadorCargoMensuale)
        {
            if (ModelState.IsValid)
            {
                SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
                SqlCommand cmd = conn.CreateCommand();
                conn.Open();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "sp_IngresarAbonador_CargoMensuales";
                //cmd.Parameters.Add("@idAbonadorCargoMensual", System.Data.SqlDbType.Int).Value = abonadorCargoMensuale.IdAbonadorCargoMensual;
                cmd.Parameters.Add("@idAbonador", System.Data.SqlDbType.Int).Value = abonadorCargoMensuale.IdAbonador;
                cmd.Parameters.Add("@idCargo", System.Data.SqlDbType.Int).Value = abonadorCargoMensuale.IdCargo;
                await cmd.ExecuteNonQueryAsync();
                conn.Close();
                //_context.Add(abonadorCargoMensuale);
                //await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdAbonador"] = new SelectList(_context.Abonadores, "IdAbonador", "NumeroCuenta", abonadorCargoMensuale.IdAbonador);
            ViewData["IdCargo"] = new SelectList(_context.CargoMensuales, "IdCargo", "IdCargo", abonadorCargoMensuale.IdCargo);
            return View(abonadorCargoMensuale);
        }

        private bool AbonadorCargoMensualeExists(int id)
        {
            return _context.AbonadorCargoMensuales.Any(e => e.IdAbonadorCargoMensual == id);
        }
    }
}
