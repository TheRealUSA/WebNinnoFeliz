using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace WebNinnoFeliz.Models
{
    [Table("Encargado_RegistroDeBajas")]
    public partial class EncargadoRegistroDeBaja
    {
        [Key]
        [Column("idEncargadoRegistroBaja")]
        public int IdEncargadoRegistroBaja { get; set; }
        [Column("idRegistroBaja")]
        public int IdRegistroBaja { get; set; }
        [Column("idEncargado")]
        public int IdEncargado { get; set; }

        [ForeignKey(nameof(IdEncargado))]
        [InverseProperty(nameof(Encargado.EncargadoRegistroDeBajas))]
        public virtual Encargado IdEncargadoNavigation { get; set; }
        [ForeignKey(nameof(IdRegistroBaja))]
        [InverseProperty(nameof(RegistroBaja.EncargadoRegistroDeBajas))]
        public virtual RegistroBaja IdRegistroBajaNavigation { get; set; }
    }
}
