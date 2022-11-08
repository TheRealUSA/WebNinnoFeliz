using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace WebNinnoFeliz.Models
{
    public partial class Asistencia
    {
        [Key]
        [Column("idAsistencia")]
        public int IdAsistencia { get; set; }
        [Column("fechaAsist", TypeName = "date")]
        public DateTime FechaAsist { get; set; }
        [Column("estadoAsist")]
        public bool EstadoAsist { get; set; }
        [Column("idNinno")]
        public int IdNinno { get; set; }

        [ForeignKey(nameof(IdNinno))]
        [InverseProperty(nameof(Ninno.Asistencia))]
        public virtual Ninno IdNinnoNavigation { get; set; }
    }
}
