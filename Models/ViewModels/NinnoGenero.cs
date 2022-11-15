using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebNinnoFeliz.Models.ViewModels
{
    public class NinnoGenero
    {
        [Required]
        [Display(Name = "ID")]
        public int IdNinno { get; set; }
        [Required]
        [Display(Name = "DNI")]
        [StringLength(20)]
        public string IdentificacionNinno { get; set; }
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
        [Column("fechaNacimiento", TypeName = "date")]
        public DateTime FechaNacimiento { get; set; }
        [Required]
        [Display(Name = "direccionNinno")]
        [StringLength(50)]
        public string DireccionNinno { get; set; }
        [Display(Name = "DetalleGenero")]
        public string DetalleGen { get; set; }
    }
}
