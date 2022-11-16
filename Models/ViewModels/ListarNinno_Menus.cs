using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebNinnoFeliz.Models.ViewModels
{
    public class ListarNinno_Menus
    {

        [Required]
        [Display(Name = "ID")]
        public int idNinnoMenu { get; set; }
        [Column("fechaConsumido", TypeName = "date")]
        public DateTime FechaConsumido { get; set; }
        [Required]
        [Display(Name = "identificacionNinno")]
        [StringLength(20)]
        public string IdentificacionNinno { get; set; }
        [Required]
        [Display(Name = "Nombre")]
        [StringLength(15)]
        public string NombreNinno { get; set; }
        [Required]
        [Display(Name = "apellido")]
        [StringLength(15)]
        public string Apell1Ninno { get; set; }
        [Required]
        [Display(Name = "apellido")]
        [StringLength(15)]
        public string Apell2Ninno { get; set; }
        [Required]
        [Display(Name = "NombreMenu")]
        [StringLength(20)]
        public string NombreMenu { get; set; }


    }
}
