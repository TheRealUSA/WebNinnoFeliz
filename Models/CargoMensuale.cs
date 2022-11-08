using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace WebNinnoFeliz.Models
{
    public partial class CargoMensuale
    {
        public CargoMensuale()
        {
            AbonadorCargoMensuales = new HashSet<AbonadorCargoMensuale>();
        }

        [Key]
        [Column("idCargo")]
        public int IdCargo { get; set; }
        [Column("cargoMensual")]
        [StringLength(10)]
        public string CargoMensual { get; set; }
        [Column("idUsoComedor")]
        public int IdUsoComedor { get; set; }

        [ForeignKey(nameof(IdUsoComedor))]
        [InverseProperty(nameof(UsoComedore.CargoMensuales))]
        public virtual UsoComedore IdUsoComedorNavigation { get; set; }
        [InverseProperty(nameof(AbonadorCargoMensuale.IdCargoNavigation))]
        public virtual ICollection<AbonadorCargoMensuale> AbonadorCargoMensuales { get; set; }
    }
}
