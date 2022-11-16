using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebNinnoFeliz.Models;

namespace WebNinnoFeliz.Utilidades
{
    public class SD
    {
        public List<Usuario> ListaUsuario()
        {

            return new List<Usuario>
            {
                
                new Usuario{ NombreUsuario ="Gerald", CorreoElectronico = "admin@gmail.com", Contrasenna= "12345678" , Roles = new string[]{"Administrador"} },
                new Usuario{ NombreUsuario ="Ivan", CorreoElectronico = "secretario@gmail.com", Contrasenna= "12345678" , Roles = new string[]{"Secretario"} },
                new Usuario{ NombreUsuario ="Grettel", CorreoElectronico = "acomedor@gmail.com", Contrasenna= "12345678" , Roles = new string[]{"AsistenteComedor"} },
                new Usuario{ NombreUsuario ="Ethan", CorreoElectronico = "asalud@gmail.com", Contrasenna = "12345678" , Roles = new string[]{"AsistenteSalud"} },
                new Usuario{ NombreUsuario ="Nayeli", CorreoElectronico = "gseguridad@gmail.com", Contrasenna = "12345678" , Roles = new string[]{"Seguridad"} },
                new Usuario{ NombreUsuario ="Ojo", CorreoElectronico = "cliente@gmail.com", Contrasenna = "12345678" , Roles = new string[]{"Cliente"} }
           
            };

        }
        public Usuario ValidarUsuario(string _correo, string _clave)
        {

            return ListaUsuario().Where(item => item.CorreoElectronico == _correo && item.Contrasenna == _clave).FirstOrDefault();

        }


    }
}
