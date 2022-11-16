using System;
using System.Collections.Generic;
using System.Data;
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

namespace WebNinnoFeliz.Controllers
{
    public class NinnoController : Controller
    {
        
        List<NinnosPlatoAlergicos> listaPlatosA = new List<NinnosPlatoAlergicos>();
        List<NinnosResidencia> listaNinnos = new List<NinnosResidencia>();
        List<NinnasResidencia> listaNinnas = new List<NinnasResidencia>();
        List<NinnosMatriculados> listaNinnosMat = new List<NinnosMatriculados>();
        List<NinnosDadosBaja> listaNinnosBaj = new List<NinnosDadosBaja>();
        List<NinnoGenero> listaninno = new List<NinnoGenero>();
        SqlDataAdapter adapter;

        private readonly WebNinnoFelizContext _context;

        public NinnoController(WebNinnoFelizContext context)
        {
            _context = context;
        }



        // Consulta

        public List<NinnosDadosBaja> ListarNinnosDadosBaja()
        {
            DataTable datatable = new DataTable();
            string error;
            try
            {
                SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
                adapter = new SqlDataAdapter("sp_CantNinnosDadosBaja", conn);
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
                            NinnosDadosBaja NiAl = new NinnosDadosBaja();
                            NiAl.AnnoDadoBaja = datatable.Rows[i][0].ToString();
                            NiAl.CantidadNinnos = Int32.Parse(datatable.Rows[i][1].ToString());
                            listaNinnosBaj.Add(NiAl);
                        }

                    }
                    conn.Close();
                }

            }
            catch (Exception e)
            {
                error = e.InnerException.Message;
            }

            return listaNinnosBaj;
        }

        public IActionResult ConsNinnosDadosBaja()
        {
            return View(ListarNinnosDadosBaja());
        }
        // Consulta

        public List<NinnosMatriculados> ListarNinnosMatriculados()
        {
            DataTable datatable = new DataTable();
            string error;
            try
            {
                SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
                adapter = new SqlDataAdapter("sp_CantNinnosMatriculados", conn);
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
                            NinnosMatriculados NiAl = new NinnosMatriculados();
                            NiAl.AnnoMatricula = datatable.Rows[i][0].ToString();
                            NiAl.CantidadNinnos = Int32.Parse(datatable.Rows[i][1].ToString());
                            listaNinnosMat.Add(NiAl);
                        }

                    }
                    conn.Close();
                }

            }
            catch (Exception e)
            {
                error = e.InnerException.Message;
            }

            return listaNinnosMat;
        }

        public IActionResult ConsNinnosMatriculados()
        {
            return View(ListarNinnosMatriculados());
        }

        // Consulta

        public List<NinnosPlatoAlergicos> ListarNinnosPlatoAlergicos()
        {
            DataTable datatable = new DataTable();
            string error;
            try
            {
                SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
                adapter = new SqlDataAdapter("sp_NinnosPlatosAlergicos", conn);
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
                            NinnosPlatoAlergicos NiAl = new NinnosPlatoAlergicos();
                            NiAl.nombrePlato = datatable.Rows[i][0].ToString();
                            NiAl.niñosalergicosalplato = Int32.Parse(datatable.Rows[i][1].ToString());
                            listaPlatosA.Add(NiAl);
                        }

                    }
                    conn.Close();
                }

            }
            catch (Exception e)
            {
                error = e.InnerException.Message;
            }

            return listaPlatosA;
        }

        public IActionResult ConsNinnosPlatoAlergicos()
        {
            return View(ListarNinnosPlatoAlergicos());
        }



        // Consulta

        public List<NinnosResidencia> ListarNinnosResidencia()
        {
            DataTable datatable = new DataTable();
            string error;
            try
            {
                SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
                adapter = new SqlDataAdapter("sp_NinnosResidencia", conn);
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
                            NinnosResidencia NiAl = new NinnosResidencia();
                            NiAl.direccionNinno = datatable.Rows[i][0].ToString();
                            NiAl.Ninnos = Int32.Parse(datatable.Rows[i][1].ToString());
                            listaNinnos.Add(NiAl);
                        }

                    }
                    conn.Close();
                }

            }
            catch (Exception e)
            {
                error = e.InnerException.Message;
            }

            return listaNinnos;
        }

        public IActionResult ConsNinnosResidencia()
        {
            return View(ListarNinnosResidencia());
        }
        // Consulta

        public List<NinnasResidencia> ListarNinnasResidencia()
        {
            DataTable datatable = new DataTable();
            string error;
            try
            {
                SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
                adapter = new SqlDataAdapter("sp_NinnasResidencia", conn);
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
                            NinnasResidencia NiAl = new NinnasResidencia();
                            NiAl.direccionNinno = datatable.Rows[i][0].ToString();
                            NiAl.Ninnos = Int32.Parse(datatable.Rows[i][1].ToString());
                            listaNinnas.Add(NiAl);
                        }

                    }
                    conn.Close();
                }

            }
            catch (Exception e)
            {
                error = e.InnerException.Message;
            }

            return listaNinnas;
        }

        public IActionResult ConsNinnasResidencia()
        {
            return View(ListarNinnasResidencia());
        }


        //Listar
        public List<NinnoGenero> ListarNinno()
        {
            DataTable datatable = new DataTable();
            string error;
            try
            {
                SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
                adapter = new SqlDataAdapter("sp_listarNinnos", conn);
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
                            NinnoGenero ninno = new NinnoGenero();
                            ninno.IdNinno = Int32.Parse(datatable.Rows[i][0].ToString());
                            ninno.IdentificacionNinno = datatable.Rows[i][1].ToString();
                            ninno.NombreNinno = datatable.Rows[i][2].ToString();
                            ninno.Apell1Ninno = datatable.Rows[i][3].ToString();
                            ninno.Apell2Ninno = datatable.Rows[i][4].ToString();
                            ninno.FechaNacimiento = DateTime.Parse(datatable.Rows[i][5].ToString());
                            ninno.DireccionNinno = datatable.Rows[i][6].ToString();
                            ninno.DetalleGen = datatable.Rows[i][7].ToString();
                            listaninno.Add(ninno);
                        }
                    }
                    conn.Close();
                }

            }
            catch (Exception e)
            {
                error = e.InnerException.Message;
            }

            return listaninno;
        }


        public IActionResult Index()
        {
            //var webNinnoFelizContext = _context.Ninnos.Include(n => n.IdGeneroNavigation);
            return View(ListarNinno());
        }

        // GET: Ninno
        //public async Task<IActionResult> Index()
        //{
        //    var webNinnoFelizContext = _context.Ninnos.Include(n => n.IdGeneroNavigation);
        //    return View(await webNinnoFelizContext.ToListAsync());
        //}


        public async Task<IActionResult> PDF()
        {
            return new ViewAsPdf(await _context.Ninnos.Include(n => n.IdGeneroNavigation).ToListAsync())
            {
                FileName = "Niño.pdf"
            };
        }

        // GET: Ninno/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ninno = await _context.Ninnos
                .Include(n => n.IdGeneroNavigation)
                .FirstOrDefaultAsync(m => m.IdNinno == id);
            if (ninno == null)
            {
                return NotFound();
            }

            return View(ninno);
        }

        // GET: Ninno/Create
        public IActionResult Create()
        {
            ViewData["IdGenero"] = new SelectList(_context.Generos, "IdGenero", "DetalleGen");
            return View();
        }

        // POST: Ninno/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdNinno,IdentificacionNinno,NombreNinno,Apell1Ninno,Apell2Ninno,FechaNacimiento,DireccionNinno,IdGenero")] Ninno ninno)
        {
            if (ModelState.IsValid)
            {
                SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
                SqlCommand cmd = conn.CreateCommand();
                conn.Open();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "sp_IngresarNinnos";
                cmd.Parameters.Add("@IdentificacionNinno", System.Data.SqlDbType.VarChar, 15).Value = ninno.IdentificacionNinno;
                cmd.Parameters.Add("@nombreNinno", System.Data.SqlDbType.VarChar, 15).Value = ninno.NombreNinno;
                cmd.Parameters.Add("@apell1Ninno", System.Data.SqlDbType.VarChar, 15).Value = ninno.Apell1Ninno;
                cmd.Parameters.Add("@apell2Ninno", System.Data.SqlDbType.VarChar, 15).Value = ninno.Apell2Ninno;
                cmd.Parameters.Add("@fechaNacimiento", System.Data.SqlDbType.Date).Value = ninno.FechaNacimiento;
                cmd.Parameters.Add("@direccionNinno", System.Data.SqlDbType.VarChar, 50).Value = ninno.DireccionNinno;
                cmd.Parameters.Add("@idGenero", System.Data.SqlDbType.Int).Value = ninno.IdGenero;
                await cmd.ExecuteNonQueryAsync();
                conn.Close();
                //_context.Add(ninno);
                //await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdGenero"] = new SelectList(_context.Generos, "IdGenero", "DetalleGen", ninno.IdGenero);
            return View(ninno);
        }

        // GET: Ninno/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ninno = await _context.Ninnos.FindAsync(id);
            if (ninno == null)
            {
                return NotFound();
            }
            ViewData["IdGenero"] = new SelectList(_context.Generos, "IdGenero", "DetalleGen", ninno.IdGenero);
            return View(ninno);
        }

        // POST: Ninno/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdNinno,IdentificacionNinno,NombreNinno,Apell1Ninno,Apell2Ninno,FechaNacimiento,DireccionNinno,IdGenero")] Ninno ninno)
        {
            if (id != ninno.IdNinno)
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
                    cmd.CommandText = "sp_ModificarNinnos";
                    cmd.Parameters.Add("@idNinno", System.Data.SqlDbType.Int).Value = ninno.IdNinno;
                    cmd.Parameters.Add("@IdentificacionNinno", System.Data.SqlDbType.VarChar, 15).Value = ninno.IdentificacionNinno;
                    cmd.Parameters.Add("@nombreNinno", System.Data.SqlDbType.VarChar, 15).Value = ninno.NombreNinno;
                    cmd.Parameters.Add("@apell1Ninno", System.Data.SqlDbType.VarChar, 15).Value = ninno.Apell1Ninno;
                    cmd.Parameters.Add("@apell2Ninno", System.Data.SqlDbType.VarChar, 15).Value = ninno.Apell2Ninno;
                    cmd.Parameters.Add("@fechaNacimiento", System.Data.SqlDbType.Date).Value = ninno.FechaNacimiento;
                    cmd.Parameters.Add("@direccionNinno", System.Data.SqlDbType.VarChar, 50).Value = ninno.DireccionNinno;
                    cmd.Parameters.Add("@idGenero", System.Data.SqlDbType.Int).Value = ninno.IdGenero;
                    await cmd.ExecuteNonQueryAsync();
                    conn.Close();
                    //_context.Update(ninno);
                    //await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NinnoExists(ninno.IdNinno))
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
            ViewData["IdGenero"] = new SelectList(_context.Generos, "IdGenero", "DetalleGen", ninno.IdGenero);
            return View(ninno);
        }

        // GET: Ninno/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ninno = await _context.Ninnos
                .Include(n => n.IdGeneroNavigation)
                .FirstOrDefaultAsync(m => m.IdNinno == id);
            if (ninno == null)
            {
                return NotFound();
            }

            return View(ninno);
        }

        // POST: Ninno/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
            SqlCommand cmd = conn.CreateCommand();
            conn.Open();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "sp_EliminarNinnos";
            cmd.Parameters.Add("@idNinno", System.Data.SqlDbType.Int).Value = id;
            await cmd.ExecuteNonQueryAsync();
            conn.Close();
            //var ninno = await _context.Ninnos.FindAsync(id);
            //_context.Ninnos.Remove(ninno);
            //await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NinnoExists(int id)
        {
            return _context.Ninnos.Any(e => e.IdNinno == id);
        }
    }
}
