using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using WebNinnoFeliz.Data;
using WebNinnoFeliz.Models;
using WebNinnoFeliz.Utilidades;

namespace WebNinnoFeliz.Controllers
{
    public class AuthController : Controller
    {
        public readonly WebNinnoFelizContext _context;
        
        public AuthController(WebNinnoFelizContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(Usuario _usuario)
        {



            SD sd = new SD();



            var usuario = sd.ValidarUsuario(_usuario.CorreoElectronico, _usuario.Contrasenna);



            if (usuario != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, usuario.NombreUsuario),
                    new Claim("CorreoElectronico", usuario.CorreoElectronico),
                };
                foreach (string rol in usuario.Roles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, rol));
                }
                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);



                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));




                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewData["Mensaje"] = "Usuario no encontrado";
                return View();
            }



        }



        public async Task<IActionResult> CerrarSesionAsync()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Inicio", "Index");



        }
        public IActionResult CerrarSesion()
        {
            HttpContext.Session.Remove("Usuario");
            //await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login","Auth");
            
        }
        public static string HashearContrasennas(string texto)
        {
            StringBuilder sb = new ();
            using (SHA256 hash = SHA256Managed.Create())
            {
                Encoding encoding = Encoding.UTF8;
                byte[] result = hash.ComputeHash(encoding.GetBytes(texto));

                foreach (byte b in result)
                    sb.Append(b.ToString("x2"));
            }
            return sb.ToString();
        }
        

    }
}
