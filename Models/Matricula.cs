using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace WebNinnoFeliz.Models
{
    public partial class Matricula
    {
        public Matricula()
        {
            EncargadoMatriculas = new HashSet<EncargadoMatricula>();
        }

        [Key]
        [Column("numeroMatricula")]
        public int NumeroMatricula { get; set; }
        [Column("fechaIngreso", TypeName = "date")]
        public DateTime FechaIngreso { get; set; }
        [Column("idNinno")]
        public int IdNinno { get; set; }

        [ForeignKey(nameof(IdNinno))]
        [InverseProperty(nameof(Ninno.Matriculas))]
        public virtual Ninno IdNinnoNavigation { get; set; }
        [InverseProperty(nameof(EncargadoMatricula.NumeroMatriculaNavigation))]
        public virtual ICollection<EncargadoMatricula> EncargadoMatriculas { get; set; }
    }
}
