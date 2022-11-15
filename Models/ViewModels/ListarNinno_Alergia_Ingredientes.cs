using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebNinnoFeliz.Models.ViewModels
{
    public class ListarNinno_Alergia_Ingredientes
    {
        
        [Required]
        [Display(Name = "ID")]
        public int idNinnoAlergiaIngrediente { get; set; }
        [Required]
        [Display(Name = "Nombre")]
        [StringLength(20)]
        public string nombreAlergia { get; set; }
        [Required]
        [Display(Name = "Nombre Ingrediente")]
        [StringLength(20)]
        public string NombreIngrediente { get; set; }
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
     

    }
}
