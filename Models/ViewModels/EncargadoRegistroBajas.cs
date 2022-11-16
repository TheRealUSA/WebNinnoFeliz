using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebNinnoFeliz.Models.ViewModels
{
    public class EncargadoRegistroBajas
    {
        [Required]
        [Display(Name = "idEncargadoRegistroBaja")]
        public int IdEncargadoRegistroBaja { get; set; }
        [Required]
        [Display(Name = "idRegistroBaja")]
        public int IdRegistroBaja { get; set; }
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
        [Display(Name = "idParentezco")]
        public string DetallePar { get; set; }
        [Column("fechaNacimiento", TypeName = "date")]
        public DateTime FechaIngreso { get; set; }
        [Required]
        [Display(Name = "Nombre")]
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
