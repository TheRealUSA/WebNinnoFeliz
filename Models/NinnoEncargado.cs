using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace WebNinnoFeliz.Models
{
    [Table("Ninno_Encargados")]
    public partial class NinnoEncargado
    {
        [Key]
        [Column("idNinnoEncargado")]
        public int IdNinnoEncargado { get; set; }
        [Column("idEncargado")]
        public int IdEncargado { get; set; }
        [Column("idNinno")]
        public int IdNinno { get; set; }

        [ForeignKey(nameof(IdEncargado))]
        [InverseProperty(nameof(Encargado.NinnoEncargados))]
        public virtual Encargado IdEncargadoNavigation { get; set; }
        [ForeignKey(nameof(IdNinno))]
        [InverseProperty(nameof(Ninno.NinnoEncargados))]
        public virtual Ninno IdNinnoNavigation { get; set; }
    }
}
