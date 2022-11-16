﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebNinnoFeliz.Models.ViewModels
{
    public class RegistroBajaNinno
    {
        [Required]
        [Display(Name = "idRegistroBaja")]
        public int IdRegistroBaja { get; set; }
        [Column("fechaBaja", TypeName = "date")]
        public DateTime FechaBaja { get; set; }
        [Required]
        [Display(Name = "identificacionNinno")]
        [StringLength(20)]
        public string IdentificacionNinno { get; set; }
        [Required]
        [Display(Name = "Nombre")]
        [StringLength(15)]
        public string NombreNinno { get; set; }
        [Required]
        [Display(Name = "Apellido")]
        [StringLength(15)]
        public string Apell1Ninno { get; set; }
        [Required]
        [Display(Name = "Apellido2")]
        [StringLength(15)]
        public string Apell2Ninno { get; set; }
        
    }
}
