using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebNinnoFeliz.Data;
using WebNinnoFeliz.Models;

namespace WebNinnoFeliz.Controllers
{
    public class ConsNinnoEncargadoController : Controller
    {
        private readonly WebNinnoFelizContext _context;

            public ConsNinnoEncargadoController(WebNinnoFelizContext context)
            {
                _context = context;
            }

            // GET: ConsParentezco
            public async Task<IActionResult> Index()
            {
                return View(await _context.NinnoEncargados.Include(n => n.IdEncargadoNavigation).Include(n => n.IdNinnoNavigation).ToListAsync());
            }

        }
    }
