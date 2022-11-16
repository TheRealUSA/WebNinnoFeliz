using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebNinnoFeliz.Models.ViewModels
{
    public class ListarMenu_Platos
    {
        [Required]
        [Display(Name = "ID")]
        public int IdnumeroMenuPlato { get; set; }
        [Required]
        [Display(Name = "NombrePlato")]
       
        [StringLength(20)]
        public string NombrePlato { get; set; }

        [Required]
        [Display(Name = "NombreMenu")]
        [StringLength(20)]
        public string NombreMenu { get; set; }


    }
}
