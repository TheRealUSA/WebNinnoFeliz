using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebNinnoFeliz.Models.ViewModels
{
    public class NinnosDadosBaja
    {
        [Required]
        [Display ( Name = "Año")]
        public string AnnoDadoBaja { get; set; }
        [Required]
        [Display(Name = "Cantidad de Niños")]
        public int CantidadNinnos { get; set; }
    }
}
