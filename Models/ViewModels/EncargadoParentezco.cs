using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebNinnoFeliz.Models.ViewModels
{
    public class EncargadoParentezco
    {
        [Required]
        [Display(Name = "idEncargado")]
        public int IdEncargado { get; set; }
        [Required]
        [Display(Name = "DNI")]
        public string IdentificacionEncargado { get; set; }
        [Required]
        [Display(Name = "nombreEncargado")]
        [StringLength(15)]
        public string NombreEncargado { get; set; }
        [Required]
        [Display(Name = "apell1Encargado")]
        [StringLength(15)]
        public string Apell1Encargado { get; set; }
        [Required]
        [Display(Name = "apell2Encargado")]
        [StringLength(15)]
        public string Apell2Encargado { get; set; }
        [Required]
        [Display(Name = "telefonoEncargado")]
        [StringLength(15)]
        public string TelefonoEncargado { get; set; }
        [Required]
        [Display(Name = "direcciónEncargado")]
        [StringLength(50)]
        public string DirecciónEncargado { get; set; }
        [Display(Name = "idParentezco")]
        public string DetallePar { get; set; }
    }
}
