using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using WebNinnoFeliz.Data;
using WebNinnoFeliz.Models.ViewModels;

namespace WebNinnoFeliz.Controllers
{
    public class ConsGuardaController : Controller
    {
        List<NinnoEncargados> listaninnoencargado = new List<NinnoEncargados>();
        SqlDataAdapter adapter;

        private readonly WebNinnoFelizContext _context;

        public ConsGuardaController(WebNinnoFelizContext context)
        {
            _context = context;
        }

        public List<NinnoEncargados> ListarNinnoEncargado()
        {
            DataTable datatable = new DataTable();
            string error;
            try
            {
                SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
                adapter = new SqlDataAdapter("sp_listarNinno_Encargados", conn);
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
                            NinnoEncargados ninno = new NinnoEncargados();
                            ninno.IdNinnoEncargado = Int32.Parse(datatable.Rows[i][0].ToString());
                            ninno.IdentificacionEncargado = datatable.Rows[i][1].ToString();
                            ninno.NombreEncargado = datatable.Rows[i][2].ToString();
                            ninno.Apell1Encargado = datatable.Rows[i][3].ToString();
                            ninno.Apell2Encargado = datatable.Rows[i][4].ToString();
                            ninno.DetallePar = datatable.Rows[i][5].ToString();
                            ninno.NombreNinno = datatable.Rows[i][6].ToString();
                            ninno.Apell1Ninno = datatable.Rows[i][7].ToString();
                            ninno.Apell2Ninno = datatable.Rows[i][8].ToString();
                            listaninnoencargado.Add(ninno);
                        }
                    }
                    conn.Close();
                }

            }
            catch (Exception e)
            {
                error = e.InnerException.Message;
            }

            return listaninnoencargado;
        }


        public IActionResult Index()
        {
            //var webNinnoFelizContext = _context.Ninnos.Include(n => n.IdGeneroNavigation);
            return View(ListarNinnoEncargado());
        }
    }
}
