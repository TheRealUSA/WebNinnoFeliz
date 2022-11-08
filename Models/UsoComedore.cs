using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace WebNinnoFeliz.Models
{
    public partial class UsoComedore
    {
        public UsoComedore()
        {
            CargoMensuales = new HashSet<CargoMensuale>();
        }

        [Key]
        [Column("idUsoComedor")]
        public int IdUsoComedor { get; set; }
        [Column("cantidadDias")]
        public int CantidadDias { get; set; }
        [Column("idMes")]
        public int IdMes { get; set; }
        [Column("idNinno")]
        public int IdNinno { get; set; }

        [ForeignKey(nameof(IdMes))]
        [InverseProperty(nameof(Mese.UsoComedores))]
        public virtual Mese IdMesNavigation { get; set; }
        [ForeignKey(nameof(IdNinno))]
        [InverseProperty(nameof(Ninno.UsoComedores))]
        public virtual Ninno IdNinnoNavigation { get; set; }
        [InverseProperty(nameof(CargoMensuale.IdUsoComedorNavigation))]
        public virtual ICollection<CargoMensuale> CargoMensuales { get; set; }
    }
}
