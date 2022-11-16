using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebNinnoFeliz.Models.ViewModels
{
    public class ListarAlergias
    {

        [Required]
        [Display(Name = "ID")]
        public int IdAlergia { get; set; }
        [Required]
        [Display(Name = "Nombre")]
        [StringLength(20)]
        public string NombreAlergia { get; set; }
        [Display(Name = "DetalleTipo")]
        public string NombreTipoAlergia { get; set; }

    }
}
