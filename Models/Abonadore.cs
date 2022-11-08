using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace WebNinnoFeliz.Models
{
    public partial class Abonadore
    {
        public Abonadore()
        {
            AbonadorCargoMensuales = new HashSet<AbonadorCargoMensuale>();
        }

        [Key]
        [Column("idAbonador")]
        public int IdAbonador { get; set; }
        [Column("numeroCuenta")]
        public int NumeroCuenta { get; set; }
        [Column("idEncargado")]
        public int IdEncargado { get; set; }

        [ForeignKey(nameof(IdEncargado))]
        [InverseProperty(nameof(Encargado.Abonadores))]
        public virtual Encargado IdEncargadoNavigation { get; set; }
        [InverseProperty(nameof(AbonadorCargoMensuale.IdAbonadorNavigation))]
        public virtual ICollection<AbonadorCargoMensuale> AbonadorCargoMensuales { get; set; }
    }
}
