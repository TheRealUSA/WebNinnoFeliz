using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebNinnoFeliz.Models.ViewModels
{
    public class NinnosPlatoAlergicos
    {
        [Required]
        [Display ( Name = "Nombre del plato")]
        public string nombrePlato { get; set; }
        [Required]
        [Display(Name = "Cantidad de Niños alergicos al plato")]
        public int niñosalergicosalplato { get; set; }
    }
}
