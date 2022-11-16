using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebNinnoFeliz.Models.ViewModels
{
    public class AbonadorUsoComedor
    {
        [Required]
        [Display(Name = "idCargo")]
        public int IdCargo { get; set; }
        [Required]
        [Display(Name = "cargoMensual")]
        public string CargoMensual { get; set; }
        [Required]
        [Display(Name = "nombreNinno")]
        [StringLength(15)]
        public string NombreNinno { get; set; }
        [Required]
        [Display(Name = "Apellido")]
        [StringLength(15)]
        public string Apell1Ninno { get; set; }
        [Required]
        [Display(Name = "Apellido2")]
        [StringLength(15)]
        public string Apell2Ninno { get; set; }
    }
}
