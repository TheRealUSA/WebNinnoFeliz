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
    public class ConsMatriculaController : Controller
    {
        List<MatriculaNinno> listaninnomatricula = new List<MatriculaNinno>();
        SqlDataAdapter adapter;

        private readonly WebNinnoFelizContext _context;

        public ConsMatriculaController(WebNinnoFelizContext context)
        {
            _context = context;
        }

        public List<MatriculaNinno> ListarNinnoMatricula()
        {
            DataTable datatable = new DataTable();
            string error;
            try
            {
                SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
                adapter = new SqlDataAdapter("sp_listarMatriculas", conn);
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
                            MatriculaNinno ninno = new MatriculaNinno();
                            ninno.NumeroMatricula = Int32.Parse(datatable.Rows[i][0].ToString());
                            ninno.FechaIngreso = DateTime.Parse(datatable.Rows[i][1].ToString());
                            ninno.IdentificacionNinno = datatable.Rows[i][2].ToString();
                            ninno.NombreNinno = datatable.Rows[i][3].ToString();
                            ninno.Apell1Ninno = datatable.Rows[i][4].ToString();
                            ninno.Apell2Ninno = datatable.Rows[i][5].ToString();

                            listaninnomatricula.Add(ninno);
                        }
                    }
                    conn.Close();
                }

            }
            catch (Exception e)
            {
                error = e.InnerException.Message;
            }

            return listaninnomatricula;
        }


        public IActionResult Index()
        {
            return View(ListarNinnoMatricula());
        }


      

        // GET: Matricula/Create
        public IActionResult Create()
        {
            ViewData["IdNinno"] = new SelectList(_context.Ninnos, "IdNinno", "IdentificacionNinno");
            return View();
        }

        // POST: Matricula/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("NumeroMatricula,FechaIngreso,IdNinno")] Matricula matricula)
        {
            if (ModelState.IsValid)
            {
                SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
                SqlCommand cmd = conn.CreateCommand();
                conn.Open();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "sp_IngresarMatriculas";
                cmd.Parameters.Add("@fechaIngreso", System.Data.SqlDbType.Date).Value = matricula.FechaIngreso;
                cmd.Parameters.Add("@idNinno", System.Data.SqlDbType.Int).Value = matricula.IdNinno;
                await cmd.ExecuteNonQueryAsync();
                conn.Close();
                //_context.Add(matricula);
                //await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdNinno"] = new SelectList(_context.Ninnos, "IdNinno", "IdentificacionNinno", matricula.IdNinno);
            return View(matricula);
        }


        private bool MatriculaExists(int id)
        {
            return _context.Matriculas.Any(e => e.NumeroMatricula == id);
        }
    }
}
