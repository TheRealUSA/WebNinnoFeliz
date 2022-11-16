using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebNinnoFeliz.Models.ViewModels
{
    public class AbonadorEncargado
    {
        [Required]
        [Display(Name = "idAbonador")]
        public int IdAbonador { get; set; }
        [Required]
        [Display(Name = "numeroCuenta")]
        public int NumeroCuenta { get; set; }
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
    }
}
