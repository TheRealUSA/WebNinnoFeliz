using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebNinnoFeliz.Models.ViewModels
{
    public class ListarPlato_Ingredientes
    {
        [Required]
        [Display(Name = "ID")]
        public int IdPlatoIngrediente { get; set; }

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
