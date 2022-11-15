using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebNinnoFeliz.Models.ViewModels
{
    public class CostePlatos
    {
        [Required]
        [Display ( Name = "Precio Plato")]
        public string precioPlato { get; set; }
        [Required]
        [Display(Name = "Cantidad de Platos")]
        public int CantPlatos { get; set; }
    }
}
