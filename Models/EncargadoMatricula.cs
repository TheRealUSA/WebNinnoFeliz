using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace WebNinnoFeliz.Models
{
    [Table("Encargado_Matriculas")]
    public partial class EncargadoMatricula
    {
        [Key]
        [Column("idEncargadoMatricula")]
        public int IdEncargadoMatricula { get; set; }
        [Column("numeroMatricula")]
        public int NumeroMatricula { get; set; }
        [Column("idEncargado")]
        public int IdEncargado { get; set; }

        [ForeignKey(nameof(IdEncargado))]
        [InverseProperty(nameof(Encargado.EncargadoMatriculas))]
        public virtual Encargado IdEncargadoNavigation { get; set; }
        [ForeignKey(nameof(NumeroMatricula))]
        [InverseProperty(nameof(Matricula.EncargadoMatriculas))]
        public virtual Matricula NumeroMatriculaNavigation { get; set; }
    }
}
