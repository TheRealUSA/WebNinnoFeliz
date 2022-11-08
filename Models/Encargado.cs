using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace WebNinnoFeliz.Models
{
    public partial class Encargado
    {
        public Encargado()
        {
            Abonadores = new HashSet<Abonadore>();
            EncargadoMatriculas = new HashSet<EncargadoMatricula>();
            EncargadoRegistroDeBajas = new HashSet<EncargadoRegistroDeBaja>();
            NinnoEncargados = new HashSet<NinnoEncargado>();
        }

        [Key]
        [Column("idEncargado")]
        public int IdEncargado { get; set; }
        [Required]
        [Column("identificacionEncargado")]
        [StringLength(20)]
        public string IdentificacionEncargado { get; set; }
        [Required]
        [Column("nombreEncargado")]
        [StringLength(15)]
        public string NombreEncargado { get; set; }
        [Required]
        [Column("apell1Encargado")]
        [StringLength(15)]
        public string Apell1Encargado { get; set; }
        [Required]
        [Column("apell2Encargado")]
        [StringLength(15)]
        public string Apell2Encargado { get; set; }
        [Required]
        [Column("telefonoEncargado")]
        [StringLength(15)]
        public string TelefonoEncargado { get; set; }
        [Required]
        [Column("direcciónEncargado")]
        [StringLength(50)]
        public string DirecciónEncargado { get; set; }
        [Column("idParentezco")]
        public int IdParentezco { get; set; }

        [ForeignKey(nameof(IdParentezco))]
        [InverseProperty(nameof(Parentezco.Encargados))]
        public virtual Parentezco IdParentezcoNavigation { get; set; }
        [InverseProperty(nameof(Abonadore.IdEncargadoNavigation))]
        public virtual ICollection<Abonadore> Abonadores { get; set; }
        [InverseProperty(nameof(EncargadoMatricula.IdEncargadoNavigation))]
        public virtual ICollection<EncargadoMatricula> EncargadoMatriculas { get; set; }
        [InverseProperty(nameof(EncargadoRegistroDeBaja.IdEncargadoNavigation))]
        public virtual ICollection<EncargadoRegistroDeBaja> EncargadoRegistroDeBajas { get; set; }
        [InverseProperty(nameof(NinnoEncargado.IdEncargadoNavigation))]
        public virtual ICollection<NinnoEncargado> NinnoEncargados { get; set; }
    }
}
