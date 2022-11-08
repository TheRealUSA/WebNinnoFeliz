using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace WebNinnoFeliz.Models
{
    public partial class RegistroBaja
    {
        public RegistroBaja()
        {
            EncargadoRegistroDeBajas = new HashSet<EncargadoRegistroDeBaja>();
        }

        [Key]
        [Column("idRegistroBaja")]
        public int IdRegistroBaja { get; set; }
        [Column("fechaBaja", TypeName = "date")]
        public DateTime FechaBaja { get; set; }
        [Column("idNinno")]
        public int IdNinno { get; set; }

        [ForeignKey(nameof(IdNinno))]
        [InverseProperty(nameof(Ninno.RegistroBajas))]
        public virtual Ninno IdNinnoNavigation { get; set; }
        [InverseProperty(nameof(EncargadoRegistroDeBaja.IdRegistroBajaNavigation))]
        public virtual ICollection<EncargadoRegistroDeBaja> EncargadoRegistroDeBajas { get; set; }
    }
}
