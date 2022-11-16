using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebNinnoFeliz.Models.ViewModels
{
    public class ListarNAI_Platos
    {
        [Required]
        [Display(Name = "ID")]
        public int IdNaiplato { get; set; }
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
        [Display(Name = "Nombre")]
        [StringLength(20)]
        public string NombreAlergia { get; set; }
        [Required]
        [Display(Name = "nombreIngrediente")]
        [StringLength(20)]
        public string NombreIngrediente { get; set; }
        [Required]
        [Display(Name = "NombrePlato")]
       
        [StringLength(20)]
        public string NombrePlato { get; set; }

 


    }
}
