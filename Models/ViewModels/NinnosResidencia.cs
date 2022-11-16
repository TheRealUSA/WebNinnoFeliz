using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebNinnoFeliz.Models.ViewModels
{
    public class NinnosResidencia
    {
        [Required]
        [Display ( Name = "Direccion")]
        public string direccionNinno { get; set; }
        [Required]
        [Display(Name = "Cantidad de Niños")]
        public int Ninnos { get; set; }
    }
}
