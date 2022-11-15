using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebNinnoFeliz.Models.ViewModels
{
    public class NinnosAlergicos
    {
        [Required]
        [Display ( Name = "Nombre tipo alergia")]
        public string nombreTipoAlergia { get; set; }
        [Required]
        [Display(Name = "Cantidad de Niños")]
        public int CantidadNinnos { get; set; }
    }
}
