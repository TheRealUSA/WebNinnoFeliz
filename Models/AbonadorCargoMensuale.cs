using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace WebNinnoFeliz.Models
{
    [Table("Abonador_CargoMensuales")]
    public partial class AbonadorCargoMensuale
    {
        [Key]
        [Column("idAbonadorCargoMensual")]
        public int IdAbonadorCargoMensual { get; set; }
        [Column("idAbonador")]
        public int IdAbonador { get; set; }
        [Column("idCargo")]
        public int IdCargo { get; set; }

        [ForeignKey(nameof(IdAbonador))]
        [InverseProperty(nameof(Abonadore.AbonadorCargoMensuales))]
        public virtual Abonadore IdAbonadorNavigation { get; set; }
        [ForeignKey(nameof(IdCargo))]
        [InverseProperty(nameof(CargoMensuale.AbonadorCargoMensuales))]
        public virtual CargoMensuale IdCargoNavigation { get; set; }
    }
}
